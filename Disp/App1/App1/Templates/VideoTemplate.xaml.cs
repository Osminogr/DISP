using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class VideoTemplate: ContentView
    {
        public static readonly BindableProperty nameProperty = BindableProperty.Create(nameof(Name), typeof(string), typeof(VideoTemplate), string.Empty);
        public string Name
        {
            get => (string)GetValue(VideoTemplate.nameProperty);
            set => SetValue(VideoTemplate.nameProperty, value);
        }
        public static readonly BindableProperty urlProperty = BindableProperty.Create(nameof(Url), typeof(string), typeof(VideoTemplate), string.Empty);
        public string Url
        {
            get => (string)GetValue(VideoTemplate.urlProperty);
            set => SetValue(VideoTemplate.urlProperty, value);
        }
        public VideoTemplate()
        {
            InitializeComponent();
        }
    }
}