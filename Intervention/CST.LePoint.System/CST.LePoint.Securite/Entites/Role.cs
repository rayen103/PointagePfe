using CST.LePoint.Tools;
using System;
using System.Runtime.Serialization;

namespace CST.LePoint.Securite.Entites
{
    [DataContract(Namespace = "")]
    public class Role
    {
        public Role()
        {
            this.Autorisations = new HashSetSerializable<Autorisation>();
            //this.Utilisateurs = new HashSetSerializable<Utilisateur>();

            Id = Guid.NewGuid();
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Nom { get; set; }
        [DataMember]
        public string Societe { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string CSociete { get; set; }

        [DataMember]
        public virtual HashSetSerializable<Autorisation> Autorisations { get; set; }

        //[IgnoreDataMember]
        //public virtual HashSetSerializable<Utilisateur> Utilisateurs { get; set; }

        public bool Equals(Role other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Role)) return false;
            return Equals((Role)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Nom;
        }
    }

    [CollectionDataContract(Namespace = "")]
    public class Roles : HashSetSerializable<Role>
    {
    }
}