package mono.com.stfalcon.chatkit.dialogs;


public class DialogsListAdapter_OnDialogClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.stfalcon.chatkit.dialogs.DialogsListAdapter.OnDialogClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDialogClick:(Lcom/stfalcon/chatkit/commons/models/IDialog;)V:GetOnDialogClick_Lcom_stfalcon_chatkit_commons_models_IDialog_Handler:Com.Stfalcon.Chatkit.Dialogs.DialogsListAdapter/IOnDialogClickListenerInvoker, ChatKitBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Stfalcon.Chatkit.Dialogs.DialogsListAdapter+IOnDialogClickListenerImplementor, ChatKitBinding", DialogsListAdapter_OnDialogClickListenerImplementor.class, __md_methods);
	}


	public DialogsListAdapter_OnDialogClickListenerImplementor ()
	{
		super ();
		if (getClass () == DialogsListAdapter_OnDialogClickListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Stfalcon.Chatkit.Dialogs.DialogsListAdapter+IOnDialogClickListenerImplementor, ChatKitBinding", "", this, new java.lang.Object[] {  });
	}


	public void onDialogClick (com.stfalcon.chatkit.commons.models.IDialog p0)
	{
		n_onDialogClick (p0);
	}

	private native void n_onDialogClick (com.stfalcon.chatkit.commons.models.IDialog p0);

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
