
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            Mark.Text = nowUser.mark;
            Model.Text = nowUser.model;
            Number.Text = nowUser.carNumber;
            Data.Text = nowUser.dataCar;
            Color.Text = nowUser.color;
            VIN.Text = nowUser.vin;
            Reg.Text = nowUser.regNumberCar;
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
                    nowUser.mark = Mark.Text;
                }
                if (Model.Text != null)
                {
                    b2 = true;
                    nowUser.model = Model.Text;
                }
                if (Number.Text != null)
                {
                    b3 = true;
                    nowUser.carNumber = Number.Text;
                }
                if (Data.Text != null)
                {
                    b4 = true;
                    nowUser.dataCar = Data.Text;
                }
                if (Color.Text != null)
                {
                    b5 = true;
                    nowUser.color = Color.Text;
                }
                if (VIN.Text != null)
                {
                    b6 = true;
                    nowUser.vin = VIN.Text;
                }
                if (Reg.Text != null)
                {
                    b7 = true;
                    nowUser.regNumberCar = Reg.Text;
                }
                if (b1 && b2 && b3 && b4 && b5 && b6 && b7)
                    await Navigation.PopAsync();
            };
            ToolbarItems.Add(tb);
        }
    }
}