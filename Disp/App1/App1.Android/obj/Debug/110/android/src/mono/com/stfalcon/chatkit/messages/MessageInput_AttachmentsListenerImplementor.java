package mono.com.stfalcon.chatkit.messages;


public class MessageInput_AttachmentsListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.stfalcon.chatkit.messages.MessageInput.AttachmentsListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAddAttachments:()V:GetOnAddAttachmentsHandler:Com.Stfalcon.Chatkit.Messages.MessageInput/IAttachmentsListenerInvoker, ChatKitBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Stfalcon.Chatkit.Messages.MessageInput+IAttachmentsListenerImplementor, ChatKitBinding", MessageInput_AttachmentsListenerImplementor.class, __md_methods);
	}


	public MessageInput_AttachmentsListenerImplementor ()
	{
		super ();
		if (getClass () == MessageInput_AttachmentsListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Stfalcon.Chatkit.Messages.MessageInput+IAttachmentsListenerImplementor, ChatKitBinding", "", this, new java.lang.Object[] {  });
	}


	public void onAddAttachments ()
	{
		n_onAddAttachments ();
	}

	private native void n_onAddAttachments ();

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
