using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterCar : ContentPage
    {
        Driver driver = new Driver();
        int currentPhotoAdd = 0;
        public RegisterCar(Driver now)
        {
            driver = now;
            InitializeComponent();

            OverrideTitleView("Регистрация", "Дальше", 80, -1);

            driver.car = new Car();

            personLabel.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    person.IsChecked = true;
                })
            });

            photo1.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    OpenAddPhotoDialog(1);
                })
            });

            photo2.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    OpenAddPhotoDialog(2);
                })
            });

            photo3.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    OpenAddPhotoDialog(3);
                })
            });
        }

        public void CloseAddPhotoDialog(object sender, EventArgs e)
        {
            gridData.IsEnabled = true;
            AddPhotoDialog.IsVisible = false;
            AddPhotoDialog.Margin = new Thickness(0, 0, 0, 0);
            currentPhotoAdd = 0;
        }

        public void OpenAddPhotoDialog(int number)
        {
            gridData.IsEnabled = false;
            AddPhotoDialog.IsVisible = true;
            AddPhotoDialog.Margin = new Thickness(0, 0, 0, 0);
            currentPhotoAdd = number;
        }

        public async void LoadPhoto(object sender, EventArgs e)
        {
            try
            {
                if (CrossMedia.Current.IsPickPhotoSupported)
                {
                    MediaFile photo = await CrossMedia.Current.PickPhotoAsync();

                    if (photo != null)
                    {
                        actInd.IsRunning = true;
                        actInd.IsVisible = true;
                        AddPhotoDialog.IsVisible = false;
                        gridData.Opacity = 0;

                        await Task.Delay(1000);

                        if (currentPhotoAdd == 3)
                        {
                            photo3.Source = photo.Path;
                            driver.car.photo3 = new Photo();
                            driver.car.photo3.data = photo.GetStream();
                            driver.car.photo3.name = Path.GetFileName(photo.Path);
                        }

                        if (currentPhotoAdd == 2)
                        {
                            photo2.Source = photo.Path;
                            driver.car.photo2 = new Photo();
                            driver.car.photo2.data = photo.GetStream();
                            driver.car.photo2.name = Path.GetFileName(photo.Path);
                        }

                        if (currentPhotoAdd == 1)
                        {
                            photo1.Source = photo.Path;
                            driver.car.photo1 = new Photo();
                            driver.car.photo1.data = photo.GetStream();
                            driver.car.photo1.name = Path.GetFileName(photo.Path);
                        }

                        await Task.Delay(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await DisplayAlert("Сообщение", "Не удалось загрузить фотографию! Попробуйте позже.", "Закрыть");
            }

            gridData.IsEnabled = true;
            gridData.Opacity = 1;
            actInd.IsRunning = false;
            actInd.IsVisible = false;
            AddPhotoDialog.Margin = new Thickness(0, 0, 0, 0);
            AddPhotoDialog.IsVisible = false;
            currentPhotoAdd = 0;
        }

        public async void TakePhoto(object sender, EventArgs e)
        {
            try
            {
                if (CrossMedia.Current.IsTakePhotoSupported)
                {
                    MediaFile photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
                    {
                        SaveToAlbum = true
                    });

                    if (photo != null)
                    {
                        actInd.IsRunning = true;
                        actInd.IsVisible = true;
                        AddPhotoDialog.IsVisible = false;
                        gridData.Opacity = 0;

                        await Task.Delay(1000);

                        if (currentPhotoAdd == 3)
                        {
                            photo3.Source = photo.Path;
                            driver.car.photo3 = new Photo();
                            driver.car.photo3.data = photo.GetStream();
                            driver.car.photo3.name = Path.GetFileName(photo.Path);
                        }

                        if (currentPhotoAdd == 2)
                        {
                            photo2.Source = photo.Path;
                            driver.car.photo2 = new Photo();
                            driver.car.photo2.data = photo.GetStream();
                            driver.car.photo2.name = Path.GetFileName(photo.Path);
                        }

                        if (currentPhotoAdd == 1)
                        {
                            photo1.Source = photo.Path;
                            driver.car.photo1 = new Photo();
                            driver.car.photo1.data = photo.GetStream();
                            driver.car.photo1.name = Path.GetFileName(photo.Path);
                        }

                        await Task.Delay(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await DisplayAlert("Сообщение", "Не удалось загрузить фотографию! Попробуйте позже.", "Закрыть");
            }

            gridData.IsEnabled = true;
            gridData.Opacity = 1;
            actInd.IsRunning = false;
            actInd.IsVisible = false;
            AddPhotoDialog.Margin = new Thickness(0, 0, 0, 0);
            AddPhotoDialog.IsVisible = false;
            currentPhotoAdd = 0;
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

                    if (driver.car.photo1 == null)
                    {
                        await DisplayAlert("Сообщение", "Добавьте первую фотографию!", "Закрыть");
                        return;
                    }

                    if (driver.car.photo2 == null)
                    {
                        await DisplayAlert("Сообщение", "Добавьте вторую фотографию!", "Закрыть");
                        return;
                    }

                    if (driver.car.photo3 == null)
                    {
                        await DisplayAlert("Сообщение", "Добавьте третью фотографию!", "Закрыть");
                        return;
                    }

                    if (!person.IsChecked)
                    {
                        await DisplayAlert("Сообщение", "Согласие не подтверждено!", "Закрыть");
                        return;
                    }

                    if (!Regex.IsMatch(vin.Text, @"^[a-zA-Z0-9]+$"))
                    {
                        await DisplayAlert("Сообщение", "VIN должен состоять из цифр и букв!", "Закрыть");
                        return;
                    }

                    driver.car.mark = mark.Text;
                    driver.car.model = model.Text;
                    driver.car.carNumber = number.Text;
                    driver.car.dataCar = year.Text;
                    driver.car.color = color.Text;
                    driver.car.vin = vin.Text;
                    driver.car.regNumberCar = ctc.Text;

                    await Navigation.PushAsync(new Card(driver));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    await DisplayAlert("Сообщение", "Непредвиденная ошибка! Попробуйте позже.", "Закрыть");
                }
            })));
        }
    }
}