using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Advertisements;
public class HUDIngame : MonoBehaviour 
{

	public GameObject GameComplete_Screen;
	public GameObject GameFaild_Screen;
	public GameObject WholeGameCompleted;
	public GameObject Pause_Screen;
	public GameObject Ingame_Controller;
	public GameObject Instruction_screen;
	public GameObject Hint_warning;


	public Text GameFaildLife;
	public bool Retry;
	public GameObject Intro_screen;
	public GameObject Questopn_screen;
	public GameObject Hint_screen;
	public int Random_value_1,Random_value_2,Random_value_3;
	public GameObject Hint_button,Solution_screen,Hint_Cost;

	private const string GAMEFAILED_RETRY = "GameFailed_Retry";
	private const string GAMEFAILED_HOME = "GameFailed_Home";
	private const string GAMECOMPLETE_COUTINUE= "GameComplete_Continue";
	private const string GAMEPAUSE_COUTINUE= "Pause_Continue";
	private const string GET_CERTI = "GameComplete_Playagain";
	private const string QUESTION_BACK = "Question_Back";
	private const string HINT_BUTTON = "Hint";
	private const string HINT_BACK = "Hint_Back";
	private const string HINT_NO_COINS_BACK = "Hint_warning_Back";
	private const string HINT_ALERT_CANCEL = "Hint_Cancel";
	private const string HINT_ALERT_OK = "Hint_AlertOK";


	
	private const string GAMEPAUSE_RETRY = "Retry";
	private const string CODE_COMPUTER_1 = "CheckCode_1";
	private const string CODE_COMPUTER_2 = "CheckCode_2";
	private const string CODE_COMPUTER_3 = "CheckCode_3";
	private const string INTRO_COUNTINUE = "Intro_Countinue";
	private const string INSTRUCTION_COUNTINUE= "Instruction_Countinue";
	private const string GAMEPAUSE_HOME = "Home";
	private const string GAME_PAUSE= "Pause";
	public static HUDIngame instance;
	public bool IngameInapp;
	public bool Check1, check2,check3;
	public bool enable_swipe;
	public bool countinue;
	public int Selected_level_num;
	public GameObject solutionobject;
	public float time_count;
	public Text time_value, coins_value,Score_value;
	public GameObject ingame_instruction;
	public int Score, Coins;
	public Text Complete_Score,complete_coins;
	public GameObject swipe_image;
	public bool Hint_open;
	// Use this for initialization
	void Start () 
	{
		Advertisement.Initialize("1110015", false);
		//125349
		instance = this;
		time_count = 0;
		Score_value.text = ": "+PlayerPrefs.GetInt("Score");
		coins_value.text = ": "+PlayerPrefs.GetInt("Coins");
		//Advertisement.Initialize("125349", false);
	}

	public void Restart()
	{
		Instruction_screen.SetActive(true);
		if (Instruction_screen.activeSelf)
			Debug.Log ("its true");
	}
	public void Reset()
	{
		Random_value_1 = GameObject.Find("Main Camera").GetComponent<Ingame_controller>().GameObject_1;
		Random_value_2 = GameObject.Find("Main Camera").GetComponent<Ingame_controller>().GameObject_2;
		Random_value_3 = GameObject.Find("Main Camera").GetComponent<Ingame_controller>().GameObject_3;
		GameObject.Find("Main Camera").GetComponent<Ingame_controller>().Level_question.gameObject.transform.GetChild(Random_value_1-1).gameObject.SetActive(false);
		GameObject.Find("Main Camera").GetComponent<Ingame_controller>().Level_question.gameObject.transform.GetChild(Random_value_2-1).gameObject.SetActive(false);
		GameObject.Find("Main Camera").GetComponent<Ingame_controller>().Level_question.gameObject.transform.GetChild(Random_value_3-1).gameObject.SetActive(false);
	}
	public IEnumerator ShowAdWhenReady()
	{
		while (!Advertisement.isReady())
			yield return null;
		
		Advertisement.Show();
	}

	// Update is called once per frame
	void Update () 
	{
//		Debug.Log("swipe is "+enable_swipe);
		if (countinue) 
		{
			time_count = time_count + Time.deltaTime;
			//Debug.Log ("time is" + (int)time_count);
			time_value.text = "" + (int)time_count;
		}
		if(MainMenuScreenManager.instance.gamefailed_Complete && GameComplete_Screen.GetComponent<CanvasGroup>().alpha == 1)
		{
			//Time.timeScale =0f;
		}
		if(MainMenuScreenManager.instance.gamefailed_Complete && GameFaild_Screen.GetComponent<CanvasGroup>().alpha == 1)
		{
			//Time.timeScale =0f;
		}
	}
	public void Level_failed()
	{
		GameFaild_Screen.SetActive(true);
		countinue = false; 
		//yield return new WaitForSeconds(0);
		MainMenuScreenManager.instance.SoundObject.GetComponent<AudioSource> ().Pause ();
		//SoundManager.instance.StopAllSounds();
		MainMenuScreenManager.instance.gamefailed_Complete =true;
		GameFaild_Screen.SetActive (true);
	}
	public void Level_complete()
	{
		calculate_points ();
		GameComplete_Screen.SetActive(true);
		countinue = false;

		//Pause all sound
		MainMenuScreenManager.instance.SoundObject.GetComponent<AudioSource> ().Pause ();
		MainMenuScreenManager.instance.gamefailed_Complete =true;
		//Level Nunmber increase for next level and store in player prefs........
		if(MainMenuScreenManager.instance.LevelNum<10)
		{
			//MainMenuScreenManager.instance.LevelNum++;
//			Debug.Log("no is ................."+MainMenuScreenManager.instance.Level_selected);
//			Debug.Log("level no is ................."+PlayerPrefs.GetInt("Level_No"));
			switch(MainMenuScreenManager.instance.Level_selected)
			{
				case 0:
				if(PlayerPrefs.GetInt("Level_No")< 1)
					PlayerPrefs.SetInt("Level_No",1);
				Debug.Log("level no is ..........new...."+PlayerPrefs.GetInt("Level_No"));
				break;			
				case 1:
					if(PlayerPrefs.GetInt("Level_No")< 2)
						PlayerPrefs.SetInt("Level_No",2);
				break;
				case 2:
					if(PlayerPrefs.GetInt("Level_No")< 3)
						PlayerPrefs.SetInt("Level_No",3);
				break;
				case 3:
					if(PlayerPrefs.GetInt("Level_No")< 4)
						PlayerPrefs.SetInt("Level_No",4);
				break;
				case 4:
					if(PlayerPrefs.GetInt("Level_No")< 5)
						PlayerPrefs.SetInt("Level_No",5);
				break;
				case 5:
				if(PlayerPrefs.GetInt("Level_No")< 6)
					PlayerPrefs.SetInt("Level_No",6);
				break;
				case 6:
					if(PlayerPrefs.GetInt("Level_No")< 7)
						PlayerPrefs.SetInt("Level_No",7);
				break;
				case 7:
					if(PlayerPrefs.GetInt("Level_No")< 8)
						PlayerPrefs.SetInt("Level_No",8);
				break;
				case 8:
					if(PlayerPrefs.GetInt("Level_No")< 9)
						PlayerPrefs.SetInt("Level_No",9);
				break;
				case 9:
					if(PlayerPrefs.GetInt("Level_No")< 10)
						PlayerPrefs.SetInt("Level_No",10);
				break;

			}
			MainMenuScreenManager.instance.LevelNum = PlayerPrefs.GetInt("Level_No");
			//PlayerPrefs.SetInt("Level_No",MainMenuScreenManager.instance.LevelNum);
		}
		else
		{
			//tenth level completed.....
			if(PlayerPrefs.GetInt("GameComplete")==0)
			{
				PlayerPrefs.SetInt("GameComplete",1);
			}
		}
	}
	public void calculate_points()
	{
		Score =Score +((int)(1000/(int)time_count));
		Coins =Coins + ((int)50/(int)time_count);
		complete_coins.text = coins_value.text = ": " +Coins;
		Complete_Score.text = Score_value.text= ": "+Score;
//		Debug.Log ("score is "+Score + "coins is "+Coins);
		PlayerPrefs.SetInt ("Score",Score);
		PlayerPrefs.SetInt ("Coins",Coins);
	}
	public void Inst_off()
	{
		ingame_instruction.SetActive (false);
	}

	public IEnumerator Swipeon()
	{
		yield return new WaitForSeconds(0.6f);
		if (Solution_screen.activeSelf) 
		{
			enable_swipe = true;
		}
	}
	public void MainMenu_sound(string soundname)
	{
		if (soundname == "Mainmenu") {
			MainMenuScreenManager.instance.SoundObject.GetComponent<AudioSource> ().clip = MainMenuScreenManager.instance.Mainmenu_sound;
			MainMenuScreenManager.instance.SoundObject.GetComponent<AudioSource> ().Play ();
		} else 
		{
			MainMenuScreenManager.instance.SoundObject.GetComponent<AudioSource> ().clip = MainMenuScreenManager.instance.Ingame_sound;
			MainMenuScreenManager.instance.SoundObject.GetComponent<AudioSource> ().Play ();
		}
	}

	public void HudButtonClick(GameObject HudbtnGO)
	{
		switch (HudbtnGO.name) 
		{
		
		case QUESTION_BACK:
			Questopn_screen.SetActive(false);
			if(Check1 && check2 && check3)
			{
				if(MainMenuScreenManager.instance.Level_selected < 8)
					Hint_button.SetActive(true);
				Solution_screen.SetActive(true);
				swipe_image.SetActive(true);
				solutionobject.SetActive(true);
				Selected_level_num = GameObject.Find ("Canvas").GetComponent<MainMenuScreenManager> ().Level_selected;
				Solution_screen.transform.GetChild(Selected_level_num).gameObject.SetActive(true);
				//enable_swipe =true;
				StartCoroutine(Swipeon());
			}
			Reset();
			break;
		case GAME_PAUSE:
			enable_swipe = false;
			Pause_Screen.SetActive(true);
			Time.timeScale = 0;
			Pause_Screen.GetComponent<CanvasGroup>().alpha = 1;
			MainMenuScreenManager.instance.SoundObject.GetComponent<AudioSource> ().Pause ();
			break;

		case HINT_BUTTON:
			if(!Hint_open)
			{
				Hint_Cost.SetActive(true);
				enable_swipe = false;
			}
			else
			{
				enable_swipe = false;
				Hint_screen.SetActive(true);
				Selected_level_num = GameObject.Find ("Canvas").GetComponent<MainMenuScreenManager> ().Level_selected;
				Hint_screen.transform.GetChild(Selected_level_num+1).gameObject.SetActive(true);
			}
			break;
		case HINT_NO_COINS_BACK:
			Hint_warning.SetActive(false);
			StartCoroutine( Swipeon());
			break;
		case HINT_BACK:
			Hint_screen.SetActive(false);
			StartCoroutine(Swipeon());
			Hint_screen.transform.GetChild(Selected_level_num+1).gameObject.SetActive(false);
			break;
		case HINT_ALERT_CANCEL:
			Hint_Cost.SetActive(false);
			StartCoroutine(Swipeon());
			break;
		case HINT_ALERT_OK:
			Hint_Cost.SetActive(false);
			Debug.Log("Coiins"+PlayerPrefs.GetInt("Coins"));
			if(PlayerPrefs.GetInt("Coins") >= 5 && !Hint_open)
			{
				enable_swipe = false;
				Hint_screen.SetActive(true);
				Selected_level_num = GameObject.Find ("Canvas").GetComponent<MainMenuScreenManager> ().Level_selected;
				Hint_screen.transform.GetChild(Selected_level_num+1).gameObject.SetActive(true);
				int temp = PlayerPrefs.GetInt ("Coins");
				PlayerPrefs.SetInt("Coins",temp-5);
				Score_value.text = ": "+PlayerPrefs.GetInt("Score");
				coins_value.text = ": "+PlayerPrefs.GetInt("Coins");
				Hint_open = true;
			}
			else if(Hint_open)
			{
				enable_swipe = false;
				Hint_screen.SetActive(true);
				Selected_level_num = GameObject.Find ("Canvas").GetComponent<MainMenuScreenManager> ().Level_selected;
				Hint_screen.transform.GetChild(Selected_level_num+1).gameObject.SetActive(true);
			}
			else
			{
				Hint_warning.SetActive(true);
				enable_swipe = false;
			}
			break;
		case GAMEFAILED_HOME:
			StartCoroutine(ShowAdWhenReady());
			Time.timeScale = 1;
			//MainMenuScreenManager.instance.gamefailed_Complete =false;
			//MainMenuScreenManager.instance.LevelSelector(MainMenuScreenManager.instance.LevelNum);
			//GameFaild_Screen.SetActive(false);
			GameFaild_Screen.GetComponent<Alphaeffect>().Back();
			MainMenuScreenManager.instance.Mainmenu_Screen.SetActive(true);
			MainMenuScreenManager.instance.Mainmenu_Screen_Bg.SetActive(true);
			Application.LoadLevel("MainMenu");
			MainMenuScreenManager.instance.SoundObject.GetComponent<AudioSource>().clip = MainMenuScreenManager.instance.Mainmenu_sound;
			MainMenuScreenManager.instance.SoundObject.GetComponent<AudioSource>().Play();
			MainMenu_sound("Mainmenu");
			break;
		case GAMEFAILED_RETRY:
			StartCoroutine(ShowAdWhenReady());
			Application.LoadLevel("Ingame");
			Time.timeScale =1;
			MainMenu_sound("ingame");
			break;
		case INTRO_COUNTINUE :
			Intro_screen.SetActive (false);
			countinue = true;
			ingame_instruction.SetActive(true);
			Invoke("Inst_off",15f);
			break;
		case INSTRUCTION_COUNTINUE :
			Instruction_screen.SetActive(false);
			Intro_screen.SetActive (true);
			break;
		case GAMECOMPLETE_COUTINUE:

			StartCoroutine(ShowAdWhenReady());
			if(PlayerPrefs.GetInt("Level_No") < 10)
			{
				Application.LoadLevel("Mainmenu");
				Time.timeScale= 1;
				GameComplete_Screen.SetActive(false);
				MainMenuScreenManager.instance.gamefailed_Complete =false;
				MainMenuScreenManager.instance.Level_selection.SetActive(true);
				MainMenuScreenManager.instance.Manage_lock();
				MainMenu_sound("Mainmenu");
				if (PlayerPrefs.GetInt ("Level_No") > 0)
					MainMenuScreenManager.instance.Countinue_button.SetActive (true);
			}
			else
			{
				Time.timeScale= 1;
				//Application.LoadLevel("Mainmenu");
				MainMenuScreenManager.instance.IngameHUD.SetActive(true);
				GameComplete_Screen.SetActive(false);
				if(PlayerPrefs.GetInt("Certificate") == 0)
				{
					WholeGameCompleted.SetActive(true);
					PlayerPrefs.SetInt("Certificate",1);
				}
				else
				{
					MainMenuScreenManager.instance.gamefailed_Complete =false;
					MainMenuScreenManager.instance.Level_selection.SetActive(true);
					MainMenuScreenManager.instance.Manage_lock();
					MainMenu_sound("Mainmenu");
				}
			}
			break;

		case CODE_COMPUTER_1:
			Questopn_screen.SetActive(true);
			Random_value_1 = GameObject.Find("Main Camera").GetComponent<Ingame_controller>().GameObject_1;
			GameObject.Find("Main Camera").GetComponent<Ingame_controller>().Level_question.gameObject.transform.GetChild(Random_value_1-1).gameObject.SetActive(true);
			Check1 = true;
			Inst_off();
			Solution_screen.SetActive(false);
			swipe_image.SetActive(false);
			enable_swipe = false;
			break;
		case CODE_COMPUTER_2:
			Questopn_screen.SetActive(true);
			Random_value_2 = GameObject.Find("Main Camera").GetComponent<Ingame_controller>().GameObject_2;
			GameObject.Find("Main Camera").GetComponent<Ingame_controller>().Level_question.transform.GetChild(Random_value_2-1).gameObject.SetActive(true);	
			check2 = true;
			Inst_off();
			Solution_screen.SetActive(false);
			swipe_image.SetActive(false);
			enable_swipe = false;
			break;
		case CODE_COMPUTER_3:
			Questopn_screen.SetActive(true); 
			Random_value_3 = GameObject.Find("Main Camera").GetComponent<Ingame_controller>().GameObject_3;
			GameObject.Find("Main Camera").GetComponent<Ingame_controller>().Level_question.transform.GetChild(Random_value_3-1).gameObject.SetActive(true);
			check3 = true;
			Inst_off();
			Solution_screen.SetActive(false);
			swipe_image.SetActive(false);
			enable_swipe = false;
			break;
		case GET_CERTI:
			if(PlayerPrefs.GetString("Firstname") == "" )
				MainMenuScreenManager.instance.Userinfo_screen.SetActive(true);
			Time.timeScale= 1;
			//MainMenuScreenManager.instance.LevelSelector(1);
			WholeGameCompleted.GetComponent<Alphaeffect>().Back();
			MainMenuScreenManager.instance.gamefailed_Complete =false;

			//MainMenuScreenManager.instance.Mainmenu_Screen.SetActive(true);
			//MainMenuScreenManager.instance.Level_selection.SetActive(true);
			//MainMenu_sound("Mainmenu");
			//Application.LoadLevel("Mainmenu");
			break;

		case GAMEPAUSE_COUTINUE:
			Time.timeScale =1;
			Pause_Screen.SetActive(false);
			Time.timeScale =1;
			StartCoroutine(Swipeon());
			MainMenuScreenManager.instance.SoundObject.GetComponent<AudioSource> ().Play ();
			break;
		case GAMEPAUSE_HOME:
			Pause_Screen.SetActive(false);
			Application.LoadLevel("MainMenu");
			GameObject.Find("Canvas").GetComponent<MainMenuScreenManager>().Mainmenu_Screen.SetActive(true);
			Time.timeScale =1;
			Retry = false;
			MainMenu_sound("Mainmenu");
			break;
		case GAMEPAUSE_RETRY:
			Application.LoadLevel(Application.loadedLevel);
			Time.timeScale =1;
			Pause_Screen.SetActive(false);
			Retry = true;
			Restart();
			MainMenu_sound("ingame");
			break;
		}
	}

}
