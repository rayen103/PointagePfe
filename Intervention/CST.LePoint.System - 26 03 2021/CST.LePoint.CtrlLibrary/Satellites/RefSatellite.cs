using System.Collections;

namespace CST.LePoint.CtrlLibrary.Satellites
{
    public class RefSatellites : ArrayList
    {
        public RefSatellites()
        {
        }
    }


    public class RefSatellite
    {
        private int id;
        private int parentID;
        private string name;

        public RefSatellite(string name, int id)
            : this(name, id, -1)
        {
        }

        public RefSatellite(string name, int id, int parentID)
        {
            this.id = id;
            this.parentID = parentID;
            this.name = name;
        }

        public int ID
        {
            get { return id; }
        }

        public int ParentID
        {
            get { return parentID; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}