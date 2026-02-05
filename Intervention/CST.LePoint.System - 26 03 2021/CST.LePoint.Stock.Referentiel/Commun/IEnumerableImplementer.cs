using System;
using System.Collections;

namespace CST.LePoint.Stock.Referentiel.Commun
{
    public class IEnumerableImplementer : IEnumerable
    {
        public IEnumerableImplementer()
        {
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}