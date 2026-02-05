using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.LePoint.VenteMobile.Metier
{
   public class Photos
    {
        private string _Pic;
        public string Pic
        {
            get { return _Pic; }
            set { _Pic = value; }
        }
        private string _PicID;
        public string PicID
        {
            get { return _PicID; }
            set { _PicID = value; }
        }
    }
}
