using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterDrLic : ContentPage
    {
        Driver driver;
        int currentPhotoAdd = 0;
        public RegisterDrLic(Driver now)
        {
            driver = now;
            InitializeComponent();

            OverrideTitleView("Регистрация", "Дальше", 80, -1);

            driver.driverLicence = new DriverLicence();

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

            personLabel.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    person.IsChecked = true;
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
                            driver.driverLicence.photo3 = new Photo();
                            driver.driverLicence.photo3.data = photo.GetStream();
                            driver.driverLicence.photo3.name = Path.GetFileName(photo.Path);
                        }

                        if (currentPhotoAdd == 2)
                        {
                            photo2.Source = photo.Path;
                            driver.driverLicence.photo2 = new Photo();
                            driver.driverLicence.photo2.data = photo.GetStream();
                            driver.driverLicence.photo2.name = Path.GetFileName(photo.Path);
                        }

                        if (currentPhotoAdd == 1)
                        {
                            photo1.Source = photo.Path;
                            driver.driverLicence.photo1 = new Photo();
                            driver.driverLicence.photo1.data = photo.GetStream();
                            driver.driverLicence.photo1.name = Path.GetFileName(photo.Path);
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
                            driver.driverLicence.photo3 = new Photo();
                            driver.driverLicence.photo3.data = photo.GetStream();
                            driver.driverLicence.photo3.name = Path.GetFileName(photo.Path);
                        }

                        if (currentPhotoAdd == 2)
                        {
                            photo2.Source = photo.Path;
                            driver.driverLicence.photo2 = new Photo();
                            driver.driverLicence.photo2.data = photo.GetStream();
                            driver.driverLicence.photo2.name = Path.GetFileName(photo.Path);
                        }

                        if (currentPhotoAdd == 1)
                        {
                            photo1.Source = photo.Path;
                            driver.driverLicence.photo1 = new Photo();
                            driver.driverLicence.photo1.data = photo.GetStream();
                            driver.driverLicence.photo1.name = Path.GetFileName(photo.Path);
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
                    if (serialNumberLicence.Text == null && (serialNumberLicence.Text != null && serialNumberLicence.Text.Length != 12))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения серии и номера водительского удостоверения!", "Закрыть");
                        return;
                    }

                    if (dateFromLicence.Date == null)
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения даты получения водительского удостоверения!", "Закрыть");
                        return;
                    }

                    if (dateToLicence.Date == null)
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения срока действия водительского удостоверения!", "Закрыть");
                        return;
                    }

                    if (driver.driverLicence.photo1 == null)
                    {
                        await DisplayAlert("Сообщение", "Добавьте первую фотографию!", "Закрыть");
                        return;
                    }

                    if (driver.driverLicence.photo2 == null)
                    {
                        await DisplayAlert("Сообщение", "Добавьте вторую фотографию!", "Закрыть");
                        return;
                    }

                    if (driver.driverLicence.photo3 == null)
                    {
                        await DisplayAlert("Сообщение", "Добавьте третью фотографию!", "Закрыть");
                        return;
                    }

                    if (!person.IsChecked)
                    {
                        await DisplayAlert("Сообщение", "Согласие не подтверждено!", "Закрыть");
                        return;
                    }

                    driver.driverLicence.number = serialNumberLicence.Text;
                    driver.driverLicence.date = dateFromLicence.Date.ToShortDateString();
                    driver.driverLicence.period = dateToLicence.Date.ToShortDateString();

                    await Navigation.PushAsync(new RegisterCar(driver));
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