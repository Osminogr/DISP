package mono.com.stfalcon.chatkit.messages;


public class MessagesListAdapter_OnMessageLongClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.stfalcon.chatkit.messages.MessagesListAdapter.OnMessageLongClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMessageLongClick:(Lcom/stfalcon/chatkit/commons/models/IMessage;)V:GetOnMessageLongClick_Lcom_stfalcon_chatkit_commons_models_IMessage_Handler:Com.Stfalcon.Chatkit.Messages.MessagesListAdapter/IOnMessageLongClickListenerInvoker, ChatKitBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Stfalcon.Chatkit.Messages.MessagesListAdapter+IOnMessageLongClickListenerImplementor, ChatKitBinding", MessagesListAdapter_OnMessageLongClickListenerImplementor.class, __md_methods);
	}


	public MessagesListAdapter_OnMessageLongClickListenerImplementor ()
	{
		super ();
		if (getClass () == MessagesListAdapter_OnMessageLongClickListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Stfalcon.Chatkit.Messages.MessagesListAdapter+IOnMessageLongClickListenerImplementor, ChatKitBinding", "", this, new java.lang.Object[] {  });
	}


	public void onMessageLongClick (com.stfalcon.chatkit.commons.models.IMessage p0)
	{
		n_onMessageLongClick (p0);
	}

	private native void n_onMessageLongClick (com.stfalcon.chatkit.commons.models.IMessage p0);

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
