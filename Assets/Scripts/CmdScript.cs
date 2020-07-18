using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CmdScript : MonoBehaviour {

	public Text CmdLeftPnl;
	public Text CmdParamPnl;
	public Text CmdColorPnl;
	public Text CmdCamera;
	public Text CmdBuyContainerPnl;
	public Text CmdBuyStatusPnl;
	public GameObject ToggleView;
	Toggle toggle;

	void ToggleValueChanged()
	{
		ViewTransportOnClickEvent();
	}

	public void ViewTransportOnClickEvent()
	{
		if(toggle.isOn == false)
		{
			CmdLeftPnl.text = "BtnDownToUpOnClickEvent";
			CmdParamPnl.text = "BtnDownToUpOnClickEvent";
			CmdColorPnl.text = "BtnDownToUpOnClickEvent";
			CmdBuyContainerPnl.text = "";
			CmdBuyStatusPnl.text = "";
			CmdCamera.text = "DisableFreeCamera";
		}
		else
		{
			CmdLeftPnl.text = "BtnUpToDownOnClickEvent";
			CmdParamPnl.text = "BtnUpToDownOnClickEvent";
			CmdColorPnl.text = "BtnUpToDownOnClickEvent";
			CmdBuyContainerPnl.text = "";
			CmdBuyStatusPnl.text = "";
			CmdCamera.text = "EnableFreeCamera";
		}
	}
	
	
	void Start () {
		toggle = ToggleView.GetComponent<Toggle>();
		toggle.onValueChanged.AddListener(delegate {
			ToggleValueChanged();
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
