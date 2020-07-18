using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CMR1 : MonoBehaviour
{
    [Header("Camera")]
    public Transform TargetForCamera;
    public Camera CameraObj;

    [Header("CameraLimit")]
    public float LeftLimitX = -360f;
    public float RightLimitX = 360f;
    public float UpLimitY = 70f;
    public float DownLimitY = 0f;
    public Text text;

    [Header("CameraRotate")]
    public float SenseRotate = 0.1f;

    [Header("CameraZoom")]
    public float ZoomMin;
    public float ZoomMax;
    public float zoomModifierSpeed = 0.1f;

    private Vector3 _offset; //смещение камеры относительно объекта
    float _rotY, _rotX; //по x, y, зум, координаты для обзора
    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;

    Vector2 firstTouchPrevPos, secondTouchPrevPos;

    void Start()
    {
        _rotY = transform.eulerAngles.y;
        _rotX = transform.eulerAngles.x;
        _offset = TargetForCamera.position - transform.position; //получает начальное смещение
        LookAtTarget();
    }

    void LookAtTarget()
    {
        Quaternion rotation = Quaternion.Euler(_rotY, _rotX, 0); //задает вращение камеры 
        transform.position = TargetForCamera.position - (rotation * _offset);
        transform.LookAt(TargetForCamera);
    }

    void LateUpdate()
    {
        text.text = transform.rotation.y.ToString();
        float input = Input.GetAxis("Mouse ScrollWheel");

        if (Input.touchCount == 1)
        {
            // GET TOUCH 0
            Touch touch0 = Input.GetTouch(0);

            // APPLY ROTATION
            if (touch0.phase == TouchPhase.Moved)
            {
                //target.transform.Rotate(touch0.deltaPosition.y, touch0.deltaPosition.x * Sens, 0f);
                _rotY = touch0.deltaPosition.y * SenseRotate;
                _rotX = touch0.deltaPosition.x * SenseRotate; //поворот камеры вокруг объекта и сохранение координат
              
                LookAtTarget();

                //Text.text = touch0.deltaPosition.y.ToString() + " " + touch0.deltaPosition.x.ToString();
            }

        }

        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

            zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

            if (touchesPrevPosDifference > touchesCurPosDifference)
            {
                if (CameraObj.fieldOfView < ZoomMax)
                {
                    CameraObj.fieldOfView += zoomModifier * 0.1f;
                }
            }
            if (touchesPrevPosDifference < touchesCurPosDifference)
            {
                if (CameraObj.fieldOfView > ZoomMin)
                {
                    CameraObj.fieldOfView -= zoomModifier * 0.1f;
                }
            }

        }
    }
}