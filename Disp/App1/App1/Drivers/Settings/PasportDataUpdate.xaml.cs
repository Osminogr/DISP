
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using System.Net.Http;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Collections.Generic;

namespace App1.Drivers.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasportDataUpdate : ContentPage
    {
        Driver nowUser;
        public PasportDataUpdate(Driver now)
        {
            nowUser = now;
            InitializeComponent();

            number.Text = nowUser.person.passport.number;
            Data.Text = nowUser.person.passport.date;
            Org.Text = nowUser.person.passport.who;
            Code.Text = nowUser.person.passport.code;

            photo1.Source = nowUser.person.passport.urlPhoto1;
            photo2.Source = nowUser.person.passport.urlPhoto2;
            photo3.Source = nowUser.person.passport.urlPhoto3;

            photo1.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (CrossMedia.Current.IsPickPhotoSupported)
                    {
                        MediaFile photo = await CrossMedia.Current.PickPhotoAsync();

                        if (photo != null)
                        {
                            photo1.Source = photo.Path;
                            nowUser.person.passport.photo1 = new Photo();
                            nowUser.person.passport.photo1.data = photo.GetStream();
                            nowUser.person.passport.photo1.name = Path.GetFileName(photo.Path);
                        }
                    }
                })
            });

            photo2.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (CrossMedia.Current.IsTakePhotoSupported)
                    {
                        MediaFile photo = await CrossMedia.Current.PickPhotoAsync();

                        if (photo != null)
                        {
                            photo2.Source = photo.Path;
                            nowUser.person.passport.photo2 = new Photo();
                            nowUser.person.passport.photo2.data = photo.GetStream();
                            nowUser.person.passport.photo2.name = Path.GetFileName(photo.Path);
                        }
                    }
                })
            });

            photo3.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (CrossMedia.Current.IsTakePhotoSupported)
                    {
                        MediaFile photo = await CrossMedia.Current.PickPhotoAsync();

                        if (photo != null)
                        {
                            photo3.Source = photo.Path;
                            nowUser.person.passport.photo3 = new Photo();
                            nowUser.person.passport.photo3.data = photo.GetStream();
                            nowUser.person.passport.photo3.name = Path.GetFileName(photo.Path);
                        }
                    }
                })
            });

            OverrideTitleView("Паспортные данные", "Сохранить", 40, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false;
                if (number.Text != null) b1 = true;
                if (Data.Text != null) b2 = true;
                if (Org.Text != null) b3 = true;
                if (Code.Text != null) b4 = true;

                if (b1 && b2 && b3 && b4)
                {
                    nowUser.person.passport.number = number.Text;
                    nowUser.person.passport.date = Data.Text;
                    nowUser.person.passport.who = Org.Text;
                    nowUser.person.passport.code = Code.Text;

                    HttpContent answer = await Server.SavePassport(nowUser.person.passport);
                    string response = await answer.ReadAsStringAsync();

                    if (response == null || (response != null && !response.Contains(nameof(Passport))))
                    {
                        await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                    }
                    else
                    {
                        Server.SaveAuthObject(nowUser, false);
                        await Navigation.PopAsync(true);
                    }
                }
                else
                {
                    await DisplayAlert("Сообщение", "Не все поля заполнены корректно!", "Закрыть");
                }
            })));
        }
    }
}