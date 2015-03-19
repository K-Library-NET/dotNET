using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public class ModelRawDataCollection
    {
        public ModelRawDataCollection()
        {
            ValueAddedKey = new List<string>();
            RawDataItems = new ObservableCollection<ModelRawData>();
        }

        public List<string> ValueAddedKey
        {
            get;
            set;
        }

        public ObservableCollection<ModelRawData> RawDataItems
        {
            get;
            set;
        }

        public void AppendToCollection(
            ObservableCollection<KeyValuePair<string, ObservableCollection<ParameterRawData>>>
            collection, ModelRawDataBuilder builder)
        {
            //clear
            this.ValueAddedKey.Clear();
            //this.RawDataItems.Clear();

            Dictionary<int, ModelRawData> dicSecond = new Dictionary<int, ModelRawData>();

            foreach (var one in collection)
            {
                ValueAddedKey.Add(one.Key);
                foreach (var two in one.Value)
                {
                    if (!dicSecond.ContainsKey(two.Second))
                    {
                        ModelRawData dt = builder.CreateModelRawDataObj();
                        dt.Second = two.Second;
                        dicSecond.Add(two.Second, dt);
                    }

                    ModelRawData data = dicSecond[two.Second];
                    builder.AssignValue(data, two);
                }
            }

            var result = from i in dicSecond
                         orderby i.Key ascending
                         select i.Value;

            this.RawDataItems = new ObservableCollection<ModelRawData>(result);
        }
    }
}
