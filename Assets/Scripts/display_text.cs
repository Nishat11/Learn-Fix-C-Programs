using UnityEngine;
using System.Collections;

public class display_text : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI()
	{
		GUI.Label (new Rect (this.gameObject.transform.localPosition.x,this.gameObject.transform.localPosition.y, 100, 100), "hiiii");
	}
}
