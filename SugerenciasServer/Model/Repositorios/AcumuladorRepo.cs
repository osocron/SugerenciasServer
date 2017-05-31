using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LanguageExt.Prelude;
using LanguageExt;

namespace SugerenciasServer.Model.Repositorios
{
    public class AcumuladorRepo : IRepository<acomulador>
    {
        private readonly SugerenciasEntities _db = new SugerenciasEntities();
        public List<acomulador> GetAll()
        {
            return _db.acomuladors.ToList();
        }

        public Option<acomulador> GetById(int id)
        {
            return _db.acomuladors.Find(id);
        }

        public List<acomulador> GetByQuery(Func<acomulador, bool> query)
        {
            return _db.acomuladors.Where(query).ToList();
        }

        public Try<int> Update(acomulador t)
        {
            throw new NotSupportedException();
        }

        public Try<int> Insert(acomulador t)
        {
            _db.acomuladors.Add(t);
            return () => _db.SaveChanges();
        }

        public Try<int> Delete(int id)
        {
            return GetById(id)
                .Some(e => Try(() =>
                {
                    _db.acomuladors.Remove(e);
                    return _db.SaveChanges();
                }
                ))
                .None(() => Try(() => 0));
        }
    }
}