using System;
using Xamarin.Forms;

namespace App1.Utils
{
    public static class TitleView
    {
        public static View OverrideView(string name, int count)
        {
            return OverrideViewExt(name, 60, count);
        }

        public static View OverrideView(string name, int left, int count)
        {
            return OverrideViewExt(name, left, count);
        }

        private static View OverrideViewExt(string name, int left, int count)
        {
            Label view = new Label();
            view.Text = name;
            if (count != -1)
                view.Text += "(" + count + ")";
            view.TextColor = Color.Black;
            view.HorizontalTextAlignment = TextAlignment.Start;
            view.Padding = new Thickness(left, 0, 0, 0);
            view.FontSize = 18;

            return view;
        }

        public static View OverrideGridView(string name, string nameAction, int count, Command commandAction)
        {
            return OverrideGridViewExt(name, nameAction, 60, count, commandAction);
        }

        public static View OverrideGridView(string name, string nameAction, int left, int count, Command commandAction)
        {
            return OverrideGridViewExt(name, nameAction, left, count, commandAction);
        }

        private static View OverrideGridViewExt(string name, string nameAction, int left, int count, Command commandAction)
        {
            Grid grid = new Grid()
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition() {
                        Width = GridLength.Star
                    },
                    new ColumnDefinition()
                    {
                        Width = 100
                    }
                },
                RowDefinitions =
                {
                    new RowDefinition()
                },
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(0, 0, 0, 0)
            };

            View label = OverrideView(name, left, count);
            Grid.SetRow(label, 0);
            Grid.SetColumn(label, 0);

            Label btn = new Label();
            btn.Text = nameAction;
            btn.TextColor = Color.FromHex("#FFB800");
            btn.HorizontalTextAlignment = TextAlignment.Center;
            btn.Padding = new Thickness(0, 0, 0, 0);
            btn.FontSize = 18;
            btn.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = commandAction
            });
            Grid.SetRow(btn, 0);
            Grid.SetColumn(btn, 1);

            grid.Children.Add(btn);
            grid.Children.Add(label);

            return grid;
        }
    }
}
