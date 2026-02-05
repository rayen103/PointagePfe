using CST.LePoint.Securite.Entites;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
 

namespace CST.LePoint.Securite.DataAccess.Xml
{
    public class ContexteSecuriteXml : IContexteSecurite
    {
        private string _FichierAccesApplication = string.Empty;
        //    ConfigurationManager.ConnectionStrings["FichierAccesApplication"].ConnectionString;

        private readonly Type[] types = new[]
            {
                typeof (Utilisateur), typeof (Role),
                typeof (Autorisation), typeof (Societe)
            };

        public ContexteSecuriteXml(bool supprimerAvant = false)
        {
            Charger();
        }

        public Entites Entites { get; private set; }

        public void Enregistrer()
        {
            var settings = new XmlWriterSettings
                {
                    Indent = true,
                    OmitXmlDeclaration = true,
                    CloseOutput = true,
                    IndentChars = "\t"
                };

            //var dcs = new DataContractSerializer(typeof(Entites), types, int.MaxValue, false, true,
            //                                     null);

            //using (XmlWriter w = XmlWriter.Create(_FichierAccesApplication, settings))
            //{
            //    dcs.WriteObject(w, Entites);
            //}

            Sauvegarder(Entites);
        }

        public void Sauvegarder(Entites entites)
        {
            if (entites == null)
                return;

            string _CApplication = ConfigurationManager.AppSettings["NomApplication"].ToString();
            //'List<Societe> listeSocietes = this.Set<Societe>().ToList();
            //Societe societe = this.Set<Societe>().;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CFGAcces_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CApplication", _CApplication);
                    //cmd.Parameters.AddWithValue("@CSociete", societe.CSociete);
                    cmd.Parameters.AddWithValue("@XmlAppAcces", entites.ToString());

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void Charger()
        {
           // string _FichierAccesApplication = string.Empty;
            string _CApplication = ConfigurationManager.AppSettings["NomApplication"].ToString();
            //List<Societe> listeSocietes = this.Set<Societe>().ToList;
            //Societe societe = listeSocietes[0];
            Societe societe = Societe.Charger();
            XmlDocument docXml = new XmlDocument();
            MemoryStream xmlStream = new MemoryStream();

            Entites = new Entites();
            var dcs = new DataContractSerializer(typeof(Entites), types, int.MaxValue, false, true,
                                                 null);

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CFGAcces_Charger";
                    cmd.Parameters.AddWithValue("@CApplication", _CApplication);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                        _FichierAccesApplication = dr["XmlAppAcces"].ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            if (string.IsNullOrEmpty(_FichierAccesApplication))
            {
                Entites = new Entites();
                return;
            }
            else
            {
                Entites = Entites.Deserialiser(_FichierAccesApplication);
            }

            //using (var xmlStream = new FileStream(_FichierAccesApplication, FileMode.OpenOrCreate, FileAccess.ReadWrite)
            //    )
            //    Entites = (Entites)dcs.ReadObject(xmlStream);

            //docXml.LoadXml(_FichierAccesApplication);
            //docXml.Save(xmlStream);
            //xmlStream.Flush();
            //xmlStream.Position = 0;
        }

        public ICollection<T> Set<T>() where T : class, new()
        {
            return
                (ICollection<T>)
                (typeof(Entites).GetProperty(typeof(T).Name + "s").GetValue(Entites, Type.EmptyTypes));
        }
    }
}