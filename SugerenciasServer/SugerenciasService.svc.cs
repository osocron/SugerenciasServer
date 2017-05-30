using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SugerenciasServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SugerenciasService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SugerenciasService.svc or SugerenciasService.svc.cs at the Solution Explorer and start debugging.
    public class SugerenciasService : ISugerenciasService
    {

        public string RegistrarPalabra(string usuario, string password, string palabra)
        {
            throw new NotImplementedException();
        }

        public List<string> RegresarResultado(string usuario, string password)
        {
            throw new NotImplementedException();
        }
    }
}
