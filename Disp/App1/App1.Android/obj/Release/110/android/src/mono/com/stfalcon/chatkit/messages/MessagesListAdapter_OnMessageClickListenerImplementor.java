package mono.com.stfalcon.chatkit.messages;


public class MessagesListAdapter_OnMessageClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.stfalcon.chatkit.messages.MessagesListAdapter.OnMessageClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMessageClick:(Lcom/stfalcon/chatkit/commons/models/IMessage;)V:GetOnMessageClick_Lcom_stfalcon_chatkit_commons_models_IMessage_Handler:Com.Stfalcon.Chatkit.Messages.MessagesListAdapter/IOnMessageClickListenerInvoker, ChatKitBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Stfalcon.Chatkit.Messages.MessagesListAdapter+IOnMessageClickListenerImplementor, ChatKitBinding", MessagesListAdapter_OnMessageClickListenerImplementor.class, __md_methods);
	}


	public MessagesListAdapter_OnMessageClickListenerImplementor ()
	{
		super ();
		if (getClass () == MessagesListAdapter_OnMessageClickListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Stfalcon.Chatkit.Messages.MessagesListAdapter+IOnMessageClickListenerImplementor, ChatKitBinding", "", this, new java.lang.Object[] {  });
	}


	public void onMessageClick (com.stfalcon.chatkit.commons.models.IMessage p0)
	{
		n_onMessageClick (p0);
	}

	private native void n_onMessageClick (com.stfalcon.chatkit.commons.models.IMessage p0);

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
