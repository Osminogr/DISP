package mono.com.stfalcon.chatkit.messages;


public class MessagesListAdapter_OnMessageViewLongClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.stfalcon.chatkit.messages.MessagesListAdapter.OnMessageViewLongClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMessageViewLongClick:(Landroid/view/View;Lcom/stfalcon/chatkit/commons/models/IMessage;)V:GetOnMessageViewLongClick_Landroid_view_View_Lcom_stfalcon_chatkit_commons_models_IMessage_Handler:Com.Stfalcon.Chatkit.Messages.MessagesListAdapter/IOnMessageViewLongClickListenerInvoker, ChatKitBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Stfalcon.Chatkit.Messages.MessagesListAdapter+IOnMessageViewLongClickListenerImplementor, ChatKitBinding", MessagesListAdapter_OnMessageViewLongClickListenerImplementor.class, __md_methods);
	}


	public MessagesListAdapter_OnMessageViewLongClickListenerImplementor ()
	{
		super ();
		if (getClass () == MessagesListAdapter_OnMessageViewLongClickListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Stfalcon.Chatkit.Messages.MessagesListAdapter+IOnMessageViewLongClickListenerImplementor, ChatKitBinding", "", this, new java.lang.Object[] {  });
	}


	public void onMessageViewLongClick (android.view.View p0, com.stfalcon.chatkit.commons.models.IMessage p1)
	{
		n_onMessageViewLongClick (p0, p1);
	}

	private native void n_onMessageViewLongClick (android.view.View p0, com.stfalcon.chatkit.commons.models.IMessage p1);

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
