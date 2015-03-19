using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    //public class FlyParameterHelper
    //{
    //    public FlyParameter FromJson(string jsonContent)
    //    {
    //        Newtonsoft.Json.JsonSerializer ser = new Newtonsoft.Json.JsonSerializer();
            
    //        System.IO.StringReader reader = new System.IO.StringReader(jsonContent);
    //        Newtonsoft.Json.JsonReader jsonReader = new Newtonsoft.Json.JsonTextReader(reader);

    //        FlyParameter fp = ser.Deserialize<FlyParameter>(jsonReader);

    //        return fp;
    //    }

    //    public string ToJson(FlyParameter parameter)
    //    {
    //        Newtonsoft.Json.JsonSerializer ser = new Newtonsoft.Json.JsonSerializer();
    //        StringBuilder builder = new StringBuilder();
    //        System.IO.StringWriter writer = new System.IO.StringWriter(builder);
    //        ser.Serialize(writer, parameter, typeof(FlyParameter));
    //        return builder.ToString();
    //    }
    //}
}
