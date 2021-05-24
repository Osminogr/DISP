using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using System.Net.Http;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;

namespace App1.Drivers.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasportDataUpdate : ContentPage
    {
        Driver driver;
        public PasportDataUpdate(Driver now)
        {
            driver = Server.GetAuthObject().driver;
            InitializeComponent();

            serialNumberPassport.Text = driver.person.passport.number;
            datePassport.Date = DateTime.Parse(driver.person.passport.date);
            whoPassport.Text = driver.person.passport.who;
            codePassport.Text = driver.person.passport.code;

            photo1.Source = driver.person.passport.urlPhoto1;
            photo2.Source = driver.person.passport.urlPhoto2;
            photo3.Source = driver.person.passport.urlPhoto3;

            photo1.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    if (CrossMedia.Current.IsPickPhotoSupported)
                    {
                        MediaFile photo = await CrossMedia.Current.PickPhotoAsync();

                        if (photo != null)
                        {
                            photo1.Source = photo.Path;
                            driver.person.passport.photo1 = new Photo();
                            driver.person.passport.photo1.data = photo.GetStream();
                            driver.person.passport.photo1.name = Path.GetFileName(photo.Path);
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
                            driver.person.passport.photo2 = new Photo();
                            driver.person.passport.photo2.data = photo.GetStream();
                            driver.person.passport.photo2.name = Path.GetFileName(photo.Path);
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
                            driver.person.passport.photo3 = new Photo();
                            driver.person.passport.photo3.data = photo.GetStream();
                            driver.person.passport.photo3.name = Path.GetFileName(photo.Path);
                        }
                    }
                })
            });

            OverrideTitleView("Паспортные данные", "Сохранить", 40, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                try
                {
                    if (serialNumberPassport.Text == null || (serialNumberPassport.Text != null && serialNumberPassport.Text.Length != 12))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения серии и номера паспорта!", "Закрыть");
                        return;
                    }

                    if (datePassport.Date == null)
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения даты выдачи паспорта!", "Закрыть");
                        return;
                    }

                    if (whoPassport.Text == null || (whoPassport.Text != null && whoPassport.Text.Length < 10))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения органа выдавшего паспорт!", "Закрыть");
                        return;
                    }

                    if (codePassport.Text == null || (codePassport.Text != null && codePassport.Text.Length != 7))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения кода подразделения паспорта!", "Закрыть");
                        return;
                    }

                    driver.person.passport.number = serialNumberPassport.Text;
                    driver.person.passport.date = datePassport.Date.ToShortDateString();
                    driver.person.passport.who = whoPassport.Text;
                    driver.person.passport.code = codePassport.Text;

                    HttpContent answer = await Server.SavePassport(driver.person.passport);
                    string response = await answer.ReadAsStringAsync();

                    if (response == null || (response != null && !response.Contains(nameof(Passport))))
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                }
            })));
        }
    }
}