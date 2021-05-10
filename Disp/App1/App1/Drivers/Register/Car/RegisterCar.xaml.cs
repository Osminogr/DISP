
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
                            driver.car.photo1.name = Path.GetFileName(photo.Path);
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
                            driver.car.photo2.name = Path.GetFileName(photo.Path);
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
                            driver.car.photo3.name = Path.GetFileName(photo.Path);
                        }
                    }
                })
            });
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false, b5 = false, b6 = false, b7 = false, b8 = false;
                if (Mark.Text != null) b1 = true;
                if (Model.Text != null) b2 = true;
                if (Number.Text != null) b3 = true;
                if (Data.Text != null) b4 = true;
                if (Color.Text != null) b5 = true;
                if (VIN.Text != null) b6 = true;
                if (Reg.Text != null) b7 = true;

                if (driver.car.photo1 != null && driver.car.photo2 != null && driver.car.photo3 != null) b8 = true;

                if (b1 && b2 && b3 && b4 && b5 && b6 && b7 && b8 && person.IsChecked)
                {
                    driver.car.mark = Mark.Text;
                    driver.car.model = Model.Text;
                    driver.car.carNumber = Number.Text;
                    driver.car.dataCar = Data.Text;
                    driver.car.color = Color.Text;
                    driver.car.vin = VIN.Text;
                    driver.car.regNumberCar = Reg.Text;

                    await Navigation.PushAsync(new Card(driver));
                }
                else
                {
                    await DisplayAlert("Сообщение", "Необходимо заполнить всю информацию!", "Закрыть");
                }
            })));
        }
    }
}