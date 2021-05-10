using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class VideoTemplate : ContentView
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

        private readonly SynchronizationContext _context;

        public VideoTemplate()
        {
            InitializeComponent();

            _context = SynchronizationContext.Current;
            Thread t = new Thread(new ThreadStart(AutoPlayVp));
            t.Start();
        }

        private void AutoPlayVp()
        {
            Thread.Sleep(1000);

            _context.Send(status => vp.Seek(1), null);
            _context.Send(status => vp.Seek(-1), null);
            _context.Send(status => videoLoading.IsVisible = false, null);
            _context.Send(status => vp.Opacity = 1, null);
        }
    }
}