package mono.com.stfalcon.chatkit.dialogs;


public class DialogsListAdapter_OnDialogViewLongClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.stfalcon.chatkit.dialogs.DialogsListAdapter.OnDialogViewLongClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDialogViewLongClick:(Landroid/view/View;Lcom/stfalcon/chatkit/commons/models/IDialog;)V:GetOnDialogViewLongClick_Landroid_view_View_Lcom_stfalcon_chatkit_commons_models_IDialog_Handler:Com.Stfalcon.Chatkit.Dialogs.DialogsListAdapter/IOnDialogViewLongClickListenerInvoker, ChatKitBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Stfalcon.Chatkit.Dialogs.DialogsListAdapter+IOnDialogViewLongClickListenerImplementor, ChatKitBinding", DialogsListAdapter_OnDialogViewLongClickListenerImplementor.class, __md_methods);
	}


	public DialogsListAdapter_OnDialogViewLongClickListenerImplementor ()
	{
		super ();
		if (getClass () == DialogsListAdapter_OnDialogViewLongClickListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Stfalcon.Chatkit.Dialogs.DialogsListAdapter+IOnDialogViewLongClickListenerImplementor, ChatKitBinding", "", this, new java.lang.Object[] {  });
	}


	public void onDialogViewLongClick (android.view.View p0, com.stfalcon.chatkit.commons.models.IDialog p1)
	{
		n_onDialogViewLongClick (p0, p1);
	}

	private native void n_onDialogViewLongClick (android.view.View p0, com.stfalcon.chatkit.commons.models.IDialog p1);

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
