package mono.com.stfalcon.chatkit.messages;


public class MessagesListAdapter_OnLoadMoreListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.stfalcon.chatkit.messages.MessagesListAdapter.OnLoadMoreListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLoadMore:(II)V:GetOnLoadMore_IIHandler:Com.Stfalcon.Chatkit.Messages.MessagesListAdapter/IOnLoadMoreListenerInvoker, ChatKitBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Stfalcon.Chatkit.Messages.MessagesListAdapter+IOnLoadMoreListenerImplementor, ChatKitBinding", MessagesListAdapter_OnLoadMoreListenerImplementor.class, __md_methods);
	}


	public MessagesListAdapter_OnLoadMoreListenerImplementor ()
	{
		super ();
		if (getClass () == MessagesListAdapter_OnLoadMoreListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Stfalcon.Chatkit.Messages.MessagesListAdapter+IOnLoadMoreListenerImplementor, ChatKitBinding", "", this, new java.lang.Object[] {  });
	}


	public void onLoadMore (int p0, int p1)
	{
		n_onLoadMore (p0, p1);
	}

	private native void n_onLoadMore (int p0, int p1);

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
