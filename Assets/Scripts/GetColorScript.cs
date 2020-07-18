using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GetColorScript : MonoBehaviour {

	public Text ColorTextObj;
	public Renderer renderer;
	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		renderer.material.color = ColorTextObj.color;
	}
}
