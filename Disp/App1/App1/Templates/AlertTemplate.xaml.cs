using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Advertiser.Campaign;

namespace App1.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class AlertTemplate: ContentView
    {
        public AlertTemplate(Alert alert)
        {
            InitializeComponent();

            title.Text = alert.title;
            header.Text = alert.header;
            text.Text = alert.text;
            date.Text = alert.date;
        }
    }
}