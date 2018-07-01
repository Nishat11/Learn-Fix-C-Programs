using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Ingame_controller : MonoBehaviour {

	public GameObject Solution;
	public GameObject selected_PC;
	public GameObject Computer1, computer2, computer3;
	//Vector3 TargetPosition = new Vector3(0.37f,0.32f,0.26f);
	public GameObject refre;
	public bool swipe;
	public Text Intro_text;
	public List<GameObject> list_Intro_code = new List<GameObject>();
	public List<GameObject> questions = new List<GameObject>();
	public List<GameObject> list_Solution = new List<GameObject>();
	public List<GameObject> list_Hint = new List<GameObject>();
	public List<GameObject> Instruction = new List<GameObject>();

	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;

	//not used
	public List<GameObject> error_code = new List<GameObject>();
	public GameObject Main_canvas,IngameHUD_obj;
	public List<GameObject> Right_questions = new List<GameObject>();
	public int level_num;
	public GameObject Level_question;
	public int GameObject_1,GameObject_2,GameObject_3;
	//public bool Solution_Enable;
	//public GameObject solutionobject;
	// Use this for initialization
	void Start () 
	{
		Main_canvas =  GameObject.Find ("Canvas");
		level_num = Main_canvas.GetComponent<MainMenuScreenManager> ().Level_selected;
		Instruction [level_num].SetActive (true);
		list_Intro_code [level_num].SetActive (true);
		Level_question = questions [level_num];
		GameObject_1 = Random.Range (1,4);
//		Debug.Log ("value of random is...."+GameObject_1);
		if (GameObject_1 == 1) 
		{
			GameObject_2 = Random.Range(2,4);
			if(GameObject_2 == 2)
				GameObject_3 = 3;
			else
				GameObject_3 = 2;
		} 
		else if (GameObject_1 == 2) 
		{
			//2 for 1 in bracket
			GameObject_2 = Random.Range(2,4);
			if(GameObject_2 == 2)
			{
				GameObject_2 = 1;
				GameObject_3 = 3;
			}
			else
				GameObject_3 = 1;
		} 
		else 
		{
			GameObject_2 = Random.Range(1,3);
			if(GameObject_2 == 1)
				GameObject_3 = 2;
			else
				GameObject_3 = 1;
		}
		set_error_tag ();

		if(!PlayerPrefs.HasKey("Score"))
		{
			PlayerPrefs.SetInt("Score",0);
		}
		if(!PlayerPrefs.HasKey("Coins"))
		{
			PlayerPrefs.SetInt("Coins",0);
		}
		HUDIngame.instance.Score = PlayerPrefs.GetInt ("Score");
		HUDIngame.instance.Coins = PlayerPrefs.GetInt ("Coins");
	}

	public void set_error_tag()
	{
//		Debug.Log ("value is"+GameObject_1+"...for second"+GameObject_2+" for third"+GameObject_3);
		if (GameObject_1 == 3)
			Computer1.tag = "error";
		else if (GameObject_2 == 3)
			computer2.tag = "error";
		else 
			computer3.tag = "error";
	}
	public void Swipe()
	{
		if(Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began)
			{
				//save began touch 2d point
				firstPressPos = new Vector2(t.position.x,t.position.y);
			}
			if(t.phase == TouchPhase.Ended)
			{
				//save ended touch 2d point
				secondPressPos = new Vector2(t.position.x,t.position.y);
				
				//create vector from the two points
				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
				
				//normalize the 2d vector
				currentSwipe.Normalize();
				
				//swipe upwards
				if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
				{
					IngameHUD_obj.GetComponent<HUDIngame>().Solution_screen.SetActive(false);
					HUDIngame.instance.swipe_image.SetActive(false);
					Debug.Log("up swipe");
					selected_PC = computer2;
					swipe = true;
					Solution.transform.position =  Vector3.MoveTowards (Solution.transform.position,selected_PC.transform.position,Time.deltaTime*0.8f);
				}
				//swipe down
				if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
				{
					Debug.Log("down swipe");
				}
				//swipe left
				if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
				{
					IngameHUD_obj.GetComponent<HUDIngame>().Solution_screen.SetActive(false);
					HUDIngame.instance.swipe_image.SetActive(false);
					Debug.Log("left swipe");
					selected_PC = Computer1;
					swipe = true;
					Solution.transform.position =  Vector3.MoveTowards (Solution.transform.position,selected_PC.transform.position,Time.deltaTime*0.8f);
				}
				//swipe right
				if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
				{
					IngameHUD_obj.GetComponent<HUDIngame>().Solution_screen.SetActive(false);
					HUDIngame.instance.swipe_image.SetActive(false);
					Debug.Log("right swipe");
					selected_PC = computer3;
					swipe = true;
					Solution.transform.position =  Vector3.MoveTowards (Solution.transform.position,selected_PC.transform.position,Time.deltaTime*0.8f);
				}
			}
		}
	}
	public void Mouse_Swipe()
	{
		if(Input.GetMouseButtonDown(0))
		{
			//save began touch 2d point
			firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		}
		if(Input.GetMouseButtonUp(0))
		{
			//save ended touch 2d point
			secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
			
			//create vector from the two points
			currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
			
			//normalize the 2d vector
			currentSwipe.Normalize();
			
			//swipe upwards
			if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
			{
				IngameHUD_obj.GetComponent<HUDIngame>().Solution_screen.SetActive(false);
				HUDIngame.instance.swipe_image.SetActive(false);

//				Debug.Log("up swipe"+selected_PC);
				selected_PC = computer2;
				swipe = true;
				//Solution.transform.position =  Vector3.MoveTowards (Solution.transform.position,selected_PC.transform.position,Time.deltaTime*0.8f);
			}
			//swipe down
			if(currentSwipe.y < 0  && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
			{
//				Debug.Log("down swipe");
			}
			//swipe left
			if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
			{
				IngameHUD_obj.GetComponent<HUDIngame>().Solution_screen.SetActive(false);
				HUDIngame.instance.swipe_image.SetActive(false);
			//	Debug.Log("left swipe"+selected_PC);
				selected_PC = Computer1;
				swipe = true;
				//Solution.transform.position =  Vector3.MoveTowards (Solution.transform.position,selected_PC.transform.position,Time.deltaTime*0.8f);
			}
			//swipe right
			if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
			{
				IngameHUD_obj.GetComponent<HUDIngame>().Solution_screen.SetActive(false);
				HUDIngame.instance.swipe_image.SetActive(false);HUDIngame.instance.swipe_image.SetActive(false);
//				Debug.Log("right swipe"+selected_PC);
				selected_PC = computer3;
				swipe = true;
				//Solution.transform.position =  Vector3.MoveTowards (Solution.transform.position,selected_PC.transform.position,Time.deltaTime*0.8f);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(HUDIngame.instance.enable_swipe)
		{
		//	Debug.Log("swipe");
			Mouse_Swipe();
			//Swipe ();
		}
		//this.transform.position = Vector3.MoveTowards (this.transform.position,refre.transform.position,Time.deltaTime*0.1f);
		if(swipe && selected_PC != null && Solution.activeSelf)
			Solution.transform.position =  Vector3.MoveTowards (Solution.transform.position,selected_PC.transform.position,Time.deltaTime*0.8f);
	}
}
