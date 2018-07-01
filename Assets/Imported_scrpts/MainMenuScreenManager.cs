using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.UI;

using System.Collections.Generic;
//using LitJson;
using UnityEngine.iOS;

public class MainMenuScreenManager : MonoBehaviour { 
	public static MainMenuScreenManager instance;

	// Use this for initialization
	private static bool created =false;

	private const string STR_BTN_PLAY = "Play";
	private const string STR_BTN_SETTING = "Setting";
	private const string STR_BTN_COUNTINUE = "Countinue";
	private const string STR_BTN_CREDITS = "Credits";
	private const string STR_BTN_VIDEO = "Video_button";
	private const string STR_BTN_SETTING_BACK= "Setting_Back";
	private const string STR_BTN_CREDITS_BACK= "Credits_Back";
	private const string STR_BTN_LEVEL_SELECTION_BACK = "Back";
	private const string STR_BTN_HELP = "Help_Btn";
	private const string STR_BTN_HELP_BACK ="Help_Back";
	private const string STR_BTN_OBJECTIVE_COUNTINUE = "Objective_countinue";
	private const string STR_BTN_SUBMIT_NAME = "Submit_name"; 
	private const string STR_BTN_SKIP_NAME = "Skip";
	private const string STR_BTN_COUNTINUE_CERTI = "certi_countinue";
	private const string STR_BTN_SHOW_CERTI = "Show_certi";


	public List <GameObject> Btn_Level = new List<GameObject>();
	public List<string> Tips = new List<string>();
	//public List<GameObject> Intro_screen = new List<GameObject>();

	public GameObject SoundObject; 
	public bool QuitPopup;
	public InputField firstname;
	public InputField Lastname;
	public GameObject Setting_Screen;
	public GameObject Credits_Screen;
	public GameObject Mainmenu_Screen;
	public GameObject Wrong_name;
	public GameObject Mainmenu_Screen_Bg;
	public GameObject Level_selection;
	public GameObject LoadingScreen;
	public GameObject ObjectiveScreen;
	public GameObject IntroScreen;
	public GameObject Userinfo_screen;
	public GameObject Certificate_screen;
	public GameObject IngameHUD;
	public GameObject Help_screen;
	public GameObject Countinue_button;
	public Text Certi_name, certi_score;
	public GameObject Show_certi_btn;

	public GameObject Quit_Screen;
	public bool gamefailed_Complete;

	public RawImage Bg_image;
	public RawImage Info_image;

	public bool Current1;
	public bool isLoading;	
	public int LevelNum;
	public int Level_selected;
	public int Levelstatus;
	public AudioClip Mainmenu_sound,Ingame_sound;

	//public WebCamTexture mCamera = null;
	public GameObject plane_obj;
	
	void Awake()
	{

		#if UNITY_IPHONE
			Share_Button.SetActive(false);
		#endif

		IngameHUD =null;
		DontDestroyOnLoad(this);
		if (!created) 
		{
			// this is the first instance - make it persist
			DontDestroyOnLoad(this.gameObject);
			created = true;
		} 
		else 
		{
			// this must be a duplicate from a scene reload - DESTROY!
			Destroy(this.gameObject);
		} 
	}
	
	void Start () 
	{

		//mCamera = new WebCamTexture ();
		//plane_obj = GameObject.FindWithTag ("transperent");
		//plane_obj.GetComponent<CanvasRenderer> ().GetMaterial().mainTexture = mCamera; 
		//mCamera.Play ();
        
		//PlayerPrefs.SetInt("Coins",100);
		instance = this;
		//PlayerPrefs.DeleteAll();
		//PlayerPrefs.SetInt ("Level_No",8);
		//PlayerPrefs.SetInt ("Coins",1000);
		//PlayerPrefs.SetInt("Certificate",0);
		//Check new user for video play and other first time stuff......
		if(!PlayerPrefs.HasKey("NewUser"))
			PlayerPrefs.SetInt("NewUser",0);

		if(!PlayerPrefs.HasKey("GameComplete"))
		{
			PlayerPrefs.SetInt("GameComplete",0);
		}
		if(!PlayerPrefs.HasKey("Certificate"))
		{
			PlayerPrefs.SetInt("Certificate",0);
		}
        if (!PlayerPrefs.HasKey("Firstname"))
        {
			PlayerPrefs.SetString("Firstname", "");
        }
		if (!PlayerPrefs.HasKey("Lastname"))
		{
			PlayerPrefs.SetString("Lastname", "");
		}

		if(!PlayerPrefs.HasKey("Level_No"))
			PlayerPrefs.SetInt("Level_No",0);
			
		LevelNum = PlayerPrefs.GetInt("Level_No");

		if(PlayerPrefs.GetInt("NewUser") == 0)
		{
			StartCoroutine(MenuStart());
			PlayerPrefs.SetInt("NewUser",1);
		}
		else
		{
			StartCoroutine(MenuStart());
		}
		#if UNITY_IOS
		if(UnityEngine.iOS.Device.generation == DeviceGeneration.iPhone5C || 
		   UnityEngine.iOS.Device.generation == DeviceGeneration.iPhone5 ||
		   UnityEngine.iOS.Device.generation == DeviceGeneration.iPhone5S || 
		   UnityEngine.iOS.Device.generation == DeviceGeneration.iPhone6 ||
		   UnityEngine.iOS.Device.generation == DeviceGeneration.iPhone6Plus
		   )
			GameObject.Find("Canvas_BG").GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
		#else
//			Debug.Log("Canvas bg 0");
			//GameObject.Find("Canvas_BG").GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
		#endif
	}


	public void Mute()
	{
		if(PlayerPrefs.GetInt("Sound") == 1)
		{
			AudioListener.pause = true;
			SettingScreen_Controller.instance.Soundoff.SetActive(true);
			SettingScreen_Controller.instance.SoundOn.SetActive(false);
		}
		else
		{
			AudioListener.pause = false;
			SettingScreen_Controller.instance.Soundoff.SetActive(false);
			SettingScreen_Controller.instance.SoundOn.SetActive(true);
		}
	}

	public IEnumerator MenuStart()
	{
		yield return new WaitForSeconds(0);
		Mute();
		//LevelSelector(LevelNum);
		IntroScreen.SetActive(false);
		Mainmenu_Screen_Bg.SetActive(true);
		Manage_lock ();
//		Debug.Log ("Certificate no"+PlayerPrefs.GetInt ("Certificate"));
		if (PlayerPrefs.GetInt ("Certificate") == 1)
			Show_certi_btn.SetActive (true);
//		Debug.Log ("level no"+PlayerPrefs.GetInt ("Level_No"));
		if (PlayerPrefs.GetInt ("Level_No") > 0)
			Countinue_button.SetActive (true);
		//SoundObject.GetComponent<AudioSource>().Play();
	}
	public IEnumerator HideName_error()
	{
		yield return new WaitForSeconds(3);
		Wrong_name.SetActive(false);
	}
  
	
	public IEnumerator Fadeout(GameObject Invisible,GameObject NextSceen)
	{
		yield return new WaitForSeconds(3);
		NextSceen.SetActive(true);
	}
  	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{ 
			if(Application.loadedLevelName != "MainMenu" && isLoading ==false) 
			{
				if(IngameHUD != null)
				{
					//if(SoundHandler.instance != null)
					//{
						//SoundHandler.instance.PauseAllSounds();
					//}
					//GameTracker.Instance.PauseTimer();
					HUDIngame.instance.enable_swipe = false;
					HUDIngame.instance.Pause_Screen.SetActive(true);
					//Debug.Log("pause from escape"+gamefailed_Complete);
					//IngameHUD.SetActive(false);
					IngameHUD.GetComponent<HUDIngame>().Pause_Screen.SetActive(true);
					IngameHUD.GetComponent<HUDIngame>().Pause_Screen.GetComponent<CanvasGroup>().alpha = 1;
				}
				Time.timeScale=0;
			}
			else if(!gamefailed_Complete && isLoading ==false)
			{
				if(!QuitPopup)
				{
					Quit_Screen.SetActive(true);
					QuitPopup = true;
				}
				else
				{
					//Quit_Screen.SetActive(false);
					Quit_Screen.GetComponent<Alphaeffect>().Back();
					QuitPopup = false;
				}

			}
		}

		if(Application.loadedLevelName != "MainMenu" && IngameHUD == null)
		{
			IngameHUD = GameObject.Find("Ingame_HUD");
			if(LoadingScreen.activeInHierarchy == true && IngameHUD !=null)
				IngameHUD.SetActive(false);
		}
	}
	void OnApplicationQuit()
	{

	}

	public void OnApplicationPause()
	{
		if(Application.loadedLevelName != "MainMenu" && Application.loadedLevelName != "StartUpScene" && !gamefailed_Complete && isLoading ==false)
		{
		}
	}

	public void GameQuit(GameObject Ref)
	{
		switch(Ref.name)
		{
		case "Quit_Yes":;
			Application.Quit();
			break;
		case "Quit_No":
			Quit_Screen.GetComponent<Alphaeffect>().Back();
			QuitPopup = false;
			break;
		}
	}

	public void Manage_lock()
	{

//		Debug.Log ("level no..........."+PlayerPrefs.GetInt("Level_No")+"max no"+LevelNum);
		for (int i =1; i<=LevelNum; i++) 
		{
			if(LevelNum < 10)
				Btn_Level[i].transform.GetChild(1).gameObject.SetActive(false);
			else
			{
				LevelNum =9;
				Btn_Level[i].transform.GetChild(1).gameObject.SetActive(false);
			}
		}
	}
	public string Levelname()
	{
		string Returnvalue ="";
		switch(LevelNum)
		{
			case 1:
				//Returnvalue= "Levelname";
				break;
			case 2:
				//Returnvalue= "Levelname";
				break;
		}
		return Returnvalue;
	}
	public IEnumerator LoadingOff()
	{
		SoundObject.GetComponent<AudioSource>().clip = Ingame_sound;
		SoundObject.GetComponent<AudioSource>().Play();
		yield return new WaitForSeconds(5.4f);
		LoadingScreen.SetActive(false);
		isLoading =false;
		if(IngameHUD !=null)
		{
			IngameHUD.SetActive(true);
			IngameHUD.GetComponent<HUDIngame>().Instruction_screen.SetActive(true);
			//IngameHUD.GetComponent<HUDIngame>().Intro_screen.SetActive(true);
		}
	}
	public void LevelButtonClicked(GameObject btn)
	{

		//Mainmenu_Screen.GetComponent<Alphaeffect>().Back();
		gamefailed_Complete =false;
		Levelstatus = PlayerPrefs.GetInt ("Level_No");
	//	Debug.Log ("level unlocked"+Levelstatus);
		switch (btn.name) 
		{

		case "Level_1":
			Level_selected = 0;
			LoadingScreen.SetActive(true);
			Level_selection.GetComponent<Alphaeffect>().Back();
			isLoading =true;
			Application.LoadLevel("Ingame");

			StartCoroutine(LoadingOff());
			break;

		case "Level_2":
			if(Levelstatus >=1)
			{
				Level_selected = 1;
				LoadingScreen.SetActive(true);
				isLoading =true;
				Application.LoadLevel("Ingame");
				Level_selection.GetComponent<Alphaeffect>().Back();
				StartCoroutine(LoadingOff());
			}
			else
				Debug.Log("Level is locked..");
		break;

		case "Level_3":
			if(Levelstatus >=2)
			{
				Level_selected = 2;
				LoadingScreen.SetActive(true);
				isLoading =true;
				Application.LoadLevel("Ingame");
				Level_selection.GetComponent<Alphaeffect>().Back();
				StartCoroutine(LoadingOff());

			}
			else
				Debug.Log("Level is locked..");
			break;
		case "Level_4":
			if(Levelstatus >=3)
			{
				Level_selected = 3;
				LoadingScreen.SetActive(true);
				isLoading =true;
				Application.LoadLevel("Ingame");
				Level_selection.GetComponent<Alphaeffect>().Back();
				StartCoroutine(LoadingOff());

			}
			else
				Debug.Log("Level is locked..");
			break;
		case "Level_5":
			if(Levelstatus >=4)
			{
				Level_selected = 4;
				LoadingScreen.SetActive(true);
				isLoading =true;
				Application.LoadLevel("Ingame");
				Level_selection.GetComponent<Alphaeffect>().Back();
				StartCoroutine(LoadingOff());
			}
			else
				Debug.Log("Level is locked..");
			break;
		case "Level_6":
			if(Levelstatus >=5)
			{
				Level_selected = 5;
				LoadingScreen.SetActive(true);
				isLoading =true;
				Application.LoadLevel("Ingame");
				Level_selection.GetComponent<Alphaeffect>().Back();
				StartCoroutine(LoadingOff());

			}
			else
				Debug.Log("Level is locked..");
			break;
		case "Level_7":
			if(Levelstatus >=6)
			{
				Level_selected = 6;
				LoadingScreen.SetActive(true);
				isLoading =true;
				Application.LoadLevel("Ingame");
				Level_selection.GetComponent<Alphaeffect>().Back();
				StartCoroutine(LoadingOff());

			}
			else
				Debug.Log("Level is locked..");
			break;
		case "Level_8":
			if(Levelstatus >=7)
			{
				Level_selected = 7;
				LoadingScreen.SetActive(true);
				isLoading =true;
				Application.LoadLevel("Ingame");
				Level_selection.GetComponent<Alphaeffect>().Back();
				StartCoroutine(LoadingOff());

			}
			else
				Debug.Log("Level is locked..");
			break;
		case "Level_9":
			if(Levelstatus >=8)
			{
				Level_selected = 8;
				LoadingScreen.SetActive(true);
				isLoading =true;
				Application.LoadLevel("Ingame");
				Level_selection.GetComponent<Alphaeffect>().Back();
				StartCoroutine(LoadingOff());

			}
			else
				Debug.Log("Level is locked..");
			break;
		case "Level_10":
			if(Levelstatus >=9)
			{
				Level_selected = 9;
				LoadingScreen.SetActive(true);
				isLoading =true;
				Application.LoadLevel("Ingame");
				Level_selection.GetComponent<Alphaeffect>().Back();
				StartCoroutine(LoadingOff());

			}
			else
				Debug.Log("Level is locked..");
			break;
		}
	}

	public void ButtonClick(GameObject btnGO)
	{
		IngameHUD = null;
		switch (btnGO.name) 
		{
		
		case STR_BTN_OBJECTIVE_COUNTINUE:
			//if(PlayerPrefs.GetString("Lastname") == "" || PlayerPrefs.GetString("Firstname") == "" )
				//Userinfo_screen.SetActive(true);

			Mainmenu_Screen.SetActive(true);
			ObjectiveScreen.SetActive(false);
			break;
		case STR_BTN_SUBMIT_NAME:
			if( firstname.text != "" )
			{
				PlayerPrefs.SetString("Lastname", ""+Lastname.text);
				PlayerPrefs.SetString("Firstname", ""+firstname.text);
				Userinfo_screen.SetActive(false);
				Certificate_screen.SetActive(true);
				certi_score.text = PlayerPrefs.GetInt("Score").ToString();
				Certi_name.text = PlayerPrefs.GetString("Firstname");
			}
			else
			{
				Wrong_name.SetActive(true);
				StartCoroutine(HideName_error());
			}
			break;
		case STR_BTN_SKIP_NAME:
			PlayerPrefs.SetString("Firstname","Player");
			Userinfo_screen.SetActive(false);
			Certificate_screen.SetActive(true);
			certi_score.text = PlayerPrefs.GetInt("Score").ToString();
			Certi_name.text = PlayerPrefs.GetString("Firstname");
			break;
		case STR_BTN_COUNTINUE_CERTI:

			Level_selection.SetActive(true);
			Certificate_screen.SetActive(false);
			Mainmenu_Screen.SetActive(false);
			if(Application.loadedLevelName == "Ingame")
			{
				HUDIngame.instance.MainMenu_sound("Mainmenu");
				if (PlayerPrefs.GetInt ("Certificate") == 1)
					Show_certi_btn.SetActive (true);
			}
			else
			{
				Mainmenu_Screen.SetActive(true);
				Level_selection.SetActive(false);
			}
			Application.LoadLevel("Mainmenu");
			break;

		case STR_BTN_SHOW_CERTI:
			certi_score.text = PlayerPrefs.GetInt("Score").ToString();
			Certi_name.text = PlayerPrefs.GetString("Firstname");
			Certificate_screen.SetActive(true);
			break;
		case STR_BTN_PLAY:
            
			Mainmenu_Screen.SetActive(false);
			Level_selection.SetActive(true);
			Manage_lock();
			break;
		case STR_BTN_SETTING:
          
			Setting_Screen.SetActive(true);
			Setting_Screen.GetComponent<CanvasGroup>().alpha = 0;
			//Mainmenu_Screen.SetActive(false);
			break;

		case STR_BTN_VIDEO:
			Application.OpenURL("https://www.youtube.com/watch?v=1nlaetZg4JY");
			break;

		case STR_BTN_CREDITS:
           
			Setting_Screen.SetActive(false);
			Credits_Screen.SetActive(true);
			break;
		case STR_BTN_CREDITS_BACK:
			Credits_Screen.GetComponent<Alphaeffect>().Back();
			Setting_Screen.SetActive(true);
			break;
//			
		case STR_BTN_COUNTINUE:
			LoadingScreen.SetActive(true);
			isLoading =true;
			StartCoroutine(LoadingOff());
			Mainmenu_Screen.GetComponent<Alphaeffect>().Back();
			gamefailed_Complete =false;
			Application.LoadLevel("Ingame");
			if(PlayerPrefs.GetInt("Level_No") >= 10)
				Level_selected =9;
			else
				Level_selected = PlayerPrefs.GetInt("Level_No");
			Debug.Log("selected level is"+Level_selected);
			break;

		case STR_BTN_LEVEL_SELECTION_BACK:
			LoadingScreen.SetActive(false);
			Level_selection.SetActive(false);
			Mainmenu_Screen.SetActive(true);
			break;

		case STR_BTN_HELP:
            //StartCoroutine(ShowAdWhenReady());
			Help_screen.SetActive(true);
			break;

		case STR_BTN_HELP_BACK:
			Help_screen.SetActive(false);
			break;

		}
	}
}
