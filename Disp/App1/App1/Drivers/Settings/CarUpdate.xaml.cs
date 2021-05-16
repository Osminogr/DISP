
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
    public partial class CarUpdate : ContentPage
    {
        Driver nowUser;
        public CarUpdate(Driver now)
        {
            nowUser = Server.GetAuthObject().driver;

            InitializeComponent();
            Mark.Text = nowUser.car.mark;
            Model.Text = nowUser.car.model;
            Number.Text = nowUser.car.carNumber;
            Data.Text = nowUser.car.dataCar;
            Color.Text = nowUser.car.color;
            VIN.Text = nowUser.car.vin;
            Reg.Text = nowUser.car.regNumberCar;

            photo1.Source = nowUser.car.urlPhoto1;
            photo2.Source = nowUser.car.urlPhoto2;
            photo3.Source = nowUser.car.urlPhoto3;

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
                            nowUser.car.photo1 = new Photo();
                            nowUser.car.photo1.data = photo.GetStream();
                            nowUser.car.photo1.name = Path.GetFileName(photo.Path);
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
                            nowUser.car.photo2 = new Photo();
                            nowUser.car.photo2.data = photo.GetStream();
                            nowUser.car.photo2.name = Path.GetFileName(photo.Path);
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
                            nowUser.car.photo3 = new Photo();
                            nowUser.car.photo3.data = photo.GetStream();
                            nowUser.car.photo3.name = Path.GetFileName(photo.Path);
                        }
                    }
                })
            });

            OverrideTitleView("Автомобиль", "Сохранить", 80, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                try
                {
                    bool b1 = false, b2 = false, b3 = false, b4 = false, b5 = false, b6 = false, b7 = false;
                    if (Mark.Text != null) b1 = true;
                    if (Model.Text != null) b2 = true;
                    if (Number.Text != null) b3 = true;
                    if (Data.Text != null) b4 = true;
                    if (Color.Text != null) b5 = true;
                    if (VIN.Text != null) b6 = true;
                    if (Reg.Text != null) b7 = true;

                    if (b1 && b2 && b3 && b4 && b5 && b6 && b7)
                    {
                        nowUser.car.mark = Mark.Text;
                        nowUser.car.model = Model.Text;
                        nowUser.car.carNumber = Number.Text;
                        nowUser.car.dataCar = Data.Text;
                        nowUser.car.color = Color.Text;
                        nowUser.car.vin = VIN.Text;
                        nowUser.car.regNumberCar = Reg.Text;

                        HttpContent answer = await Server.SaveCar(nowUser.car);
                        string response = await answer.ReadAsStringAsync();

                        if (response == null || (response != null && !response.Contains(nameof(Car))))
                        {
                            await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                        }
                        else
                        {
                            nowUser = await Server.GetDriver(nowUser.id);
                            Server.SaveAuthObject(nowUser, false);
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