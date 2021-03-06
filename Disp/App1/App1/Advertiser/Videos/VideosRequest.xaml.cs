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
using System.Threading;

namespace App1.Advertiser
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideosRequest : ContentPage
    {
        Adv nowUser;
        VideoRequest videoRequest;
        bool photo1Set = false;
        bool photo2Set = false;
        bool photo3Set = false;
        public VideosRequest(Adv now)
        {
            nowUser = now;
            InitializeComponent();

            BindingContext = this;

            videoRequest = new VideoRequest();
            videoRequest.adv = nowUser;
            videoRequest.photos = new List<Photo>();

            OverrideTitleView("Заказать", "Готово", 85, -1);

            photo1.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    AddPhoto(0);
                })
            });

            photo2.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    AddPhoto(1);
                })
            });

            photo3.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    AddPhoto(2);
                })
            });
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                videoRequest.text = editorText.Text;

                ShowLoading();

                try
                {
                    var answer = await Server.AddVideoRequest(videoRequest);

                    if (answer != null)
                    {
                        await DisplayAlert("Сообщение", "Заявка на создание видеоролика отправлена! Ожидайте ответа от администратора.", "Закрыть");

                        Thread.Sleep(1000);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        Thread.Sleep(1000);
                        HideLoading();

                        await DisplayAlert("Сообщение", "Не удалось выполнить отправку заявки! Попробуйте позже.", "Закрыть");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);

                    Thread.Sleep(1000);
                    HideLoading();

                    await DisplayAlert("Сообщение", "Не удалось выполнить отправку заявки! Попробуйте позже.", "Закрыть");
                }
            })));
        }

        private void ShowLoading()
        {
            actInd.IsVisible = true;
            actInd.IsRunning = true;
            gridRoot.Opacity = 0.3;
            gridRoot.IsEnabled = false;
        }

        private void HideLoading()
        {
            actInd.IsVisible = false;
            actInd.IsRunning = false;
            gridRoot.Opacity = 1;
            gridRoot.IsEnabled = true;
        }

        public async void AddPhoto(int type)
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.PickPhotoAsync();

                if (photo != null)
                {
                    videoRequest.photos.Add(new Photo() { name = Path.GetFileName(photo.Path), data = photo.GetStream() });

                    if (type == 0)
                    {
                        photo1Set = true;
                        if (photo1Set && !photo2Set)
                        {
                            photo2.IsEnabled = true;
                            photo2.Source = "photoactive.png";
                        }

                        photo1.Source = photo.Path;
                    }

                    if (type == 1)
                    {
                        photo2Set = true;
                        if (photo2Set && !photo3Set)
                        {
                            photo3.IsEnabled = true;
                            photo3.Source = "photoactive.png";
                        }

                        photo2.Source = photo.Path;
                    }

                    if (type == 2)
                    {
                        photo3Set = true;
                        photo3.Source = photo.Path;
                    }
                }
            }
        }
    }
}