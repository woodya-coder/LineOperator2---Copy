using LineOperator2.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LineOperator2.Models
{
    public class Product : BaseViewModel
    {
        private int id;
        [SQLite.AutoIncrement, SQLite.PrimaryKey]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string partname ="";
        public string PartName
        {
            get { return partname; }
            set { SetProperty(ref partname, value); }
        }

        private int partsperbox = 0;
        public int PartsPerBox
        {
            get { return partsperbox; }
            set { SetProperty(ref partsperbox,value); }
        }

        private float cutlength = 0.0f;
        public float CutLength
        {
            get { return cutlength; }
            set { SetProperty(ref cutlength, value); }
        }

        private int multiplier = 1;
        public int Multiplier
        {
            get { return multiplier; }
            set { SetProperty(ref multiplier, value); }
        }

        private string boxLength = "12 ft";
        public string BoxLength
        {
            get { return boxLength; }
            set { SetProperty(ref boxLength, value); }
        }

        private string palletSize = "12 ft";
        public string PalletSize
        {
            get { return palletSize; }
            set { SetProperty(ref palletSize, value); }
        }

        private string crateSize = "";
        public string CrateSize
        {
            get { return crateSize; }
            set { SetProperty(ref crateSize, value); }
        }
    }
}
