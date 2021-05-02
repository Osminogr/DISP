
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Collections.Generic;
using System;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPasp : ContentPage
    {
        Driver driver;
        public RegisterPasp(Driver now)
        {
            driver = now;
            InitializeComponent();

            OverrideTitleView("Регистрация", "Дальше", 80, -1);

            driver.person.passport = new Passport();

            photo1.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (CrossMedia.Current.IsTakePhotoSupported)
                    {
                        MediaFile photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions() {
                            SaveToAlbum = true,
                            Name = "photo" + DateTime.Now + ".jpg"
                        });

                        if (photo != null)
                        {
                            photo1.Source = photo.Path;
                            driver.person.passport.photo1 = new Photo();
                            driver.person.passport.photo1.data = photo.GetStream();
                        }
                    }
                })
            });

            photo2.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (CrossMedia.Current.IsPickPhotoSupported)
                    {
                        MediaFile photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions(), default);

                        if (photo != null)
                        {
                            photo2.Source = photo.Path;
                            driver.person.passport.photo2 = new Photo();
                            driver.person.passport.photo2.data = photo.GetStream();
                        }
                    }
                })
            });

            photo3.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (CrossMedia.Current.IsPickPhotoSupported)
                    {
                        MediaFile photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions(), default);

                        if (photo != null)
                        {
                            photo3.Source = photo.Path;
                            driver.person.passport.photo3 = new Photo();
                            driver.person.passport.photo3.data = photo.GetStream();
                        }
                    }
                })
            });
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(() =>
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false, b5 = false;
                if (number.Text != null)
                {
                    b1 = true;
                    driver.person.passport.number = number.Text;
                }
                if (Data.Text != null)
                {
                    b2 = true;
                    driver.person.passport.date = Data.Text;
                }
                if (Org.Text != null)
                {
                    b3 = true;
                    driver.person.passport.who = Org.Text;
                }
                if (Code.Text != null)
                {
                    b4 = true;
                    driver.person.passport.code = Code.Text;
                }
                if (Town.Text != null)
                {
                    b5 = true;
                    driver.person.city = Town.Text;
                }
                if (b1 && b2 && b3 && b4 && b5)
                    Navigation.PushAsync(new RegisterDrLic(driver));
            })));
        }
    }
}