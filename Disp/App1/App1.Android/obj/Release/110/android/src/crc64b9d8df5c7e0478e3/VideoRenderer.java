package crc64b9d8df5c7e0478e3;


public class VideoRenderer
	extends crc643f46942d9dd1fff9.ViewRenderer_2
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Rox.VideoRenderer, Rox.Xamarin.Video.Android", VideoRenderer.class, __md_methods);
	}


	public VideoRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == VideoRenderer.class)
			mono.android.TypeManager.Activate ("Rox.VideoRenderer, Rox.Xamarin.Video.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public VideoRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == VideoRenderer.class)
			mono.android.TypeManager.Activate ("Rox.VideoRenderer, Rox.Xamarin.Video.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public VideoRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == VideoRenderer.class)
			mono.android.TypeManager.Activate ("Rox.VideoRenderer, Rox.Xamarin.Video.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
