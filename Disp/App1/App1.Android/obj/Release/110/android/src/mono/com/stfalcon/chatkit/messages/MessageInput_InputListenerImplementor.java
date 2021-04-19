package mono.com.stfalcon.chatkit.messages;


public class MessageInput_InputListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.stfalcon.chatkit.messages.MessageInput.InputListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onSubmit:(Ljava/lang/CharSequence;)Z:GetOnSubmit_Ljava_lang_CharSequence_Handler:Com.Stfalcon.Chatkit.Messages.MessageInput/IInputListenerInvoker, ChatKitBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Stfalcon.Chatkit.Messages.MessageInput+IInputListenerImplementor, ChatKitBinding", MessageInput_InputListenerImplementor.class, __md_methods);
	}


	public MessageInput_InputListenerImplementor ()
	{
		super ();
		if (getClass () == MessageInput_InputListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Stfalcon.Chatkit.Messages.MessageInput+IInputListenerImplementor, ChatKitBinding", "", this, new java.lang.Object[] {  });
	}


	public boolean onSubmit (java.lang.CharSequence p0)
	{
		return n_onSubmit (p0);
	}

	private native boolean n_onSubmit (java.lang.CharSequence p0);

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
