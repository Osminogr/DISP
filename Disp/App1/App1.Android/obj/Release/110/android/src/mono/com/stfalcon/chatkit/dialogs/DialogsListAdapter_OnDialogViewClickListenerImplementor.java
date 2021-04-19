package mono.com.stfalcon.chatkit.dialogs;


public class DialogsListAdapter_OnDialogViewClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.stfalcon.chatkit.dialogs.DialogsListAdapter.OnDialogViewClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDialogViewClick:(Landroid/view/View;Lcom/stfalcon/chatkit/commons/models/IDialog;)V:GetOnDialogViewClick_Landroid_view_View_Lcom_stfalcon_chatkit_commons_models_IDialog_Handler:Com.Stfalcon.Chatkit.Dialogs.DialogsListAdapter/IOnDialogViewClickListenerInvoker, ChatKitBinding\n" +
			"";
		mono.android.Runtime.register ("Com.Stfalcon.Chatkit.Dialogs.DialogsListAdapter+IOnDialogViewClickListenerImplementor, ChatKitBinding", DialogsListAdapter_OnDialogViewClickListenerImplementor.class, __md_methods);
	}


	public DialogsListAdapter_OnDialogViewClickListenerImplementor ()
	{
		super ();
		if (getClass () == DialogsListAdapter_OnDialogViewClickListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Stfalcon.Chatkit.Dialogs.DialogsListAdapter+IOnDialogViewClickListenerImplementor, ChatKitBinding", "", this, new java.lang.Object[] {  });
	}


	public void onDialogViewClick (android.view.View p0, com.stfalcon.chatkit.commons.models.IDialog p1)
	{
		n_onDialogViewClick (p0, p1);
	}

	private native void n_onDialogViewClick (android.view.View p0, com.stfalcon.chatkit.commons.models.IDialog p1);

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
