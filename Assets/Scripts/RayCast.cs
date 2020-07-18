using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RayCast : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public Text Text;
    Camera camera;
	// Update is called once per frame
	void Update () {
        foreach (Touch touch in Input.touches)
        {
            int pointerID = touch.fingerId;
            if (EventSystem.current.IsPointerOverGameObject(pointerID))
            {
                // at least on touch is over a canvas UI
                Text.text = "Is is Canvas";
                Debug.Log("Canvas");
            }

            if (touch.phase == TouchPhase.Ended)
            {
                // here we don't know if the touch was over an canvas UI
                Text.text = "It isn't Canvas";
                Debug.Log("It isn't Canvas");
            }
        }
        
    }

    void testss()
    {

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            camera = GetComponent<Camera>();
            Ray raycast = camera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Text.text = raycastHit.collider.name;
                Debug.Log(raycastHit.collider.name);
                if (raycastHit.collider.name == "Soccer")
                {
                    Debug.Log("Soccer Ball clicked");
                    Text.text = "Soccer Ball clicked";
                }

                //OR with Tag

                if (raycastHit.collider.CompareTag("SoccerTag"))
                {
                    Debug.Log("Soccer Ball clicked");
                }
            }
        }
    }
}
