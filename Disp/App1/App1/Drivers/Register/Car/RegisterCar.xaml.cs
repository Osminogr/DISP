
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterCar : ContentPage
    {
        Driver driver = new Driver();
        public RegisterCar(Driver now)
        {
            driver = now;
            InitializeComponent();

            OverrideTitleView("Регистрация", "Дальше", 80, -1);

            driver.car = new Car();

            photo1.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (CrossMedia.Current.IsTakePhotoSupported)
                    {
                        MediaFile photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                        {
                            SaveToAlbum = true,
                            Name = "photo" + DateTime.Now + ".jpg"
                        });

                        if (photo != null)
                        {
                            photo1.Source = photo.Path;
                            driver.car.photo1 = new Photo();
                            driver.car.photo1.data = photo.GetStream();
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
                            driver.car.photo2 = new Photo();
                            driver.car.photo2.data = photo.GetStream();
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
                            driver.car.photo3 = new Photo();
                            driver.car.photo3.data = photo.GetStream();
                        }
                    }
                })
            });
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(() =>
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false, b5 = false, b6 = false, b7 = false;
                if (Mark.Text != null)
                {
                    b1 = true;
                    driver.car.mark = Mark.Text;
                }
                if (Model.Text != null)
                {
                    b2 = true;
                    driver.car.model = Model.Text;
                }
                if (Number.Text != null)
                {
                    b3 = true;
                    driver.car.carNumber = Number.Text;
                }
                if (Data.Text != null)
                {
                    b4 = true;
                    driver.car.dataCar = Data.Text;
                }
                if (Color.Text != null)
                {
                    b5 = true;
                    driver.car.color = Color.Text;
                }
                if (VIN.Text != null)
                {
                    b6 = true;
                    driver.car.vin = VIN.Text;
                }
                if (Reg.Text != null)
                {
                    b7 = true;
                    driver.car.regNumberCar = Reg.Text;
                }
                if (b1 && b2 && b3 && b4 && b5 && b6 && b7)
                    Navigation.PushAsync(new Card(driver));
            })));
        }
    }
}