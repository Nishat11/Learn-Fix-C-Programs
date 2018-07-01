using UnityEngine;
using System.Collections;

public class Solution : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Computer") 
		{
			this.gameObject.SetActive(false);
			HUDIngame.instance.Level_failed();
		}
		else if(other.tag == "error")
		{
			this.gameObject.SetActive(false);
			HUDIngame.instance.Level_complete();
		}
	}
}
