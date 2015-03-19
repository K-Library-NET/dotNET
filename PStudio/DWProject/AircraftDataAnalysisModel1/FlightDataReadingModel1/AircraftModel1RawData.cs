using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataReading.AircraftModel1
{
    public class AircraftModel1RawData // : ModelRawData
    {
        public AircraftModel1RawData()
        {
            //this.Fields = new string[] {
            //    "Second", "Et","Hp","Vi","M","aT","Vy","Tt","ZH","HG","FY","EW","NS",
            //    "DR","GS","Wy","Wx","Wz","KZB","KCB","ZS","CS","Ny","Nx","Nz",
            //    "Dy","Dx","Dz","T6L","T6R","NHL","NHR",
            //    "KG1","KG2","KG3","KG4","KG5","KG6","KG7","KG8","KG9","KG10","KG11","KG12","KG13","KG14","KG15"
            //};
        } 

        public double Second
       // public override double Second
        {
            get;
            set;
        }

        public double Et
        {
            get;
            set;
        }

        public double Hp
        {
            get;
            set;
        }

        public double Vi
        {
            get;
            set;
        }

        public double M
        {
            get;
            set;
        }
        public double aT { get; set; }
        public double Vy { get; set; }
        public double Tt { get; set; }
        public double ZH { get; set; }
        public double HG { get; set; }
        public double FY { get; set; }
        public double EW { get; set; }
        public double NS { get; set; }
        public double DR { get; set; }
        public double GS { get; set; }
        public double Wy { get; set; }
        public double Wx { get; set; }
        public double Wz { get; set; }
        public double KZB { get; set; }
        public double KCB { get; set; }
        public double ZS { get; set; }
        public double CS { get; set; }
        public double Ny { get; set; }
        public double Nx { get; set; }
        public double Nz { get; set; }
        public double Dx { get; set; }
        public double Dy { get; set; }
        public double Dz { get; set; }
        public double T6L { get; set; }
        public double T6R { get; set; }
        public double NHL { get; set; }
        public double NHR { get; set; }

        public int KG1
        {
            get;
            set;
        }

        public int KG2
        {
            get;
            set;
        }

        public int KG3
        {
            get;
            set;
        }

        public int KG4
        {
            get;
            set;
        }

        public int KG5
        {
            get;
            set;
        }

        public int KG6
        {
            get;
            set;
        }

        public int KG7
        {
            get;
            set;
        }

        public int KG8
        {
            get;
            set;
        }

        public int KG9
        {
            get;
            set;
        }

        public int KG10
        {
            get;
            set;
        }

        public int KG11
        {
            get;
            set;
        }

        public int KG12
        {
            get;
            set;
        }

        public int KG13
        {
            get;
            set;
        }

        public int KG14
        {
            get;
            set;
        }

        public int KG15
        {
            get;
            set;
        }
    }
}
