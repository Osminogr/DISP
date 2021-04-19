package mono.com.stfalcon.chatkit.messages;


public class MessageInput_TypingListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.stfalcon.chatkit.messages.MessageInput.TypingListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onStartTyping:()V:GetOnStartTypingHandler:Com.Stfalcon.Chatkit.Messages.MessageInput/ITypingListenerInvoker, ChatKitBinding\n" +
			"n_onStopTyping:()V:GetOnStopTypingHandler:Com.Stfalcon.Chatkit.Messages.MessageInput/ITypingListenerInvoker, ChatKitBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Stfalcon.Chatkit.Messages.MessageInput+ITypingListenerImplementor, ChatKitBinding", MessageInput_TypingListenerImplementor.class, __md_methods);
	}


	public MessageInput_TypingListenerImplementor ()
	{
		super ();
		if (getClass () == MessageInput_TypingListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Stfalcon.Chatkit.Messages.MessageInput+ITypingListenerImplementor, ChatKitBinding", "", this, new java.lang.Object[] {  });
	}


	public void onStartTyping ()
	{
		n_onStartTyping ();
	}

	private native void n_onStartTyping ();


	public void onStopTyping ()
	{
		n_onStopTyping ();
	}

	private native void n_onStopTyping ();

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
