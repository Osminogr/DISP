
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterDrLic : ContentPage
    {
        Driver driver;
        public RegisterDrLic(Driver now)
        {
            driver = now;
            InitializeComponent();

            OverrideTitleView("Регистрация", "Дальше", 80, -1);

            driver.driverLicence = new DriverLicence();

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
                            driver.driverLicence.photo1 = new Photo();
                            driver.driverLicence.photo1.data = photo.GetStream();
                            driver.driverLicence.photo1.name = Path.GetFileName(photo.Path);
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
                            driver.driverLicence.photo2 = new Photo();
                            driver.driverLicence.photo2.data = photo.GetStream();
                            driver.driverLicence.photo2.name = Path.GetFileName(photo.Path);
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
                            driver.driverLicence.photo3 = new Photo();
                            driver.driverLicence.photo3.data = photo.GetStream();
                            driver.driverLicence.photo3.name = Path.GetFileName(photo.Path);
                        }
                    }
                })
            });
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false;
                if (Number.Text != null) b1 = true;
                if (Date.Text != null) b2 = true;
                if (Period.Text != null) b3 = true;
                if (driver.driverLicence.photo1 != null && driver.driverLicence.photo2 != null && driver.driverLicence.photo3 != null) b4 = true;

                if (b1 && b2 && b3 && b4 && person.IsChecked)
                {
                    driver.driverLicence.number = Number.Text;
                    driver.driverLicence.date = Date.Text;
                    driver.driverLicence.period = Period.Text;

                    await Navigation.PushAsync(new RegisterCar(driver));
                }
                else await DisplayAlert("Сообщение", "Необходимо заполнить всю информацию!", "Закрыть");
            })));
        }
    }
}