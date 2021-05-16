
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Collections.Generic;
using System;
using System.IO;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPasp : ContentPage
    {
        Driver driver;
        public RegisterPasp(Driver now)
        {
            driver = now;
            InitializeComponent();

            OverrideTitleView("Регистрация", "Дальше", 80, -1);

            driver.person.passport = new Passport();

            photo1.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    try
                    {
                        if (CrossMedia.Current.IsTakePhotoSupported)
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
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        await DisplayAlert("Сообщение", "Не удалось загрузить фотографию! Попробуйте позже.", "Закрыть");
                    }
                })
            });

            photo2.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    try
                    {
                        if (CrossMedia.Current.IsPickPhotoSupported)
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
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        await DisplayAlert("Сообщение", "Не удалось загрузить фотографию! Попробуйте позже.", "Закрыть");
                    }
                })
            });

            photo3.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    try
                    {
                        if (CrossMedia.Current.IsPickPhotoSupported)
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
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        await DisplayAlert("Сообщение", "Не удалось загрузить фотографию! Попробуйте позже.", "Закрыть");
                    }
                })
            });

            personLabel.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => {
                    person.IsChecked = true;
                })
            });
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                try
                {
                    bool b1 = false, b2 = false, b3 = false, b4 = false, b5 = false, b6 = false;
                    if (number.Text != null) b1 = true;
                    if (Data.Text != null) b2 = true;
                    if (Org.Text != null) b3 = true;
                    if (Code.Text != null) b4 = true;
                    if (Town.Text != null) b5 = true;
                    if (driver.person.passport.photo1 != null && driver.person.passport.photo2 != null && driver.person.passport.photo3 != null) b6 = true;

                    if (b1 && b2 && b3 && b4 && b5 && b6 && person.IsChecked)
                    {
                        driver.person.passport.number = number.Text;
                        driver.person.passport.date = Data.Text;
                        driver.person.passport.who = Org.Text;
                        driver.person.passport.code = Code.Text;
                        driver.person.city = Town.Text;

                        await Navigation.PushAsync(new RegisterDrLic(driver));
                    }
                    else await DisplayAlert("Сообщение", "Необходимо заполнить всю информацию!", "Закрыть");
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