using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Widget;
using App1;
using App1.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace App1.Droid
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        public CustomMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
            }

            if (e.NewElement != null)
            {
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.SetInfoWindowAdapter(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);
            marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.car));
            return marker;
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;

                if (marker.Snippet != null)
                {
                    view = inflater.Inflate(Resource.Layout.InfoWindow, null);

                    var fio = view.FindViewById<TextView>(Resource.Id.textView1);
                    var car = view.FindViewById<TextView>(Resource.Id.textView2);

                    fio.Text = marker.Title;
                    car.Text = marker.Snippet;
                }
                else
                {
                    view = inflater.Inflate(Resource.Layout.InfoWindowEasy, null);

                    var fio = view.FindViewById<TextView>(Resource.Id.textView1);

                    fio.Text = marker.Title;
                }
                
                return view;
            }
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }
    }
}