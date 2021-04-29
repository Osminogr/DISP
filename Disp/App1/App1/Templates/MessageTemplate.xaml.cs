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
    public partial class MessageTemplate: ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
                                                              "text",
                                                              typeof(string),
                                                              typeof(MessageTemplate),
                                                              string.Empty);

        public string text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty TimeProperty = BindableProperty.Create(
                                                              "time",
                                                              typeof(string),
                                                              typeof(MessageTemplate),
                                                              string.Empty);

        public string time
        {
            get => (string)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }

        public static readonly BindableProperty OwnerNameProperty = BindableProperty.Create(
                                                              "owner",
                                                              typeof(string),
                                                              typeof(MessageTemplate),
                                                              string.Empty);

        public string owner
        {
            get => (string)GetValue(OwnerNameProperty);
            set => SetValue(OwnerNameProperty, value);
        }

        public static readonly BindableProperty StatusProperty = BindableProperty.Create(
                                                              "status",
                                                              typeof(string),
                                                              typeof(MessageTemplate),
                                                              string.Empty);

        public string status
        {
            get => (string)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        public MessageTemplate(Message message)
        {
            text = message.text;
            time = message.time;
            owner = message.owner.name;
            status = message.status.ToString();

            InitializeComponent();

            if (message.owner.id == message.receiver.id)
            {
                MyMessage.IsVisible = true;
                OtherMessage.IsVisible = false;

                if (status == "1")
                {
                    MyImage.Source = "senddef.png";
                }
                if (status == "2")
                {
                    MyImage.Source = "sendcomplete.png";
                }
            }
            else
            {
                MyMessage.IsVisible = false;
                OtherMessage.IsVisible = true;

                if (status == "1")
                {
                    OtherImage.Source = "senddef.png";
                }
                if (status == "2")
                {
                    OtherImage.Source = "sendcomplete.png";
                }
            }
        }
    }
}