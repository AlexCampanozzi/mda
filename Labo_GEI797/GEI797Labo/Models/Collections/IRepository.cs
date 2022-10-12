using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEI797Labo.Models.Collections
{
    public interface IRepository<T>
    {
        T GetItem(int index);
        List<T> getAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
