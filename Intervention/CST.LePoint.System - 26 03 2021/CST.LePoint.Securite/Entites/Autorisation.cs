using CST.LePoint.Tools;
using System.Runtime.Serialization;

namespace CST.LePoint.Securite.Entites
{
    [DataContract(Namespace = "")]
    public sealed class Autorisation
    {
        #region - Propriétés -

        [IgnoreDataMember]
        public Actions Actions { get; set; }

        [DataMember(Name = "Actions")]
        private int ActionsInt { get { return (int)Actions; } set { Actions = (Actions)value; } }

        [DataMember]
        public string NomForm { get; set; }

        //[IgnoreDataMember]
        //public HashSetSerializable<Role> Roles { get; set; }

        #endregion - Propriétés -

        #region - Méthodes -

        public Autorisation()
        {
            Actions = Actions.Rien;
           // this.Roles = new HashSetSerializable<Role>();
        }

        public bool ContainsOperation(Actions op)
        {
            return (op & Actions) == op;
        }

        public void AddOperation(Actions op)
        {
            Actions |= op;
        }

        public void RemoveOperation(Actions op)
        {
            Actions &= ~op;
        }

        public override string ToString()
        {
            return NomForm + " " + Actions.ToString();
        }

        public bool Equals(Autorisation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Actions, Actions) && Equals(other.NomForm, NomForm);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Autorisation)) return false;
            return Equals((Autorisation)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Actions.GetHashCode() * 397) ^ (NomForm != null ? NomForm.GetHashCode() : 0);
            }
        }

        #endregion - Méthodes -
    }

    [CollectionDataContract(Namespace = "")]
    public class Autorisations : HashSetSerializable<Autorisation>
    {
    }
}