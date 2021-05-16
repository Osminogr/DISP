
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using System.Net.Http;
using Plugin.Media;
using System;
using Plugin.Media.Abstractions;
using System.IO;

namespace App1.Drivers.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrLicUpdate : ContentPage
    {
        Driver driver;
        public DrLicUpdate(Driver now)
        {
            driver = Server.GetAuthObject().driver;
            InitializeComponent();

            Number.Text = driver.driverLicence.number;
            Data.Text = driver.driverLicence.date;
            Period.Text = driver.driverLicence.period;

            photo1.Source = driver.driverLicence.urlPhoto1;
            photo2.Source = driver.driverLicence.urlPhoto2;
            photo3.Source = driver.driverLicence.urlPhoto3;

            photo1.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (CrossMedia.Current.IsTakePhotoSupported)
                    {
                        MediaFile photo = await CrossMedia.Current.PickPhotoAsync();

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
                    if (CrossMedia.Current.IsTakePhotoSupported)
                    {
                        MediaFile photo = await CrossMedia.Current.PickPhotoAsync();

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
                    if (CrossMedia.Current.IsTakePhotoSupported)
                    {
                        MediaFile photo = await CrossMedia.Current.PickPhotoAsync();

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

            OverrideTitleView("Водит. удостоверение", "Сохранить", 15, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                try
                {
                    bool b1 = false, b2 = false, b3 = false;
                    if (Number.Text != null) b1 = true;
                    if (Data.Text != null) b2 = true;
                    if (Period.Text != null) b3 = true;

                    if (b1 && b2 && b3)
                    {
                        driver.driverLicence.number = Number.Text;
                        driver.driverLicence.date = Data.Text;
                        driver.driverLicence.period = Period.Text;

                        HttpContent answer = await Server.SaveDriverLicence(driver.driverLicence);
                        string response = await answer.ReadAsStringAsync();

                        if (response == null || (response != null && !response.Contains(nameof(DriverLicence))))
                        {
                            await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                        }
                        else
                        {
                            driver = await Server.GetDriver(driver.id);
                            Server.SaveAuthObject(driver, false);
                            await Navigation.PopAsync(true);
                        }
                    }
                    else
                    {
                        await DisplayAlert("Сообщение", "Не все поля заполнены корректно!", "Закрыть");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                }
            })));
        }
    }
}