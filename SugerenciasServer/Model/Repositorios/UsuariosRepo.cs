using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LanguageExt.Prelude;
using LanguageExt;

namespace SugerenciasServer.Model.Repositorios
{
    public class UsuariosRepo : IRepository<TblUsuario>
    {
        private readonly SugerenciasEntities _db = new SugerenciasEntities();
        public List<TblUsuario> GetAll()
        {
            return _db.TblUsuarios.ToList();
        }

        public Option<TblUsuario> GetById(int id)
        {
            return _db.TblUsuarios.Find(id);
        }

        public List<TblUsuario> GetByQuery(Func<TblUsuario, bool> query)
        {
            return _db.TblUsuarios.Where(query).ToList();
        }

        public Try<int> Update(TblUsuario t)
        {
            Try<int> UpdateEntity(TblUsuario current, TblUsuario that)
            {
                current.usuario = that.usuario;
                current.password = that.password;
                return () => _db.SaveChanges();
            }

            return GetById(t.Id_Usuario)
                .Some(a => UpdateEntity(a, t))
                .None(() => (() => 0));
        }

        public Try<int> Insert(TblUsuario t)
        {
            _db.TblUsuarios.Add(t);
            return () => _db.SaveChanges();
        }

        public Try<int> Delete(int id)
        {
            return GetById(id)
                .Some(e => Try(() =>
                {
                    _db.TblUsuarios.Remove(e);
                    return _db.SaveChanges();
                }
                ))
                .None(() => Try(() => 0));
        }
    }
}