using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MobileMarketDemo.Win
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.webBrowser2 = new Gecko.GeckoWebBrowser();
            this.tabPageGecko.Controls.Add(this.webBrowser2);
            // 
            // webBrowser2
            //
            this.webBrowser2.AllowDrop = false;
            //this.webBrowser2.AllowWebBrowserDrop = false;
            this.webBrowser2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser2.NoDefaultContextMenu = true;
            this.webBrowser2.CreateWindow2 += webBrowser2_CreateWindow2;
            //this.webBrowser2.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser2.Location = new System.Drawing.Point(3, 3);
            this.webBrowser2.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser2.Name = "webBrowser2";
            //this.webBrowser2.ScriptErrorsSuppressed = true;
            this.webBrowser2.Size = new System.Drawing.Size(684, 341);
            this.webBrowser2.TabIndex = 0;
        }

        void webBrowser2_CreateWindow2(object sender, Gecko.GeckoCreateWindow2EventArgs e)
        {
            e.Cancel = true;
            if (!string.IsNullOrEmpty(e.Uri))
            {
                Uri uri = new Uri(e.Uri);
                if (uri.AbsoluteUri.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase)
                    || uri.AbsoluteUri.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
                    this.Navigate(e.Uri);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Navigate(this.textBox1.Text);
        }

        private void Navigate(string urlText)
        {
            if (this.tabControl1.SelectedIndex == 0)
                this.webBrowser1.Navigate(urlText);
            else
                this.webBrowser2.Navigate(urlText);

            this.OnNavigationHistoryChanged();
        }

        private void OnNavigationHistoryChanged()
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                if (this.webBrowser1.CanGoBack)
                    this.button1.Enabled = true;
                else this.button1.Enabled = false;

                if (this.webBrowser1.CanGoForward)
                    this.button2.Enabled = true;
                else this.button2.Enabled = false;
            }
            else
            {
                if (this.webBrowser2.CanGoBack)
                    this.button1.Enabled = true;
                else this.button1.Enabled = false;

                if (this.webBrowser2.CanGoForward)
                    this.button2.Enabled = true;
                else this.button2.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;

            this.webBrowser1.Navigate(this.textBox1.Text);
            this.webBrowser2.Navigate(this.textBox1.Text);

            this.webBrowser1.Navigated += webBrowser1_Navigated;
            this.webBrowser2.Navigated += webBrowser2_Navigated;
        }

        void webBrowser2_Navigated(object sender, Gecko.GeckoNavigatedEventArgs e)
        {
            this.OnNavigationHistoryChanged();
        }

        void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            this.OnNavigationHistoryChanged();
        }

        void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnNavigationHistoryChanged();
            //throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Back();
        }

        private void Back()
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                this.webBrowser1.GoBack();
            }
            else
            {
                this.webBrowser2.GoBack();
            }

            this.OnNavigationHistoryChanged();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Forward();
        }

        private void Forward()
        {
            if (this.tabControl1.SelectedIndex == 0)
                this.webBrowser1.GoForward();
            else
                this.webBrowser2.GoForward();

            this.OnNavigationHistoryChanged();
        }
    }
}
