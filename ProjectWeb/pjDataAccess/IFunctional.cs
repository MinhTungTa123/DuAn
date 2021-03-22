using System;
using System.Collections.Generic;
using System.Text;

namespace pjDataAccess
{
    interface IFunctional<T>
    {
        public ICollection<T> getall();
        public T add(T Tmodel);
        public T delete(T Tmodel);
        public T update(T Tmodel);
        public T get(int id);

        public void update1(T t1, T t2);

    }
}
