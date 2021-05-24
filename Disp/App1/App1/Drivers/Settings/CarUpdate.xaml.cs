using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using System.Net.Http;
using Plugin.Media;
using System;
using Plugin.Media.Abstractions;
using System.IO;
using System.Text.RegularExpressions;

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
            mark.Text = nowUser.car.mark;
            model.Text = nowUser.car.model;
            number.Text = nowUser.car.carNumber;
            year.Text = nowUser.car.dataCar;
            color.Text = nowUser.car.color;
            vin.Text = nowUser.car.vin;
            ctc.Text = nowUser.car.regNumberCar;

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
                    if (mark.Text == null || (mark.Text != null && mark.Text.Length < 3))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения марки ТС!", "Закрыть");
                        return;
                    }

                    if (model.Text == null || (model.Text != null && model.Text.Length < 3))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения модели ТС!", "Закрыть");
                        return;
                    }

                    if (number.Text == null || (number.Text != null && number.Text.Length < 8))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения номера ТС!", "Закрыть");
                        return;
                    }

                    if (year.Text == null || (year.Text != null && year.Text.Length != 4))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения года ТС!", "Закрыть");
                        return;
                    }

                    if (color.Text == null || (color.Text != null && color.Text.Length < 3))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения цвета!", "Закрыть");
                        return;
                    }

                    if (vin.Text == null || (vin.Text != null && vin.Text.Length != 17))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения VIN!", "Закрыть");
                        return;
                    }

                    if (ctc.Text == null || (ctc.Text != null && ctc.Text.Length != 12))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения СТС!", "Закрыть");
                        return;
                    }

                    if (!Regex.IsMatch(vin.Text, @"^[a-zA-Z0-9]+$"))
                    {
                        await DisplayAlert("Сообщение", "VIN должен состоять из цифр и букв!", "Закрыть");
                        return;
                    }

                    nowUser.car.mark = mark.Text;
                    nowUser.car.model = model.Text;
                    nowUser.car.carNumber = number.Text;
                    nowUser.car.dataCar = year.Text;
                    nowUser.car.color = color.Text;
                    nowUser.car.vin = vin.Text;
                    nowUser.car.regNumberCar = ctc.Text;

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
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                }
            })));
        }
    }
}