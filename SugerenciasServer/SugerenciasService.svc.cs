using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
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
            throw new NotImplementedException();
        }
    }
}
