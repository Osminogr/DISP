
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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
                                await Task.Delay(1000);
                                photo1.Source = photo.Path;
                                driver.person.passport.photo1 = new Photo();
                                driver.person.passport.photo1.data = photo.GetStream();
                                driver.person.passport.photo1.name = Path.GetFileName(photo.Path);
                                await Task.Delay(1000);
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
                                await Task.Delay(1000);
                                photo2.Source = photo.Path;
                                driver.person.passport.photo2 = new Photo();
                                driver.person.passport.photo2.data = photo.GetStream();
                                driver.person.passport.photo2.name = Path.GetFileName(photo.Path);
                                await Task.Delay(1000);
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
                                await Task.Delay(1000);
                                photo3.Source = photo.Path;
                                driver.person.passport.photo3 = new Photo();
                                driver.person.passport.photo3.data = photo.GetStream();
                                driver.person.passport.photo3.name = Path.GetFileName(photo.Path);
                                await Task.Delay(1000);
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
                Command = new Command(() =>
                {
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
                    if (town.Text == null || (town.Text != null && town.Text.Length < 3))
                    {
                        await DisplayAlert("Сообщение", "Ошибка заполнения города!", "Закрыть");
                        return;
                    }

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
                        await DisplayAlert("Сообщение", "Ошибка заполнения кода подразделения паспорт!", "Закрыть");
                        return;
                    }

                    if (driver.person.passport.photo1 == null)
                    {
                        await DisplayAlert("Сообщение", "Добавьте первую фотографию!", "Закрыть");
                        return;
                    }

                    if (driver.person.passport.photo2 == null)
                    {
                        await DisplayAlert("Сообщение", "Добавьте вторую фотографию!", "Закрыть");
                        return;
                    }

                    if (driver.person.passport.photo3 == null)
                    {
                        await DisplayAlert("Сообщение", "Добавьте третью фотографию!", "Закрыть");
                        return;
                    }

                    if (!person.IsChecked)
                    {
                        await DisplayAlert("Сообщение", "Согласие не подтверждено!", "Закрыть");
                        return;
                    }

                    if (!Regex.IsMatch(town.Text, @"^[a-zA-Z]+$") && !Regex.IsMatch(town.Text, @"^[а-яА-Я]+$"))
                    {
                        await DisplayAlert("Сообщение", "Город не должен содержать цифры!", "Закрыть");
                        return;
                    }

                    driver.person.passport.number = serialNumberPassport.Text;
                    driver.person.passport.date = datePassport.Date.ToShortDateString();
                    driver.person.passport.who = whoPassport.Text;
                    driver.person.passport.code = codePassport.Text;
                    driver.person.city = town.Text;

                    await Navigation.PushAsync(new RegisterDrLic(driver));
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