using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PStudio.WinApp.Aircraft.FDAPlatform.Domain;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace PStudio.WinApp.Aircraft.FDAPlatform
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
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
            this.Frame.Navigate(typeof(FlightAnalysis), "FlightAnalysis");
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btSelect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btImport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
