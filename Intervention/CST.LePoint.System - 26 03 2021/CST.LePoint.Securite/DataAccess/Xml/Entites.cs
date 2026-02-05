using CST.LePoint.Securite.Entites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace CST.LePoint.Securite.DataAccess.Xml
{
    [DataContract(Namespace = "")]
    public class Entites : IDisposable
    {
        public Entites()
        {
            Autorisations = new Autorisations();
            Autorisations.ItemAdded += Autorisations_ItemAdded;

            Utilisateurs = new Utilisateurs();
            Utilisateurs.ItemAdded += Utilisateurs_ItemAdded;

            Roles = new Roles();
            Roles.ItemAdded += Roles_ItemAdded;
            Roles.ItemRemoved += Roles_ItemRemoved;

            Societes = new Societes();
            Societes.ItemAdded += Societes_ItemAdded;
            Societes.ItemRemoved += Societes_ItemRemoved;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            xmlSerializer.Serialize(new StringWriter(sb), this);
            return sb.ToString();
        }

        [DataMember]
        public Autorisations Autorisations { get; set; }

        [DataMember]
        public Utilisateurs Utilisateurs { get; set; }

        [DataMember]
        public Roles Roles { get; set; }

        [DataMember]
        public Societes Societes { get; set; }

        private void Autorisations_ItemAdded(Autorisation item)
        {
        }

        private void Utilisateurs_ItemAdded(Utilisateur item)
        {
            foreach (Role role in item.Roles)
            {
                Roles.Add(role);
            }
        }

        private void Roles_ItemAdded(Role role)
        {
            foreach (Autorisation autorisation in role.Autorisations)
            {
                Autorisations.Add(autorisation);
            }
        }

        private void Roles_ItemRemoved(Role role)
        {
            List<Utilisateur> users = Utilisateurs.Where(u => u.Roles.Any(r => r.Id == role.Id)).ToList();
            users.ForEach(u => u.Roles.Remove(role));
        }

        private void Societes_ItemAdded(Societe societe)
        {
        }

        private void Societes_ItemRemoved(Societe societe)
        {
            //foreach (Utilisateur utilisateur in Utilisateurs.Where(u => u.Societe.CSociete == societe.CSociete))
            //{
            //    Utilisateurs.Remove(utilisateur);
            //}
        }

        public void Dispose()
        {
            this.Autorisations = null;
            this.Roles = null;
            this.Societes = null;
            this.Utilisateurs = null;
        }

        public Entites Deserialiser(string xmlValeur)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Entites));
            return xmlSerializer.Deserialize(new StringReader(xmlValeur)) as Entites;
        }
    }
}