using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.LePoint.Intervention.Metier
{
    public class RattachementPhotos
    {
        #region Proprities

        public int IDPhoto { get; set; }

        public string Photo { get; set; }

        public string NRattachement { get; set; }

        #endregion
    }

    public class RattachementPhotosCollection : List<RattachementPhotos>
    {
        public static RattachementPhotosCollection charger(string NRattachement)
        {
            RattachementPhotosCollection photos = new RattachementPhotosCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_RattachementPhotos_Charger";
                    cmd.Parameters.AddWithValue("@NRattachement", NRattachement);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            RattachementPhotos photo = new RattachementPhotos();

                            photo.IDPhoto = int.Parse(dr["IDPhoto"].ToString());
                            photo.NRattachement = dr["NRattachement"].ToString();
                            if (dr["Photo"] != DBNull.Value)
                                photo.Photo = dr["Photo"].ToString();
                            photos.Add(photo);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return photos;
        }
    }
}
