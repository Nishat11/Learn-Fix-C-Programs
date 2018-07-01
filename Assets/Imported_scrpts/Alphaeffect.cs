using UnityEngine;
using System.Collections;

public class Alphaeffect : MonoBehaviour 
{
	private float alphacount;
	private bool called;
	// Use this for initialization
	void Start () 
	{
		called = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(called && this.gameObject.GetComponent<CanvasGroup>().alpha <=1)
		{
			this.gameObject.GetComponent<CanvasGroup>().alpha+=Time.deltaTime*4;
		}
		else if(!called && this.gameObject.GetComponent<CanvasGroup>().alpha>=0)
		{
			this.gameObject.GetComponent<CanvasGroup>().alpha-=Time.deltaTime*4;
			if(this.gameObject.GetComponent<CanvasGroup>().alpha<=0)
				this.gameObject.SetActive(false);
		}
	}

	public void Back()
	{
		called = false;
	}

	void OnEnable()
	{
		called = true;
	}
	void OnDisable()
	{
		this.gameObject.GetComponent<CanvasGroup>().alpha=0;
		called = false;
	}
	            
}
