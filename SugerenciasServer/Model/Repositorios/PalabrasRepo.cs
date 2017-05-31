using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LanguageExt.Prelude;
using LanguageExt;

namespace SugerenciasServer.Model.Repositorios
{
    public class PalabrasRepo : IRepository<palabra>
    {
        private readonly SugerenciasEntities _db = new SugerenciasEntities();
        public List<palabra> GetAll()
        {
            return _db.palabras.ToList();
        }

        public Option<palabra> GetById(int id)
        {
            return _db.palabras.Find(id);
        }

        public List<palabra> GetByQuery(Func<palabra, bool> query)
        {
            return _db.palabras.Where(query).ToList();
        }

        public Try<int> Update(palabra t)
        {
            Try<int> UpdateEntity(palabra current, palabra that)
            {
                current.palabra1 = that.palabra1;
                return () => _db.SaveChanges();
            }

            return GetById(t.id)
                .Some(a => UpdateEntity(a, t))
                .None(() => (() => 0));
        }

        public Try<int> Insert(palabra t)
        {
            _db.palabras.Add(t);
            return () => _db.SaveChanges();
        }

        public Try<int> Delete(int id)
        {
            return GetById(id)
                .Some(e => Try(() =>
                {
                    _db.palabras.Remove(e);
                    return _db.SaveChanges();
                }
                ))
                .None(() => Try(() => 0));
        }
    }
}