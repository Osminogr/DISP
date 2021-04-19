using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [DesignTimeVisible(true)]
    public partial class Vid : ContentView
    {
        public static readonly BindableProperty nameProperty = BindableProperty.Create(nameof(Name), typeof(string), typeof(Vid), string.Empty);
        public string Name
        {
            get => (string)GetValue(Vid.nameProperty);
            set => SetValue(Vid.nameProperty, value);
        }
        public static readonly BindableProperty urlProperty = BindableProperty.Create(nameof(Url), typeof(string), typeof(Vid), string.Empty);
        public string Url
        {
            get => (string)GetValue(Vid.nameProperty);
            set => SetValue(Vid.nameProperty, value);
        }
        public Vid()
        {
            InitializeComponent();
        }
    }
}