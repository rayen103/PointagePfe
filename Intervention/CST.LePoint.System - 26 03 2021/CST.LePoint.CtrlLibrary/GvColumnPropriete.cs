using CST.LePoint.Referentiel;
using System.Collections.Generic;

namespace CST.LePoint.CtrlLibrary
{
    public class GvColumnPropriete
    {
        private GvColumnPropriete()
        {
            this.Titre = string.Empty;
            this.Type = GvColumnType.String;
            this.Etat = GvColumnEtat.Disable;
            this.ItemCollection = null;
            this.ToolTip = string.Empty;
            this.Merge = GvColumnMerge.Default;

        }

        public GvColumnPropriete(string titre)
            : this()
        {
            this.Titre = titre;
        }

        public GvColumnPropriete(string titre, GvColumnType type)
            : this(titre)
        {
            this.Type = type;
        }

        public GvColumnPropriete(string titre, GvColumnType type, GvColumnMerge merge)
            : this(titre)
        {
            this.Type = type;
            this.Merge = merge;
        }

        public GvColumnPropriete(string titre, GvColumnMerge merge)
            : this(titre)
        {
            this.Merge = merge;
        }

        public GvColumnPropriete(string titre, GvColumnType type, string toolTip)
            : this(titre)
        {
            this.Type = type;
            this.ToolTip = toolTip;
        }

        public GvColumnPropriete(string titre, GvColumnType type, ItemCollection itemCollection)
            : this(titre)
        {
            this.Type = type;
            this.ItemCollection = itemCollection;
        }

        public GvColumnPropriete(string titre, GvColumnEtat etat)
            : this(titre)
        {
            this.Etat = etat;
        }

        public GvColumnPropriete(string titre, GvColumnEtat etat, GvColumnMerge merge)
            : this(titre)
        {
            this.Etat = etat;
            this.Merge = merge;
        }
        
        public GvColumnPropriete(string titre, GvColumnType type, GvColumnEtat etat)
            : this(titre)
        {
            this.Type = type;
            this.Etat = etat;
        }

        public GvColumnPropriete(string titre, GvColumnType type, GvColumnEtat etat, GvColumnMerge merge)
            : this(titre)
        {
            this.Type = type;
            this.Etat = etat;
            this.Merge = merge;
        }


        public GvColumnPropriete(string titre, GvColumnType type, GvColumnEtat etat, ItemCollection itemCollection)
            : this(titre)
        {
            this.Type = type;
            this.Etat = etat;
            this.ItemCollection = itemCollection;
        }

        public enum GvColumnEtat
        {
            Disable,
            Enable,
            Invisible,
            Visible
        }

        public enum GvColumnMerge
        {
            Default,
            AllowMerge,
            NotAllowMerge,
        }

        public enum GvColumnType
        {
            LookUp,
            LookUpVide,
            Boolean,
            Date,
            DateTime,
            Time,
            Decimal,
            Currency,
            Integer,
            Percent,
            String,
            Button,
            DecimalPositif,
            Photos,
            Color,
            Memo
        }

        public GvColumnEtat Etat { get; set; }

        public ItemCollection ItemCollection { get; set; }

        public string Titre { get; set; }

        public string ToolTip { get; set; }

        public GvColumnType Type { get; set; }

        public GvColumnMerge Merge { get; set; }
    }

    public class GvColumnProprietes : List<GvColumnPropriete>
    {
    }
}