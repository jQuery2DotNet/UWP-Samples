using System.Collections.Generic;
using GalaSoft.MvvmLight;
using Mdl2Tool.Utility;

namespace Mdl2Tool.Models
{
    public class MdlModel : ViewModelBase
    {
        public MdlModel()
        {
            IconList = Helper.GetFieldValues(new Mdl2());
        }

        private Dictionary<string, string> iconList;

        public Dictionary<string, string> IconList
        {
            get { return iconList; }
            set
            {
                iconList = value;
                this.RaisePropertyChanged();
            }
        }

    }

    public class MDL
    {
        public string code { get; set; }

        public string name { get; set; }

        public string unicode { get; set; }
    }
}
