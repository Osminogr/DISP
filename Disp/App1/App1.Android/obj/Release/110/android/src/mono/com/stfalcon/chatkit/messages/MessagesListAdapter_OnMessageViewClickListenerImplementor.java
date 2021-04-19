package mono.com.stfalcon.chatkit.messages;


public class MessagesListAdapter_OnMessageViewClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.stfalcon.chatkit.messages.MessagesListAdapter.OnMessageViewClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMessageViewClick:(Landroid/view/View;Lcom/stfalcon/chatkit/commons/models/IMessage;)V:GetOnMessageViewClick_Landroid_view_View_Lcom_stfalcon_chatkit_commons_models_IMessage_Handler:Com.Stfalcon.Chatkit.Messages.MessagesListAdapter/IOnMessageViewClickListenerInvoker, ChatKitBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Stfalcon.Chatkit.Messages.MessagesListAdapter+IOnMessageViewClickListenerImplementor, ChatKitBinding", MessagesListAdapter_OnMessageViewClickListenerImplementor.class, __md_methods);
	}


	public MessagesListAdapter_OnMessageViewClickListenerImplementor ()
	{
		super ();
		if (getClass () == MessagesListAdapter_OnMessageViewClickListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Stfalcon.Chatkit.Messages.MessagesListAdapter+IOnMessageViewClickListenerImplementor, ChatKitBinding", "", this, new java.lang.Object[] {  });
	}


	public void onMessageViewClick (android.view.View p0, com.stfalcon.chatkit.commons.models.IMessage p1)
	{
		n_onMessageViewClick (p0, p1);
	}

	private native void n_onMessageViewClick (android.view.View p0, com.stfalcon.chatkit.commons.models.IMessage p1);

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
