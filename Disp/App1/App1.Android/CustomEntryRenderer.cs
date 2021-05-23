using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using App1;
using App1.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace App1.Droid
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                //this.Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
                var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
                shape.Paint.Color = Xamarin.Forms.Color.Black.ToAndroid();
                shape.Paint.SetStyle(Paint.Style.Stroke);
                shape.Paint.StrokeWidth = 5;
                this.Control.Background = shape;
                this.Control.SetPadding(10, 0, 0, 0);
            }
        }
    }
}