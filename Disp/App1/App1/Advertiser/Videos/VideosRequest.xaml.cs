using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using App1.Domain;
using App1.Utils;
using App1.Templates;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace App1.Advertiser
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideosRequest : ContentPage
    {
        Adv nowUser;
        VideoRequest videoRequest;
        public VideosRequest(Adv now)
        {
            nowUser = now;
            InitializeComponent();

            BindingContext = this;

            videoRequest = new VideoRequest();
            videoRequest.adv = nowUser;
            videoRequest.photos = new List<Stream>();

            OverrideTitleView("Заказать", "Готово", 85, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(() =>
            {
                videoRequest.text = editorText.Text;
                Navigation.PopAsync();
            })));
        }

        public async void AddPhoto(object sender, EventArgs e)
        {
            string number = (string)((Button)sender).CommandParameter;

            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                List<MediaFile> photo = await CrossMedia.Current.PickPhotosAsync(null, new MultiPickerOptions() { MaximumImagesCount = 1 }, default);

                if (photo != null && photo.Count == 1)
                {
                    videoRequest.photos.Add(photo[0].GetStream());

                    if (number == "0")
                    {
                        photo2.IsEnabled = true;
                        photo2.ImageSource = "photoactive.png";

                        photo1.ImageSource = photo[0].Path;
                    }

                    if (number == "1")
                    {
                        photo3.IsEnabled = true;
                        photo3.ImageSource = "photoactive.png";

                        photo2.ImageSource = photo[0].Path;
                    }

                    if (number == "2")
                    {
                        photo3.ImageSource = photo[0].Path;
                    }
                }
            }
        }
    }
}