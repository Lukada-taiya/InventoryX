using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Domain
{
    public interface IBaseRepository<T>
    {
        void Add(T entity);
        List<T> GetAll();
        T Get(int id);
        void Update(T entity);
        void Delete(int id);
    }
}
