using UnityEngine;
using System.Collections;

public class SettingScreen_Controller : MonoBehaviour 
{
	public static SettingScreen_Controller instance;
	
	public GameObject Soundoff;
	public GameObject SoundOn;
	private const string STR_BTN_SOUND_ON = "SoundOn";
	private const string STR_BTN_SOUND_OFF = "SoundOff";
	
	
	// Use this for initialization
	void Start () 
	{
		instance = this;

		if(!PlayerPrefs.HasKey("Sound"))
			PlayerPrefs.SetInt("Sound",0);

		if(!PlayerPrefs.HasKey("Notification"))
			PlayerPrefs.SetInt("Notification",0);
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	public void Setting_ButtonClick(GameObject btnGO)
	{
		switch (btnGO.name) 
		{
			case STR_BTN_SOUND_ON:
				PlayerPrefs.SetInt("Sound",1);
				Debug.Log("Sound"+PlayerPrefs.GetInt("Sound"));
				SoundOn.SetActive(false);
				Soundoff.SetActive(true);
				MainMenuScreenManager.instance.Mute();
				break;	
			case STR_BTN_SOUND_OFF:

				PlayerPrefs.SetInt("Sound",0);
				Debug.Log("Sound"+PlayerPrefs.GetInt("Sound"));
				Soundoff.SetActive(false);
				SoundOn.SetActive(true);
				MainMenuScreenManager.instance.Mute();
				break;

		}
	}
}
