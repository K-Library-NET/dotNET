using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataEntitiesRT
{
    public class DataTypeConverter
    {
        public const string STRING = "String";
        public const string INT32 = "Int32";
        public const string DATETIME = "DateTime";
        public const string FLOAT = "Single";
        public const string LONG = "Int64";

        public static Type TypeDateTime = typeof(DateTime);
        public static Type TypeInt32 = typeof(int);
        public static Type TypeString = typeof(string);
        public static Type TypeLong = typeof(long);
        public static Type TypeFloat = typeof(float);

        public static string GetString(Type type)
        {
            if (type == TypeDateTime)
                return DATETIME;
            else if (type == TypeInt32)
                return INT32;
            else if (type == TypeLong)
                return LONG;
            else if (type == TypeFloat)
                return FLOAT;
            else
                return STRING;
        }

        public static Type GetType(string type)
        {
            if (type == INT32)
                return TypeInt32;
            else if (type == DATETIME)
                return TypeDateTime;
            else if (type == FLOAT)
                return TypeFloat;
            else if (type == LONG)
                return TypeLong;
            else
                return TypeString;
        }

        //public static MongoDB.Bson.BsonValue ToBsonType(string valueText, string typeStr)
        //{
        //    if (!string.IsNullOrEmpty(typeStr))
        //    {
        //        if (typeStr == DATETIME)
        //            return new MongoDB.Bson.BsonDateTime(DateTime.Parse(valueText));
        //        else if (typeStr == INT32)
        //            return new MongoDB.Bson.BsonInt32(Int32.Parse(valueText));
        //        else
        //            return new MongoDB.Bson.BsonString(valueText);
        //    }

        //    return null;
        //}
    }
}
