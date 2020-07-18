using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour {

	public bool EnableScript = false;
	[Header("Conditions")]
	public string ActionStatus;
	public string AccelerateStatus;
	public string SpeedStatus;

	[Header("RemoteManager")]
	public bool UseRemoteManagement = false;
	public GameObject RemoteManagementCmdLine;

	[Header("AutoTargets")]
	public bool UseAutoSettingForTargets = true;
	public Canvas ScreenCanvas;

	[Header("AutoHiding")]
	public bool UseAutoHidingButtons = false;
	public bool BtnUpToDownIsHiding = false;
	public bool BtnDownToUpIsHiding = false;


	[Header("User interface")]
	public GameObject BtnUpToDown;
	public GameObject BtnDownToUp;

	[Header("Down panel")]
	//Panel
	public GameObject DownPanelObject;
	public float ShiftTarget_BDownPanelY;
	//Targets
	public GameObject TargetForDownPanelA;
	public GameObject TargetForDownPanelB;
	//Speed
	public float SpeedForDownPanel;
	private float SpeedForDownPanelDefault = 0;
	//Acceleration
	public float AccelerationForDownPanel;
	private float AccelerationForDownPanelDefault;

	public void RemoteManagerUI()
	{
		if (UseRemoteManagement == true)
		{
			Text RemoteCommand = RemoteManagementCmdLine.GetComponent<Text>();
			switch (RemoteCommand.text)
			{
				case "BtnDownToUpOnClickEvent":
					{
						BtnDownToUpOnClickEvent();
					}
					break;

				case "BtnUpToDownOnClickEvent":
					{
						BtnUpToDownOnClickEvent();
					}
					break;

			}
			RemoteCommand.text = "";

		}
	}

	private void Accelerate(string action)
	{
		AccelerateStatus = action;
		switch (action)
		{
			case "Yes":
				{
					AccelerationForDownPanel = AccelerationForDownPanel + AccelerationForDownPanel;
				}
				break;
			case "SetDefaultVariables":
				{
					AccelerationForDownPanelDefault = AccelerationForDownPanel;
				}
				break;
			case "ReturnDefaultVariables":
				{
					AccelerationForDownPanel = AccelerationForDownPanelDefault;
				}
				break;
		}
	}

	private void SetSpeedForPanels(string action)
	{
		SpeedStatus = action;
		switch (action)
		{
			case "Yes":
				{
					SpeedForDownPanel =
						SpeedForDownPanelDefault * AccelerationForDownPanel * 1f;
				}
				break;
			case "SetDefaultVariables":
				{
					SpeedForDownPanelDefault = SpeedForDownPanel;
				}
				break;
			case "ReturnDefaultVariables":
				{
					SpeedForDownPanel = SpeedForDownPanelDefault;
				}
				break;
		}
	}

	void Start()
	{

	}

	public void BtnDownToUpOnClickEvent()
	{
		ActionStatus = "DownToUp";
		//HiddingButton
		if (UseAutoHidingButtons == true)
		{
			BtnDownToUp.SetActive(true);
			BtnUpToDown.SetActive(false);
		}

	}

	public void BtnUpToDownOnClickEvent()
	{
		ActionStatus = "UpToDown";
		//HiddingButton
		if (UseAutoHidingButtons == true)
		{
			BtnDownToUp.SetActive(false);
			BtnUpToDown.SetActive(true);
		}
	}

	/// <summary>
	/// Awake() - создает и устанавливает события onClick для элемента Button для объектов BtnLeftToRight и BtnRightToLeft.
	/// </summary>
	void Awake()
	{
		SetSpeedForPanels("SetDefaultVariables");
		Accelerate("SetDefaultVariables");
		RectTransform rectTransform = ScreenCanvas.GetComponent<RectTransform>();
		if (UseAutoSettingForTargets == true)
		{
			//((rectTransform.sizeDelta.x / 2) * 3) - правый бок экрана
			TargetForDownPanelB.transform.position =
				new Vector3(TargetForDownPanelB.transform.position.x, 0 - ((rectTransform.sizeDelta.y/2) - ShiftTarget_BDownPanelY));
		}

		//Creating events for buttons
		Button BtnUpToDownB = BtnUpToDown.GetComponent<Button>();
		BtnUpToDownB.onClick.AddListener(() => { BtnDownToUpOnClickEvent(); });
		//BtnUpToDown = BtnUpToDown.GetComponent<Button>();
		//BtnUpToDown.onClick.AddListener(() => { BtnDownToUpOnClickEvent(); });

		Button BtnDownToUpB = BtnDownToUp.GetComponent<Button>();
		BtnDownToUpB.onClick.AddListener(() => { BtnUpToDownOnClickEvent(); });
		//BtnDownToUp = BtnDownToUp.GetComponent<Button>();
		//BtnDownToUp.onClick.AddListener(() => { BtnUpToDownOnClickEvent(); });

		//Hiding Buttons
		if (UseAutoHidingButtons == true)
		{
			BtnDownToUp.SetActive(BtnDownToUpIsHiding);
			BtnUpToDown.SetActive(BtnUpToDownIsHiding);
		}

	
	}

	/// <summary>
	/// ChangePanelMaster() - Перемещает Panel's Object по If в Target со Speed и Acceleration
	/// </summary>
	public void ChangePanelMaster()
	{
		if (EnableScript)
		{
			switch (ActionStatus)
			{
				case "DownToUp":
					{
						SetSpeedForPanels("Yes");
						if (DownPanelObject.transform.position != TargetForDownPanelB.transform.position)
						{
							DownPanelObject.transform.position =
								Vector3.MoveTowards(DownPanelObject.transform.position, TargetForDownPanelB.transform.position, SpeedForDownPanel * 1f * Time.deltaTime);
						}
						else
						{
							if (DownPanelObject.transform.position == TargetForDownPanelB.transform.position)
							{
								ActionStatus = "none";
							}
						}
						Accelerate("Yes");
					}
					break;
				case "UpToDown":
					{
						SetSpeedForPanels("Yes");
						if (DownPanelObject.transform.position != TargetForDownPanelA.transform.position)
						{
							DownPanelObject.transform.position =
								Vector3.MoveTowards(DownPanelObject.transform.position, TargetForDownPanelA.transform.position, SpeedForDownPanel * 1f * Time.deltaTime);
						}
						else
						{
							if (DownPanelObject.transform.position == TargetForDownPanelA.transform.position)
							{
								ActionStatus = "none";
							}
						}
						Accelerate("Yes");
					}
					break;
				case "none":
					{
						//Action
						Accelerate("ReturnDefaultVariables");
						SetSpeedForPanels("ReturnDefaultVariables");
						//Status
						ActionStatus = "-";
						AccelerateStatus = "-";
						SpeedStatus = "-";
					}
					break;
			}
		}
	}

	void Update()
	{
		ChangePanelMaster();
		RemoteManagerUI();
	}
}
