using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LanguageExt.Prelude;
using LanguageExt;

namespace SugerenciasServer.Model.Repositorios
{
    
    public class BaseConocimientoRepo : IRepository<baseconocimiento>
    {
        private readonly BaseConocimientoConn _db = new BaseConocimientoConn();

        public List<baseconocimiento> GetAll()
        {
            return _db.baseconocimientoes.ToList();
        }

        public Option<baseconocimiento> GetById(int id)
        {
            return _db.baseconocimientoes.Find(id);
        }

        public List<baseconocimiento> GetByQuery(Func<baseconocimiento, bool> query)
        {
            return _db.baseconocimientoes.Where(query).ToList();
        }

        public Try<int> Update(baseconocimiento t)
        {
            Try<int> UpdateEntity(baseconocimiento current, baseconocimiento that)
            {
                current.palabra = that.palabra;
                current.peso = that.peso;
                return () => _db.SaveChanges();
            }

            return GetById(t.id)
                .Some(a => UpdateEntity(a, t))
                .None(() => (() => 0));
        }

        public Try<int> Insert(baseconocimiento t)
        {
            _db.baseconocimientoes.Add(t);
            return () => _db.SaveChanges();
        }

        public Try<int> Delete(int id)
        {
            return GetById(id)
                .Some(e => Try(() =>
                    {
                        _db.baseconocimientoes.Remove(e);
                        return _db.SaveChanges();
                    }
                ))
                .None(() => Try(() => 0));
        }
    }
}