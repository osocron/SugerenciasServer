using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;
using static LanguageExt.Prelude;
using SugerenciasServer.Model.Repositorios;

namespace SugerenciasServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SugerenciasService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SugerenciasService.svc or SugerenciasService.svc.cs at the Solution Explorer and start debugging.
    public class SugerenciasService : ISugerenciasService
    {
        private readonly UsuariosRepo _usuariosRepo = new UsuariosRepo();
        private readonly PalabrasRepo _palabrasRepo = new PalabrasRepo();
        private readonly BaseConocimientoRepo _baseConocimientoRepo = new BaseConocimientoRepo();
        private readonly AcumuladorRepo _acumuladorRepo = new AcumuladorRepo();
        private readonly int _pesoRequerido = 10;

        public string RegistrarPalabra(string usuario, string password, string palabra)
        {
            var p = new palabra()
            {
                palabra1 = palabra
            };
            return _usuariosRepo.GetByQuery(u =>
                    u.usuario == usuario &&
                    u.password == password)
                .HeadOrNone()
                .Match(
                    Some: user => _palabrasRepo.Insert(p)
                        .Match(
                            Succ: i => i != 0
                                ? _baseConocimientoRepo
                                    .GetDB().baseconocimientoes.Where(b => b.palabra == palabra).ToList()
                                    .HeadOrNone()
                                    .Match(
                                        Some: conocmiento => _acumuladorRepo.Insert(new acomulador()
                                        {
                                            id_base_conocimiento = conocmiento.id,
                                            id_palabras = p.id
                                        }).Match(
                                            Succ: c => "Exitoso, encontrado y acumulado",
                                            Fail: ex => ex.Message),
                                        None: () => "Exitoso, no encontrado en base de conocimiento")
                                : "No se pudo insertar la palabra",
                            Fail: ex => ex.Message),
                    None: () => "Credenciales no encontradas");
        }

        public List<string> RegresarResultado(string usuario, string password)
        {
            return _usuariosRepo.GetByQuery(u =>
                    u.usuario == usuario &&
                    u.password == password)
                .HeadOrNone()
                .Match(
                    Some: user =>
                    {
                        SugerenciasEntities db = new SugerenciasEntities();
                        var pesoTotal = (from acc in db.acomuladors
                            join pBaseCon in db.baseconocimientoes on acc.id_base_conocimiento equals pBaseCon.id
                            select pBaseCon).ToList().Aggregate(0, (acc, next) => acc + next.peso);
                        return pesoTotal >= _pesoRequerido ? TruncateTableAndReturnResult(db) : new List<string>();
                    },
                    None: () => new List<string>());
        }

        private List<string> TruncateTableAndReturnResult(SugerenciasEntities db)
        {
            db.acomuladors.RemoveRange(db.acomuladors);
            db.SaveChanges();
            return GoogleSearch();
        }

        private List<string> GoogleSearch()
        {
            const string apiKey = "AIzaSyB13pz0pD46cc7UHpUe8sPE0GcyOc3Nko8";
            const string searchEngineId = "003925559047818067440:h5c9q2euouo";
            const string query = "hardware";
            var customSearchService = new CustomsearchService(new BaseClientService.Initializer { ApiKey = apiKey });
            var listRequest = customSearchService.Cse.List(query);
            listRequest.Cx = searchEngineId;
            listRequest.Hl = "es-419";
            listRequest.Lr = CseResource.ListRequest.LrEnum.LangEs;
            List<string> result = new List<string>();
            try
            {
                foreach (var item in listRequest.Execute().Items)
                {
                    result.Add(item.Link);
                }
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }

    }
}
