using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

	public Scrollbar Loading;
	public float LoadingTime;
	public Text Tips;
	public int TipsNo;
	// Use this for initialization
	void Start () {

	}

	void OnEnable()
	{
		TipsNo = Random.Range(0,4);
		Tips.text ="  Tip :- "+MainMenuScreenManager.instance.Tips[TipsNo];//+ MainMenuScreenManager.instance.JsonString["Tips"][(MainMenuScreenManager.instance.LevelNum-1)]["Tip"].ToString();
		Loading.size =0;
	}
	// Update is called once per frame
	void Update () 
	{
		if(Loading.size < LoadingTime)
			Loading.size = Loading.size+(Time.deltaTime);
	}
}
