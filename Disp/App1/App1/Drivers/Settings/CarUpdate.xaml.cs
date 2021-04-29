
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

namespace App1.Drivers.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarUpdate : ContentPage
    {
        Driver nowUser;
        public CarUpdate(Driver now)
        {
            nowUser = now;
            InitializeComponent();
            Mark.Text = nowUser.car.mark;
            Model.Text = nowUser.car.model;
            Number.Text = nowUser.car.carNumber;
            Data.Text = nowUser.car.dataCar;
            Color.Text = nowUser.car.color;
            VIN.Text = nowUser.car.vin;
            Reg.Text = nowUser.car.regNumberCar;
            ToolbarItem tb = new ToolbarItem
            {
                Text = "Сохранить"
            };
            tb.Clicked += async (s, e) =>
            {
                bool b1 = false, b2 = false, b3 = false, b4 = false, b5 = false, b6 = false, b7 = false;
                if (Mark.Text != null)
                {
                    b1 = true;
                    nowUser.car.mark = Mark.Text;
                }
                if (Model.Text != null)
                {
                    b2 = true;
                    nowUser.car.model = Model.Text;
                }
                if (Number.Text != null)
                {
                    b3 = true;
                    nowUser.car.carNumber = Number.Text;
                }
                if (Data.Text != null)
                {
                    b4 = true;
                    nowUser.car.dataCar = Data.Text;
                }
                if (Color.Text != null)
                {
                    b5 = true;
                    nowUser.car.color = Color.Text;
                }
                if (VIN.Text != null)
                {
                    b6 = true;
                    nowUser.car.vin = VIN.Text;
                }
                if (Reg.Text != null)
                {
                    b7 = true;
                    nowUser.car.regNumberCar = Reg.Text;
                }
                if (b1 && b2 && b3 && b4 && b5 && b6 && b7)
                    await Navigation.PopAsync();
            };
            ToolbarItems.Add(tb);
        }
    }
}