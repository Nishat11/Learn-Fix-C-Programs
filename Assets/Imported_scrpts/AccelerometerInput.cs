using UnityEngine;
using System.Collections;

public class AccelerometerInput : MonoBehaviour 
{
	public GameObject ForGround;
	public GameObject BackGround;
	public GameObject ForGround1;
	public GameObject BackGround1;

	public Vector3 forGround_Pos;
	public Vector3 backGround_Pos;

	public Vector2 aPosition0; 
	public Vector2 aPosition1; 
	public Vector2 bPosition0; 
	public Vector2 bPosition1;
	
	public float currentTilt =0;
	private int ease = 25;
	private float maxMovement = 30	;
	
	// Use this for initialization
	void Start () 
	{
		forGround_Pos = ForGround.transform.localPosition;
		backGround_Pos = BackGround.transform.localPosition;

		aPosition0 = new Vector3(forGround_Pos.x-3, forGround_Pos.y,forGround_Pos.z);
		aPosition1 = new Vector3(forGround_Pos.x+3, forGround_Pos.y,forGround_Pos.z);
		bPosition0 = new Vector3(backGround_Pos.x-maxMovement, backGround_Pos.y,backGround_Pos.z);
		bPosition1 = new Vector3(backGround_Pos.x+maxMovement, backGround_Pos.y,backGround_Pos.z);
	}
	
	void OnGUI()
	{
		currentTilt = Input.acceleration.x;// = Mathf.Round(Input.acceleration.x);
		float Bg_Pos;
		//vickyfloat Fg_Pos;

		//Debug.Log ("ease ----"+ease+"background limit"+bPosition1.x+"local pos"+BackGround.transform.localPosition.x+"Time"+Time.timeScale);
		if(currentTilt<0)
		{
			//right means move left

			Bg_Pos = ((bPosition1.x - BackGround.transform.localPosition.x) / ease);

			BackGround.transform.Translate(-Bg_Pos*currentTilt,backGround_Pos.y,0);
			BackGround1.transform.Translate(-Bg_Pos*currentTilt,backGround_Pos.y,0);
			ForGround.transform.Translate(Bg_Pos*currentTilt,backGround_Pos.y,0);
			ForGround1.transform.Translate(Bg_Pos*currentTilt,backGround_Pos.y,0);

		}
		else if(currentTilt>0)
		{
			Bg_Pos = (bPosition0.x - BackGround.transform.localPosition.x) / ease;
			BackGround.transform.Translate(Bg_Pos*currentTilt,backGround_Pos.y,0);
			BackGround1.transform.Translate(Bg_Pos*currentTilt,backGround_Pos.y,0);
			ForGround.transform.Translate(-Bg_Pos*currentTilt,backGround_Pos.y,0);
			ForGround1.transform.Translate(-Bg_Pos*currentTilt,backGround_Pos.y,0);
		}
	}
}
