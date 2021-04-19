package mono.com.stfalcon.chatkit.dialogs;


public class DialogsListAdapter_OnDialogLongClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.stfalcon.chatkit.dialogs.DialogsListAdapter.OnDialogLongClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDialogLongClick:(Lcom/stfalcon/chatkit/commons/models/IDialog;)V:GetOnDialogLongClick_Lcom_stfalcon_chatkit_commons_models_IDialog_Handler:Com.Stfalcon.Chatkit.Dialogs.DialogsListAdapter/IOnDialogLongClickListenerInvoker, ChatKitBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Stfalcon.Chatkit.Dialogs.DialogsListAdapter+IOnDialogLongClickListenerImplementor, ChatKitBinding", DialogsListAdapter_OnDialogLongClickListenerImplementor.class, __md_methods);
	}


	public DialogsListAdapter_OnDialogLongClickListenerImplementor ()
	{
		super ();
		if (getClass () == DialogsListAdapter_OnDialogLongClickListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Stfalcon.Chatkit.Dialogs.DialogsListAdapter+IOnDialogLongClickListenerImplementor, ChatKitBinding", "", this, new java.lang.Object[] {  });
	}


	public void onDialogLongClick (com.stfalcon.chatkit.commons.models.IDialog p0)
	{
		n_onDialogLongClick (p0);
	}

	private native void n_onDialogLongClick (com.stfalcon.chatkit.commons.models.IDialog p0);

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
