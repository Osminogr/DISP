using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;

namespace App1.Drivers.Register
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelfPhotoDriver : ContentPage
    {
        string phone;
        Driver driver;
        public SelfPhotoDriver(string number)
        {
            phone = number;

            InitializeComponent();
            StartSelfPhoto();
        }

        private async void StartSelfPhoto()
        {
            try
            {
                if (CrossMedia.Current.IsTakePhotoSupported)
                {
                    MediaFile photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions() {
                        SaveToAlbum = true
                    });

                    if (photo != null)
                    {
                        await Task.Delay(1000);

                        
                        driver = new Driver();
                        driver.person = new Person();
                        driver.person.phone = phone;
                        driver.person.selfPhoto = new Photo() {
                            data = photo.GetStream(),
                            name = Path.GetFileName(photo.Path)
                        };

                        await Navigation.PushAsync(new RegisterChooseDriver(driver), true);
                    }
                    else
                    {
                        await DisplayAlert("Сообщение", "Не удалось сделать фотографию! Попробуйте позже.", "Закрыть");
                        await Navigation.PopAsync(true);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await DisplayAlert("Сообщение", "Не удалось сделать фотографию! Попробуйте позже.", "Закрыть");
                await Navigation.PopAsync(true);
            }
        }
    }
}