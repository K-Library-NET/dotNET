using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading.AircraftModel1
{
    public class AircraftModel1RawDataBuilder : FlightDataEntitiesRT.ModelRawDataBuilder
    {
        public void AssignValueExt(AircraftModel1RawData data, FlightDataEntitiesRT.ParameterRawData two)
        {
            if (data != null && two != null && two.Values != null && two.Values.Count() > 0)
            {
                AircraftModel1RawData dt = data;
                if (!string.IsNullOrEmpty(two.ParameterID))// && dt.Fields.Contains(two.ParameterID))
                {
                    /*Fields = new string[] {
                "Second", "Et","Hp","Vi","M","aT","Vy","Tt","ZH","HG","FY","EW","NS",
                     */
                    if (two.ParameterID == "Et")
                        dt.Et = two.Values[0];
                    if (two.ParameterID == "Hp")
                        dt.Hp = two.Values[0];
                    if (two.ParameterID == "Vi")
                        dt.Vi = two.Values[0];
                    if (two.ParameterID == "M")
                        dt.M = two.Values[0];
                    if (two.ParameterID == "aT")
                        dt.aT = two.Values[0];
                    if (two.ParameterID == "Vy")
                        dt.Vy = two.Values[0];
                    if (two.ParameterID == "Tt")
                        dt.Tt = two.Values[0];
                    if (two.ParameterID == "ZH")
                        dt.ZH = two.Values[0];
                    if (two.ParameterID == "HG")
                        dt.HG = two.Values[0];
                    if (two.ParameterID == "EW")
                        dt.EW = two.Values[0];
                    if (two.ParameterID == "NS")
                        dt.NS = two.Values[0];
                    //"DR","GS","Wy","Wx","Wz","KZB","KCB","ZS","CS","Ny","Nx","Nz",
                    if (two.ParameterID == "DR")
                        dt.DR = two.Values[0];
                    if (two.ParameterID == "GS")
                        dt.GS = two.Values[0];
                    if (two.ParameterID == "Wy")
                        dt.Wy = two.Values[0];
                    if (two.ParameterID == "Wx")
                        dt.Wx = two.Values[0];
                    if (two.ParameterID == "Wz")
                        dt.Wz = two.Values[0];
                    if (two.ParameterID == "KZB")
                        dt.KZB = two.Values[0];
                    if (two.ParameterID == "KCB")
                        dt.KCB = two.Values[0];
                    if (two.ParameterID == "ZS")
                        dt.ZS = two.Values[0];
                    if (two.ParameterID == "CS")
                        dt.CS = two.Values[0];
                    if (two.ParameterID == "Ny")
                        dt.Ny = two.Values[0];
                    if (two.ParameterID == "Nx")
                        dt.Nx = two.Values[0];
                    if (two.ParameterID == "Nz")
                        dt.Nz = two.Values[0];
                    //"Dy","Dx","Dz","T6L","T6R","NHL","NHR",
                    if (two.ParameterID == "Dy")
                        dt.Dy = two.Values[0];
                    if (two.ParameterID == "Dx")
                        dt.Dx = two.Values[0];
                    if (two.ParameterID == "Dz")
                        dt.Dz = two.Values[0];
                    if (two.ParameterID == "T6L")
                        dt.T6L = two.Values[0];
                    if (two.ParameterID == "T6R")
                        dt.T6R = two.Values[0];
                    if (two.ParameterID == "NHL")
                        dt.NHL = two.Values[0];
                    if (two.ParameterID == "NHR")
                        dt.NHR = two.Values[0];
                    //"KG1","KG2","KG3","KG4","KG5","KG6","KG7","KG8","KG9","KG10","KG11","KG12","KG13","KG14","KG15"
                    if (two.ParameterID == "KG1")
                        dt.KG1 = (int)two.Values[0];
                    if (two.ParameterID == "KG2")
                        dt.KG2 = (int)two.Values[0];
                    if (two.ParameterID == "KG3")
                        dt.KG3 = (int)two.Values[0];
                    if (two.ParameterID == "KG4")
                        dt.KG4 = (int)two.Values[0];
                    if (two.ParameterID == "KG5")
                        dt.KG5 = (int)two.Values[0];
                    if (two.ParameterID == "KG6")
                        dt.KG6 = (int)two.Values[0];
                    if (two.ParameterID == "KG7")
                        dt.KG7 = (int)two.Values[0];
                    if (two.ParameterID == "KG8")
                        dt.KG8 = (int)two.Values[0];
                    if (two.ParameterID == "KG9")
                        dt.KG9 = (int)two.Values[0];
                    if (two.ParameterID == "KG10")
                        dt.KG10 = (int)two.Values[0];
                    if (two.ParameterID == "KG11")
                        dt.KG11 = (int)two.Values[0];
                    if (two.ParameterID == "KG12")
                        dt.KG12 = (int)two.Values[0];
                    if (two.ParameterID == "KG13")
                        dt.KG13 = (int)two.Values[0];
                    if (two.ParameterID == "KG14")
                        dt.KG14 = (int)two.Values[0];
                    if (two.ParameterID == "KG15")
                        dt.KG15 = (int)two.Values[0];

                }
            }
        }



        public override FlightDataEntitiesRT.ModelRawData CreateModelRawDataObj()
        {
            return null;
            // return new AircraftModel1RawData();
        }

        public override void AssignValue(FlightDataEntitiesRT.ModelRawData data, FlightDataEntitiesRT.ParameterRawData two)
        {
            if (data != null && data is AircraftModel1RawData && two != null && two.Values != null && two.Values.Count() > 0)
            {
                AircraftModel1RawData dt = new AircraftModel1RawData(); // data as AircraftModel1RawData;
                if (!string.IsNullOrEmpty(two.ParameterID))// && dt.Fields.Contains(two.ParameterID))
                {
                    /*Fields = new string[] {
                "Second", "Et","Hp","Vi","M","aT","Vy","Tt","ZH","HG","FY","EW","NS",
                     */
                    if (two.ParameterID == "Et")
                        dt.Et = two.Values[0];
                    if (two.ParameterID == "Hp")
                        dt.Hp = two.Values[0];
                    if (two.ParameterID == "Vi")
                        dt.Vi = two.Values[0];
                    if (two.ParameterID == "M")
                        dt.M = two.Values[0];
                    if (two.ParameterID == "aT")
                        dt.aT = two.Values[0];
                    if (two.ParameterID == "Vy")
                        dt.Vy = two.Values[0];
                    if (two.ParameterID == "Tt")
                        dt.Tt = two.Values[0];
                    if (two.ParameterID == "ZH")
                        dt.ZH = two.Values[0];
                    if (two.ParameterID == "HG")
                        dt.HG = two.Values[0];
                    if (two.ParameterID == "EW")
                        dt.EW = two.Values[0];
                    if (two.ParameterID == "NS")
                        dt.NS = two.Values[0];
                    //"DR","GS","Wy","Wx","Wz","KZB","KCB","ZS","CS","Ny","Nx","Nz",
                    if (two.ParameterID == "DR")
                        dt.DR = two.Values[0];
                    if (two.ParameterID == "GS")
                        dt.GS = two.Values[0];
                    if (two.ParameterID == "Wy")
                        dt.Wy = two.Values[0];
                    if (two.ParameterID == "Wx")
                        dt.Wx = two.Values[0];
                    if (two.ParameterID == "Wz")
                        dt.Wz = two.Values[0];
                    if (two.ParameterID == "KZB")
                        dt.KZB = two.Values[0];
                    if (two.ParameterID == "KCB")
                        dt.KCB = two.Values[0];
                    if (two.ParameterID == "ZS")
                        dt.ZS = two.Values[0];
                    if (two.ParameterID == "CS")
                        dt.CS = two.Values[0];
                    if (two.ParameterID == "Ny")
                        dt.Ny = two.Values[0];
                    if (two.ParameterID == "Nx")
                        dt.Nx = two.Values[0];
                    if (two.ParameterID == "Nz")
                        dt.Nz = two.Values[0];
                    //"Dy","Dx","Dz","T6L","T6R","NHL","NHR",
                    if (two.ParameterID == "Dy")
                        dt.Dy = two.Values[0];
                    if (two.ParameterID == "Dx")
                        dt.Dx = two.Values[0];
                    if (two.ParameterID == "Dz")
                        dt.Dz = two.Values[0];
                    if (two.ParameterID == "T6L")
                        dt.T6L = two.Values[0];
                    if (two.ParameterID == "T6R")
                        dt.T6R = two.Values[0];
                    if (two.ParameterID == "NHL")
                        dt.NHL = two.Values[0];
                    if (two.ParameterID == "NHR")
                        dt.NHR = two.Values[0];
                    //"KG1","KG2","KG3","KG4","KG5","KG6","KG7","KG8","KG9","KG10","KG11","KG12","KG13","KG14","KG15"
                    if (two.ParameterID == "KG1")
                        dt.KG1 = (int)two.Values[0];
                    if (two.ParameterID == "KG2")
                        dt.KG2 = (int)two.Values[0];
                    if (two.ParameterID == "KG3")
                        dt.KG3 = (int)two.Values[0];
                    if (two.ParameterID == "KG4")
                        dt.KG4 = (int)two.Values[0];
                    if (two.ParameterID == "KG5")
                        dt.KG5 = (int)two.Values[0];
                    if (two.ParameterID == "KG6")
                        dt.KG6 = (int)two.Values[0];
                    if (two.ParameterID == "KG7")
                        dt.KG7 = (int)two.Values[0];
                    if (two.ParameterID == "KG8")
                        dt.KG8 = (int)two.Values[0];
                    if (two.ParameterID == "KG9")
                        dt.KG9 = (int)two.Values[0];
                    if (two.ParameterID == "KG10")
                        dt.KG10 = (int)two.Values[0];
                    if (two.ParameterID == "KG11")
                        dt.KG11 = (int)two.Values[0];
                    if (two.ParameterID == "KG12")
                        dt.KG12 = (int)two.Values[0];
                    if (two.ParameterID == "KG13")
                        dt.KG13 = (int)two.Values[0];
                    if (two.ParameterID == "KG14")
                        dt.KG14 = (int)two.Values[0];
                    if (two.ParameterID == "KG15")
                        dt.KG15 = (int)two.Values[0];

                }
            }
        }
    }
}
