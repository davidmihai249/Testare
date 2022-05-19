using System.Collections.Generic;

namespace ShoppingCart
{
    public interface Repository<T,TID>
    {
        void Save(T elem);

        void Delete(T elem);

        void Update(T elem, TID ID);

        T FindById(TID ID);

        IEnumerable<T> FindAll();
    }
}