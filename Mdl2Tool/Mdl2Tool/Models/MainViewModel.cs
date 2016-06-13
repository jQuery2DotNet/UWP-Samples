using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using GalaSoft.MvvmLight;
using Windows.Data.Json;
using Windows.Storage;

namespace Mdl2Tool.Models
{
    public class MainViewModel : ViewModelBase
    {
        public MdlModel MdlModel { get; set; }

        public List<MDL> MDLIconList { get; set; }

        public MDL MDLIconDetail => MDLIconList.Where(p => p.name == MdlIconSelected?.Key ?? string.Empty).FirstOrDefault();

        private string search;

        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged("MdlIconFilter");
            }
        }

        public Dictionary<string, string> MdlIconFilter
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(search))
                    return MdlModel.IconList.Where(p => p.Key.ToLower().Contains(search)).ToDictionary(x => x.Key, x => x.Value);

                return MdlModel.IconList;
            }
        }

        private dynamic mdlIconSelected;

        public dynamic MdlIconSelected
        {
            get
            {
                return mdlIconSelected;
            }

            set
            {
                mdlIconSelected = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged("MDLIconDetail");
                this.RaisePropertyChanged("MdlIconSyntex");

            }
        }

        public string MdlIconSyntex => ($@"<TextBlock Text=""{ MDLIconDetail?.code ?? string.Empty  }""
FontFamily=""Segoe MDL2 Assets"" /> ");


        public MainViewModel()
        {
            MdlModel = new MdlModel();
            MDLIconList = GetMDLIconList();
            MdlIconSelected = MdlIconFilter.FirstOrDefault();
        }

        private List<MDL> GetMDLIconList()
        {
            List<MDL> lstContent = new List<MDL>();
            Uri appUri = new Uri("ms-appx:///Assets/Mdl2.json"); //File name should be prefixed with 'ms-appx:///Assets/* 
            StorageFile anjFile = StorageFile.GetFileFromApplicationUriAsync(appUri).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
            string jsonText = FileIO.ReadTextAsync(anjFile).AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
            var jsonSerializer = new DataContractJsonSerializer(typeof(MDL));
            JsonArray anjarray = JsonArray.Parse(jsonText);
            foreach (JsonValue oJsonVal in anjarray)
            {
                JsonObject oJsonObj = oJsonVal.GetObject();
                using (MemoryStream jsonStream = new MemoryStream(Encoding.Unicode.GetBytes(oJsonObj.ToString())))
                {
                    MDL oContent = (MDL)jsonSerializer.ReadObject(jsonStream);
                    lstContent.Add(oContent);
                }
            }

            return lstContent;
        }

    }
}
