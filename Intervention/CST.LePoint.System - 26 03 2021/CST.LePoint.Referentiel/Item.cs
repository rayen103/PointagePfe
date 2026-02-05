using System.Collections.Generic;
using System.Linq;

namespace CST.LePoint.Referentiel
{
    public class Item
    {
        public string Code { get; set; }

        public string Libelle { get; set; }

        public Item()
        {
            Code = string.Empty;
            Libelle = string.Empty;
        }
    }

    public class ItemCollection : List<Item>
    {
        public bool Existe(Item item)
        {
            return (this.Where(x => x.Code == item.Code).FirstOrDefault() != null);
        }

        public Item Obtenir(string code)
        {
            Item item = this.Where(x => x.Code.Equals(code)).FirstOrDefault();
            return item;
        }
    }
}