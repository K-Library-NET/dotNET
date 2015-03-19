using AircraftDataAnalysisWinRT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT
{
    public class ApplicationContext : System.ComponentModel.INotifyPropertyChanged
    {
        private ApplicationContext()
        {
            //使用云端IIS服务，去掉下面两行注释
            //this.DataInputServiceURL = "http://42.96.198.241/AircraftDataAnalysisWcfService/DataInputService.svc";
            //this.AircraftServiceURL = "http://42.96.198.241/AircraftDataAnalysisWcfService/AircraftService.svc";

            //使用本机IIS服务（不是VS调试器），去掉下面两行注释
            this.DataInputServiceURL = "http://localhost/AircraftDataAnalysisWcfService/DataInputService.svc";
            this.AircraftServiceURL = "http://localhost/AircraftDataAnalysisWcfService/AircraftService.svc";

            //上面四行全部注释，那就是使用VS调试器

            this.Init();
        }

        private void Init()
        {
            string value = ServerHelper.GetAppConfigValue("IsShowGridEnable", this.AircraftServiceURL);
            if (value != null && value.Equals(bool.TrueString, StringComparison.CurrentCultureIgnoreCase))
            {
                m_isShowGridEnable = true;
            }
            else { m_isShowGridEnable = false; }
        }

        public string AircraftServiceURL { get; set; }

        public string DataInputServiceURL { get; set; }

        private static ApplicationContext m_instance;
        public static ApplicationContext Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new ApplicationContext();
                return m_instance;
            }
        }

        private FlightDataEntitiesRT.Flight m_currentFlight = null;

        public FlightDataEntitiesRT.Flight CurrentFlight
        {
            get
            {
                return m_currentFlight;
            }
            set
            {
                m_currentFlight = value;

                if (this.m_currentFlight != null)
                {
                    var rec = ServerHelper.GetDecisionRecords(m_currentFlight);
                    if (rec != null && rec.Count() > 0)
                    {
                        m_isCurrentFlightHasFault = true;
                    }
                    else
                    {
                        m_isCurrentFlightHasFault = false;
                    }
                }
                else
                {
                    m_isCurrentFlightHasFault = false;
                }

                this.OnPropertyChanged("CurrentFlight");
                this.OnPropertyChanged("IsCurrentFlightNoFault");
                this.OnPropertyChanged("IsCurrentFlightFault");
            }
        }

        private void OnPropertyChanged(string prop)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(prop));
        }

        public FlightDataEntitiesRT.AircraftModel CurrentAircraftModel { get; set; }

        private bool m_isShowGridEnable = false;

        public bool IsShowGridEnable
        {
            get
            {
                return m_isShowGridEnable;
            }
        }

        public bool IsCurrentFlightNoFault
        {
            get
            {
                return !IsCurrentFlightFault;
            }
        }

        private bool m_isCurrentFlightHasFault = false;

        public bool IsCurrentFlightFault
        {
            get
            {
                return m_isCurrentFlightHasFault;
            }
        }

        private Dictionary<string, object> m_objectMap = new Dictionary<string, object>();

        internal IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel> GetChartPanels(FlightDataEntitiesRT.AircraftModel aircraftModel)
        {
            if (this.m_objectMap.ContainsKey("AircraftModel_ChartPanel:" + aircraftModel.ModelName)
                && this.m_objectMap["AircraftModel_ChartPanel:" + aircraftModel.ModelName] != null)
            {
                return m_objectMap["AircraftModel_ChartPanel:" + aircraftModel.ModelName] as IEnumerable<FlightDataEntitiesRT.Charts.ChartPanel>;
            }
            else
            {
                var panels = ServerHelper.GetChartPanels(aircraftModel);
                m_objectMap.Add("AircraftModel_ChartPanel:" + aircraftModel.ModelName, panels);
                return panels;
            }
        }

        internal FlightDataEntitiesRT.FlightParameters GetFlightParameters(
            FlightDataEntitiesRT.AircraftModel aircraftModel)
        {
            if (this.m_objectMap.ContainsKey("AircraftModel_FlightParameters:" + aircraftModel.ModelName)
                && this.m_objectMap["AircraftModel_FlightParameters:" + aircraftModel.ModelName] != null)
            {
                return m_objectMap["AircraftModel_FlightParameters:" + aircraftModel.ModelName] as FlightDataEntitiesRT.FlightParameters;
            }
            else
            {
                var parameters = ServerHelper.GetFlightParameters(aircraftModel);
                m_objectMap.Add("AircraftModel_FlightParameters:" + aircraftModel.ModelName, parameters);
                return parameters;
            }
        }

        public Task<FlightDataEntitiesRT.FlightParameters> GetFlightParametersAsync(
            FlightDataEntitiesRT.AircraftModel aircraftModel)
        {
            if (this.m_objectMap.ContainsKey("AircraftModel_FlightParameters:" + aircraftModel.ModelName)
                && this.m_objectMap["AircraftModel_FlightParameters:" + aircraftModel.ModelName] != null)
            {
                return Task.Run<FlightDataEntitiesRT.FlightParameters>(
                      new Func<FlightDataEntitiesRT.FlightParameters>(() =>
                      {
                          if (this.m_objectMap.ContainsKey("AircraftModel_FlightParameters:" + aircraftModel.ModelName)
                              && this.m_objectMap["AircraftModel_FlightParameters:" + aircraftModel.ModelName] != null)
                          {
                              return m_objectMap["AircraftModel_FlightParameters:" + aircraftModel.ModelName] as FlightDataEntitiesRT.FlightParameters;
                          }
                          return null;
                      }));
                //  return m_objectMap["AircraftModel_FlightParameters:" + aircraftModel.ModelName] as FlightDataEntitiesRT.FlightParameters;
            }
            else
            {
                Task<FlightDataEntitiesRT.FlightParameters> parameters = ServerHelper.GetFlightParametersAsync(
                    AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentAircraftModel);
                parameters.ContinueWith(new Action<Task<FlightDataEntitiesRT.FlightParameters>>(
                    delegate(Task<FlightDataEntitiesRT.FlightParameters> t)
                    {
                        if (!m_objectMap.ContainsKey("AircraftModel_FlightParameters:" + aircraftModel.ModelName))
                        {
                            m_objectMap.Add("AircraftModel_FlightParameters:" + aircraftModel.ModelName, t.Result);
                        }
                    }));
                return parameters;
            }
        }

        internal IEnumerable<FlightDataEntitiesRT.Decisions.Decision> GetDecisions(FlightDataEntitiesRT.AircraftModel aircraftModel)
        {
            if (this.m_objectMap.ContainsKey("AircraftModel_Decisions:" + aircraftModel.ModelName)
                && this.m_objectMap["AircraftModel_Decisions:" + aircraftModel.ModelName] != null)
            {
                return m_objectMap["AircraftModel_Decisions:" + aircraftModel.ModelName] as IEnumerable<FlightDataEntitiesRT.Decisions.Decision>;
            }
            else
            {
                var decisions = ServerHelper.GetDecisions(aircraftModel);
                m_objectMap.Add("AircraftModel_Decisions:" + aircraftModel.ModelName, decisions);
                return decisions;
            }

        }


        internal string GetFlightParameterCaption(string p)
        {
            var parames = this.GetFlightParameters(CurrentAircraftModel);
            var result = from one in parames.Parameters
                         where one.ParameterID == p
                         select one.Caption;

            if (result != null && result.Count() > 0)
                return result.First();

            return p;
        }

        public Domain.FlightAnalysisViewModelOld GetViewModelByCurrentFlight()
        {
            return GetCurrentViewModel(CurrentFlight);
        }

        public Domain.FlightAnalysisViewModelOld GetCurrentViewModel(FlightDataEntitiesRT.Flight flight)
        {
            if (flight == null || string.IsNullOrEmpty(flight.FlightID))
            {
                return null;
            }

            string key = this.GetFlightViewModelKey(flight.FlightID);
            return GetCurrentViewModel(key);
        }

        public Domain.FlightAnalysisViewModelOld GetCurrentViewModel(string key)
        {
            if (this.m_objectMap.ContainsKey(key)
                && this.m_objectMap[key] != null
                && this.m_objectMap[key] is Domain.FlightAnalysisViewModelOld)
            {
                return this.m_objectMap[key] as Domain.FlightAnalysisViewModelOld;
            }

            return null;
        }

        public void SetCurrentViewModel(FlightDataEntitiesRT.Flight flight,
            AircraftDataAnalysisWinRT.Domain.FlightAnalysisViewModelOld viewModel)
        {
            if (flight == null || string.IsNullOrEmpty(flight.FlightID))
            {
                return;
            }

            SetCurrentViewModel(flight.FlightID, viewModel);
        }

        public void SetCurrentViewModel(string flightID,
            AircraftDataAnalysisWinRT.Domain.FlightAnalysisViewModelOld viewModel)
        {
            string key = this.GetFlightViewModelKey(flightID);
            if (this.m_objectMap.ContainsKey(key))
            {
                this.m_objectMap[key] = viewModel;
            }
            else
            {
                this.m_objectMap.Add(key, viewModel);
            }
        }

        private string GetFlightViewModelKey(string flightID)
        {
            return string.Format("FlightViewModel: {0}", flightID);
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
