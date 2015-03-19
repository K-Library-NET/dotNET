using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AircraftDataAnalysisWinRT.DataModel;
using AircraftDataAnalysisWinRT.MyControl;
using PStudio.WinApp.Aircraft.FDAPlatform.Domain;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using AircraftDataAnalysisWinRT.Services;
using AircraftDataAnalysisWinRT;
using System.Text;
using AircraftDataAnalysisWinRT.Domain;
using System.Threading.Tasks;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace PStudio.WinApp.Aircraft.FDAPlatform
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private FlightDateTreeViewModel m_dateViewModel;
        private FlightAircraftInstanceTreeViewModel m_aircraftInstanceViewModel;

        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 在此页将要在 Frame 中显示时进行调用。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。Parameter
        /// 属性通常用于配置页。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Main start:{0}", DateTime.Now));
            base.OnNavigatedTo(e);
            this.Loaded += MainPage_Loaded;
            this.Unloaded += MainPage_Unloaded;

            this.btShowGrid.DataContext = ApplicationContext.Instance;
            this.borderFault.DataContext = ApplicationContext.Instance;
            this.borderNoFault.DataContext = ApplicationContext.Instance;

            this.ProgressBar1.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.ProgressBar1.IsIndeterminate = true;
            //System.Diagnostics.Debug.WriteLine(string.Format("Main navigated:{0}", DateTime.Now));//DEBUG
            //return;//DEBUG
            Task.Run(async () =>
            {
                try
                {
                    //liangdawen 20131028
                    //Loading方法应该获取最新架次
                    AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentAircraftModel
                       = await ServerHelper.GetCurrentAircraftModelAsync();
                    Task<FlightDataEntitiesRT.FlightParameters> parameters =
                        ApplicationContext.Instance.GetFlightParametersAsync(
                        AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentAircraftModel);

                    var flights = ServerHelper.GetAllFlights(AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentAircraftModel);
                    await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                         new Windows.UI.Core.DispatchedHandler(() =>
                         {
                             //debug: write flightDates
                             //this.DebugWriteFlightDates(flights);

                             this.grdFlights.ItemsSource = flights;
                             this.m_dateViewModel = new FlightDateTreeViewModel(flights);
                             this.m_aircraftInstanceViewModel = new FlightAircraftInstanceTreeViewModel(flights);
                             this.navFlightByDate.DataContext = m_dateViewModel;
                             this.navFlightByDate.ItemsSource = m_dateViewModel.Children;
                             //.ItemsSource = m_dateViewModel.Nodes;
                             this.navFlightByAircraftInstance.DataContext = m_aircraftInstanceViewModel;
                             //.ItemsSource = m_aircraftInstanceViewModel.Nodes;
                             this.ProgressBar1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                             this.ProgressBar1.IsIndeterminate = false;
                             SetCurrentFlight();
                         }));
                }
                catch (Exception exe)
                {
                    LogHelper.Error(exe);
                }
                System.Diagnostics.Debug.WriteLine(string.Format("Main navigated:{0}", DateTime.Now));
            });
        }

        private void DebugWriteFlightDates(FlightDataEntitiesRT.Flight[] flights)
        {
            Random rand = new Random();
            int stepYear = 2011;
            int stepmonth = 1;
            int stepday = 1;
            foreach (var f in flights)
            {
                int year = stepYear + Convert.ToInt32(Math.Round(rand.NextDouble() * 3.0));
                int month = stepmonth + Convert.ToInt32(Math.Round(rand.NextDouble() * 11));
                int day = stepday + Convert.ToInt32(Math.Round(rand.NextDouble() * 27));
                DateTime dct = new DateTime(year, month, day);
                f.FlightDate = dct;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            this.Loaded -= MainPage_Loaded;
            this.Unloaded -= MainPage_Unloaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Main before load:{0}", DateTime.Now));
            SetCurrentFlight();
            System.Diagnostics.Debug.WriteLine(string.Format("Main loaded:{0}", DateTime.Now));
        }

        private void SetCurrentFlight()
        {
            try
            {
                if (this.grdFlights.ItemsSource != null && this.grdFlights.ItemsSource is IEnumerable<FlightDataEntitiesRT.Flight>)
                {
                    var flights = this.grdFlights.ItemsSource as IEnumerable<FlightDataEntitiesRT.Flight>;
                    if (flights != null && flights.Count() > 0 && ApplicationContext.Instance.CurrentFlight == null)
                    {
                        m_switcher = false;
                        this.grdFlights.SelectedIndex = 0;
                        m_switcher = true;
                        var selectedFlightItem = this.grdFlights.SelectedItem as FlightDataEntitiesRT.Flight;
                        this.SetCurrentFlight(selectedFlightItem, this.grdFlights);
                    }
                    else if (flights != null && flights.Count() > 0 && ApplicationContext.Instance.CurrentFlight != null)
                    {
                        var f = flights.FirstOrDefault(new Func<FlightDataEntitiesRT.Flight, bool>(
                            delegate(FlightDataEntitiesRT.Flight flight)
                            {
                                if (flight.FlightID == ApplicationContext.Instance.CurrentFlight.FlightID)
                                    return true;
                                return false;
                            }));

                        m_switcher = false;
                        this.grdFlights.SelectedItem = f;
                        m_switcher = true;
                        this.SetCurrentFlight(f, this.grdFlights);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void SetCurrentFlight(FlightDataEntitiesRT.Flight flight, object sender)
        {
            this.m_switcher = false;
            AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentFlight = flight;
            if (sender != null)
            {
                if (sender != this.grdFlights)
                {
                    this.grdFlights.SelectedItem = flight;
                }
                if (sender != this.navFlightByDate)
                {
                    IFlightTreeNode node = this.m_dateViewModel.FindNodeByFlight(flight);
                    this.navFlightByDate.SelectedItem = node;
                }
                if (sender != this.navFlightByAircraftInstance)
                {
                    IFlightTreeNode node = this.m_aircraftInstanceViewModel.FindNodeByFlight(flight);
                    this.navFlightByAircraftInstance.SelectedItem = node;
                }
            }

            if (AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentFlight.GlobeDatas == null
                || AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentFlight.GlobeDatas.Length == 0)
            {
                AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentFlight.GlobeDatas = ServerHelper.GetFlightGlobeDatas(
                    AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentAircraftModel,
                    AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentFlight.FlightID,
                    AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentFlight.EndSecond);
            }

            this.scMap.CurrentFlight = flight;//this.grdFlights.SelectedItem as FlightDataEntitiesRT.Flight;
            this.m_switcher = true;
        }

        void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void btHistory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btStatReport_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StatReport), "StatReport");
        }

        private void btTrendAnalysis_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TrendAnalysis), "TrendAnalysis");
        }

        private void btEngineMonitoring_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EngineMonitoring), "EngineMonitoring");
        }

        private void btExtremumReport_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ExtremumReport), "ExtremumReport");
        }

        private void btFaultDiagnosis_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FaultDiagnosis), "FaultDiagnosis");
        }

        private void btFlightAnalysis_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(FlightAnalysisSub),
            //new FlightAnalysisSubNavigateParameter()
            //{
            //    HostParameterID = "Hp",
            //    CurrentStartSecond = ApplicationContext.Instance.CurrentFlight.StartSecond,
            //    CurrentEndSecond = ApplicationContext.Instance.CurrentFlight.EndSecond,

            //});
            this.Frame.Navigate(typeof(FlightAnalysis), "FlightAnalysis");
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(FlightAnalysisSubLite), // new ExtremumReportSubEditChartNavigationParameter()
            //    new SubEditChartNavigationParameter()
            //{
            //    DataLoader = new AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader()
            //    {
            //        CurrentAircraftModel =
            //            ApplicationContext.Instance.CurrentAircraftModel,
            //        CurrentFlight = ApplicationContext.Instance.CurrentFlight,
            //    },
            //    HostParameterID = "Nx",
            //    HostParameterYAxis = FlightAnalysisSubViewYAxis.LeftYAxis,
            //    RelatedParameterIDs = new SubEditChartNavigationParameter.RelatedParameterInfo[]
            //    {
            //         new SubEditChartNavigationParameter.RelatedParameterInfo(){ RelatedParameterID = "T6L",
            //              YAxis = FlightAnalysisSubViewYAxis.RightYAxis},
            //         new SubEditChartNavigationParameter.RelatedParameterInfo(){ RelatedParameterID = "T6R",
            //             YAxis = FlightAnalysisSubViewYAxis.RightYAxis},
            //    }
            //    //MaxValueSecond = 1000,
            //    //MinValueSecond = 2000,
            //});

            //this.Frame.Navigate(typeof(AddFilePage));

            //debug:
            this.Frame.Navigate(typeof(AircraftDataAnalysisModel1.WinRT.DeleteFlightConfirm));
        }

        private void btSelect_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ImportAircraftBatchConfirm), null);
        }

        private async void btImport_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            openPicker.FileTypeFilter.Add(".phy");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                //20131029 liangdawen:
                var aircraftModel = AircraftDataAnalysisWinRT.ApplicationContext.Instance.CurrentAircraftModel;
                var flightParameter = AircraftDataAnalysisWinRT.ApplicationContext.Instance.GetFlightParameters(
                    ApplicationContext.Instance.CurrentAircraftModel);
                FlightDataEntitiesRT.IFlightRawDataExtractor extractor = null;
                FlightDataEntitiesRT.Flight currentFlight = null;

                bool correct = CreateTempCurrentFlight(file, aircraftModel, flightParameter, ref extractor, ref currentFlight);

                AddFileViewModel model = new AddFileViewModel(currentFlight, file, extractor,
                    aircraftModel, flightParameter);
                model.IsTempFlightParseError = !correct;

                this.Frame.Navigate(typeof(AircraftDataAnalysisWinRT.Domain.ImportLoadConfirmPage), model);
                //20131029 liangdawen
                //this.SetLoading(true);
                //await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                //     new Windows.UI.Core.DispatchedHandler(
                //     delegate()
                //     {
                //         var t = System.Threading.Tasks.Task.Delay(5000);
                //         t.Wait();
                //         //debug: 模拟需要时间
                //         model.InitLoadHeader();
                //     }));

                //this.SetLoading(false);
                //ImportAircraftConfirm confirm = new ImportAircraftConfirm();
                //confirm.Show(dialogArea, model);
            }
            else
            {
                //this.ViewModel = null;
                //this.DataContext = null;
            }
        }

        /// <summary>
        /// 临时的方法，多个机型后要考虑使用别的方式创建，例如反射
        /// </summary>
        /// <param name="file"></param>
        /// <param name="aircraftModel"></param>
        /// <param name="flightParameter"></param>
        /// <param name="extractor"></param>
        /// <param name="currentFlight"></param>
        private bool CreateTempCurrentFlight(StorageFile file, FlightDataEntitiesRT.AircraftModel aircraftModel,
            FlightDataEntitiesRT.FlightParameters flightParameter, ref FlightDataEntitiesRT.IFlightRawDataExtractor extractor,
            ref FlightDataEntitiesRT.Flight currentFlight)
        {
            return BuildTempFlightByRule(file, aircraftModel, flightParameter, ref extractor, ref currentFlight);
        }

        public static bool BuildTempFlightByRule(StorageFile file, FlightDataEntitiesRT.AircraftModel aircraftModel, FlightDataEntitiesRT.FlightParameters flightParameter, ref FlightDataEntitiesRT.IFlightRawDataExtractor extractor, ref FlightDataEntitiesRT.Flight currentFlight)
        {
            bool correct = true;

            if (aircraftModel != null && !string.IsNullOrEmpty(aircraftModel.ModelName))
            {
                //if (aircraftModel.ModelName == "F4D")
                //{
                var result = FlightDataReading.AircraftModel1.FlightRawDataExtractorFactory
                    .CreateFlightRawDataExtractor(file, flightParameter);
                extractor = result;
                //}
            }

            var aircraft = new FlightDataEntitiesRT.AircraftInstance()
            {
                AircraftModel = aircraftModel,
                LastUsed = DateTime.Now
            };
            try
            {
                var aircraftNumber = (extractor as FlightDataReading.AircraftModel1.FlightDataReadingHandler).ParseAircraftNumber(file.Name);
                aircraft.AircraftNumber = aircraftNumber;
            }
            catch
            {
                correct = false;
            }

            currentFlight = new FlightDataEntitiesRT.Flight()
            {
                Aircraft = aircraft,
                StartSecond = 0,
                FlightName = file.Name,
                FlightID = RemoveFlightPHYFileIllegalChars(file.DisplayName)
            };

            try
            {
                currentFlight.FlightDate = (extractor as FlightDataReading.AircraftModel1.FlightDataReadingHandler).ParseDate(file.Name);
            }
            catch
            {
                correct = false;
            }
            return correct;
        }

        private static string RemoveFlightPHYFileIllegalChars(string p)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in p)
            {
                if (char.IsNumber(c) || c == '-')
                    builder.Append(c);
                else break;
            }
            return builder.ToString();
        }

        private void OnCurrentFlightChanged(object sender, SelectionChangedEventArgs e)
        {//防止消息循环
            if (m_switcher == false)
                return;

            if (this.grdFlights.SelectedItem != null && this.grdFlights.SelectedItem is FlightDataEntitiesRT.Flight)
            {
                this.SetCurrentFlight(this.grdFlights.SelectedItem as FlightDataEntitiesRT.Flight, this.grdFlights);
            }
        }

        private void dialogArea_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Double Tapped:+" + sender.GetHashCode().ToString());
        }

        private void OnCurrentFlightChanged2(object sender, SelectionChangedEventArgs e)
        {//防止消息循环
            if (m_switcher == false)
                return;

            if (this.navFlightByDate.SelectedItem != null && this.navFlightByDate.SelectedItem is FlightViewNode)
            {
                this.SetCurrentFlight((this.navFlightByDate.SelectedItem as FlightViewNode).Flight, this.navFlightByDate);
            }
        }

        private void OnCurrentFlightChanged3(object sender, SelectionChangedEventArgs e)
        {//防止消息循环
            if (m_switcher == false)
                return;

            if (this.navFlightByAircraftInstance.SelectedItem != null && this.navFlightByAircraftInstance.SelectedItem is FlightViewNode)
            {
                this.SetCurrentFlight((this.navFlightByAircraftInstance.SelectedItem as FlightViewNode).Flight, this.navFlightByAircraftInstance);
            }
        }

        private bool m_switcher = true;

        private void btShowGridClick_Click(object sender, RoutedEventArgs e)
        {
            if (ApplicationContext.Instance.CurrentFlight != null)
            {
                this.Frame.Navigate(typeof(AircraftDataAnalysisWinRT.MyControl.GridDataPage),
                    new GridDataDisplayArg()
                    {
                        EndSecond = ApplicationContext.Instance.CurrentFlight.EndSecond,
                        DataLoader = new AircraftDataAnalysisModel1.WinRT.Domain.AircraftAnalysisDataLoader()
                        {
                            CurrentAircraftModel = ApplicationContext.Instance.CurrentAircraftModel,
                            CurrentFlight = ApplicationContext.Instance.CurrentFlight
                        },
                        ParameterIDs = ApplicationContext.Instance.GetFlightParameters(
                        ApplicationContext.Instance.CurrentAircraftModel).ToParameterIDs(),
                        StartSecond = 0
                    });
            }

        }
    }
}
