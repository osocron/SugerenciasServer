using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;

namespace SugerenciasServer.Model.Repositorios
{
    interface IRepository<T>
    {
        List<T> GetAll();
        Option<T> GetById(int id);
        List<T> GetByQuery(Func<T, bool> query); 
        Try<int> Update(T t);
        Try<int> Insert(T t);
        Try<int> Delete(int id);
    }
}
