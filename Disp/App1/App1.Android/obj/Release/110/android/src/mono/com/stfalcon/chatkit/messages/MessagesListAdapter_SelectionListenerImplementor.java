package mono.com.stfalcon.chatkit.messages;


public class MessagesListAdapter_SelectionListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.stfalcon.chatkit.messages.MessagesListAdapter.SelectionListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onSelectionChanged:(I)V:GetOnSelectionChanged_IHandler:Com.Stfalcon.Chatkit.Messages.MessagesListAdapter/ISelectionListenerInvoker, ChatKitBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Stfalcon.Chatkit.Messages.MessagesListAdapter+ISelectionListenerImplementor, ChatKitBinding", MessagesListAdapter_SelectionListenerImplementor.class, __md_methods);
	}


	public MessagesListAdapter_SelectionListenerImplementor ()
	{
		super ();
		if (getClass () == MessagesListAdapter_SelectionListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Stfalcon.Chatkit.Messages.MessagesListAdapter+ISelectionListenerImplementor, ChatKitBinding", "", this, new java.lang.Object[] {  });
	}


	public void onSelectionChanged (int p0)
	{
		n_onSelectionChanged (p0);
	}

	private native void n_onSelectionChanged (int p0);

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
