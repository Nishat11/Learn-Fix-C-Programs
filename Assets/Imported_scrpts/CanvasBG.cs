using UnityEngine;
using System.Collections;

public class CanvasBG : MonoBehaviour {


	private static bool createdBg =false;

	void Awake()
	{
		DontDestroyOnLoad(this);
		if (!createdBg) 
		{
			// this is the first instance - make it persist
			DontDestroyOnLoad(this.gameObject);
			createdBg = true;
		} 
		else 
		{
			// this must be a duplicate from a scene reload - DESTROY!
			Destroy(this.gameObject);
		} 

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
