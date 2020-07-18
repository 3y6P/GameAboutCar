using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CamRotScript : MonoBehaviour {

	[Header("Conditions")]
	public bool EnableRotation = false;
	public bool EnableZoom;

	[Header("RemoteManager")]
	public bool UseRemoteManagement = false;
	public GameObject RemoteManagementCmdLine;

	[Header("CameraAndTraget")]
	public Camera CameraObj;
	public Transform target;

	[Header("Offset")]
	public Vector3 offset;

	[Header("Rotate")]
	public float sensitivity = 0.1f; // чувствительность мышки
	public float limit = 80; // ограничение вращения по Y
	public float limit1 = 10;
	public float limit2 = 70;
	public float limit3 = 70;

	[Header("Zoom")]
	public float zoom = 0.01f; // чувствительность при увеличении, колесиком мышки
	public float zoomMax = 10f; // макс. увеличение
	public float zoomMin = 10f; // мин. увеличение
	private float X, Y;

	float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;
	Vector2 firstTouchPrevPos, secondTouchPrevPos;

	public void RemoteManagerUI()
	{
		if (UseRemoteManagement == true)
		{
			Text RemoteCommand = RemoteManagementCmdLine.GetComponent<Text>();
			switch (RemoteCommand.text)
			{
				case "EnableFreeCamera":
					{
						EnableRotation = true;
					}
					break;

				case "DisableFreeCamera":
					{
						EnableRotation = false;
					}
					break;

			}
			RemoteCommand.text = "";

		}
	}

	public void CamRotate()
	{

		if (EnableRotation == true)
		{
			if (Input.touchCount == 1)
			{

				// GET TOUCH 0
				Touch touch0 = Input.GetTouch(0);

				// APPLY ROTATION
				if (touch0.phase == TouchPhase.Moved)
				{
					//cube.transform.Rotate(touch0.deltaPosition.y, touch0.deltaPosition.x, 0f);

					//X = transform.localEulerAngles.y + touch0.deltaPosition.x * sensitivity;
					X = CameraObj.transform.localEulerAngles.y + touch0.deltaPosition.x * sensitivity;


					Y += touch0.deltaPosition.y * sensitivity;
					Y = Mathf.Clamp(Y, limit1, limit);

					CameraObj.transform.localEulerAngles = new Vector3(-Y, X, 0);
					CameraObj.transform.position = CameraObj.transform.localRotation * offset + target.position;


				}

			}
		}
	}

	void Start()
	{
		limit = Mathf.Abs(limit);
		if (limit > 90) limit = 90;
		offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 2);
		CameraObj.transform.position = target.position + offset;
	}



	void Update()
	{
		RemoteManagerUI();
		foreach (Touch touch in Input.touches)
		{
			int pointerID = touch.fingerId;
			if (EventSystem.current.IsPointerOverGameObject(pointerID))
			{
				EnableRotation = false;
			}

			if (touch.phase == TouchPhase.Ended)
			{
				EnableRotation = true;
			}
		}

		CamRotate();
		Touch firstTouch = Input.GetTouch(0); ;
		Touch secondTouch = Input.GetTouch(1); ;

		if (Input.touchCount == 2)
		{
			firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
			secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

			touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
			touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

			zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoom;

			if (touchesPrevPosDifference > touchesCurPosDifference)
			{
				if (CameraObj.fieldOfView < 100)
				{
					CameraObj.fieldOfView += zoomModifier * 0.1f;
				}
			}
			if (touchesPrevPosDifference < touchesCurPosDifference)
			{
				if (CameraObj.fieldOfView > 10)//70
				{
					CameraObj.fieldOfView -= zoomModifier * 0.1f;
				}
			}

		}
	}
}
