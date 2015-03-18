using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Manager_Game : Singleton<UI_Manager_Game> {

	public Scrollbar scoreScrollbar;
	public Image scrollBarHandle;
	public Image lash;
	public int scroreBarLimit;

	public Text comboTxt;
	public Text finalScoreTxt;

	public Sprite handle_1;
	public Sprite handle_2;
	public Sprite handle_3;
	public Sprite handle_4;
	public Sprite handle_5;
	public Sprite handle_6;

	public Canvas pauseCanvas;
	public Canvas gameCanvas;
	public Canvas optionCanvas;
	public Canvas finalCanvas;

	public Image soundLevel;
	public Image lumLevel;

	public Sprite lvl0;
	public Sprite lvl1;
	public Sprite lvl2;
	public Sprite lvl3;
	public Sprite lvl4;

	private float timer1;
	private float timer2;
	private int score1;
	private float score2;
	private int stacks;
	private float scoreScrollbarValue;
	private float baseLashXPosition;
	private int comboCount;

	private int soundVolume;
	private int lumVolume;

	// Use this for initialization
	void Start () 
	{
		scoreScrollbar.value = 0;
		stacks = 0;
		score1 = 0;
		scoreScrollbarValue = 0.0f;
		baseLashXPosition = lash.rectTransform.localPosition.x;
		Time.timeScale = 1;

		optionCanvas.enabled = false;
		pauseCanvas.enabled = false;
		gameCanvas.enabled = true;
		finalCanvas.enabled = false;


		soundVolume = 4;
		lumVolume = 4;
	}
	
	// Update is called once per frame
	void Update () {
		score1 = GameManager.Instance.Score ;
		score2 = (float)score1;
		timer1 = GameManager.Instance.Timer ;
		timer2 = 167.0f - timer1; 

		if (timer2 >= 12.0f) {
			comboCount = GameManager.Instance.ComboCounter;
			comboTxt.text = ("x" + comboCount);
<<<<<<< HEAD
=======
			//Debug.Log (timer2);
>>>>>>> origin/master
		}
		scoreScrollbarValue = (score2 - (scroreBarLimit*stacks))/scroreBarLimit;
		scoreScrollbar.size = scoreScrollbarValue;
		Vector3 newVector = lash.rectTransform.localPosition;
		newVector.x = (baseLashXPosition - (1.92f*timer2));
		lash.rectTransform.localPosition = newVector;
		if (score2 > (scroreBarLimit*(stacks+1))) 
		{
			stacks = stacks +1;
			ColorSwap ();
		}

	}

	void ColorSwap () 
	{

		switch (stacks) 
		{
			case 1 :
				scoreScrollbar.GetComponent<Image>().sprite = handle_1;
				scrollBarHandle.sprite = handle_2;
			break;

			case 2 : 
				scoreScrollbar.GetComponent<Image>().sprite = handle_2;
				scrollBarHandle.sprite = handle_3;
			break;
				
			case 3 : 
				scoreScrollbar.GetComponent<Image>().sprite = handle_3;
				scrollBarHandle.sprite = handle_4;
			break;

			case 4 : 
				scoreScrollbar.GetComponent<Image>().sprite = handle_4;
				scrollBarHandle.sprite = handle_5;
			break;

			case 5 : 
				scoreScrollbar.GetComponent<Image>().sprite = handle_5;
				scrollBarHandle.sprite = handle_6;
			break;

		}
	}

	public void PauseMode ()
	{
		pauseCanvas.enabled = true;
		gameCanvas.enabled = false;
		optionCanvas.enabled = false;
		finalCanvas.enabled = false;
		Time.timeScale = 0;
	}

	public void UnPauseMode ()
	{
		pauseCanvas.enabled = false;
		gameCanvas.enabled = true;
		optionCanvas.enabled = false;
		finalCanvas.enabled = false;
		Time.timeScale = 1;
	}

	public void OptionMode ()
	{
		optionCanvas.enabled = true;
		gameCanvas.enabled = false;
		pauseCanvas.enabled = false;
		finalCanvas.enabled = false;
	}

	public void UnOptionMode ()
	{
		pauseCanvas.enabled = true;
		gameCanvas.enabled = false;
		optionCanvas.enabled = false;
		finalCanvas.enabled = false;
	}

	public void FinalScoreMode ()
	{
		pauseCanvas.enabled = false;
		gameCanvas.enabled = false;
		optionCanvas.enabled = false;
		finalCanvas.enabled = true;
		Time.timeScale = 0;
		finalScoreTxt.text = (""+score1);
	}
	public void RestartMode ()
	{
		Time.timeScale = 1;
		Application.LoadLevel ("level");
	}
	public void MainMenuMode ()
	{
		Time.timeScale = 1;
		Application.LoadLevel ("Main_Menu");
	}
	public void VolumeChange(bool Positive)
	{
		if (Positive) 
		{
			soundVolume ++;
			VolumeRefresh ();
		}
		if (!Positive) 
		{
			soundVolume --;
			VolumeRefresh ();
		}
		if (soundVolume >= 4) { 
			soundVolume = 4;
		}
		if (soundVolume <= 0) { 
			soundVolume = 0;
		}

	}
	public void LumChange(bool Positive)
	{
		if (Positive) 
		{
			lumVolume ++;
			LumRefresh ();
		}
		if (!Positive) 
		{
			lumVolume --;
			LumRefresh ();
		}
		if (lumVolume >= 4) { 
			lumVolume = 4;
		}
		if (lumVolume <= 0) {
			lumVolume = 0;
		}

	}

	public void VolumeRefresh ()
	{
		switch (soundVolume) 
		{
			case 0 :
			soundLevel.sprite = lvl0;
			break;

			case 1 :
			soundLevel.sprite = lvl1;			
			break;

			case 2 :
			soundLevel.sprite = lvl2;			
			break;

			case 3 :
			soundLevel.sprite = lvl3;			
			break;

			case 4 :
			soundLevel.sprite = lvl4;			
			break;

		}
	}

	public void LumRefresh ()
	{
		switch (lumVolume) 
		{
			case 0 :
			lumLevel.sprite = lvl0;	
			break;

			case 1 :
			lumLevel.sprite = lvl1;			
			break;

			case 2 :
			lumLevel.sprite = lvl2;	
			break;

			case 3 :
			lumLevel.sprite = lvl3;	
			break;

			case 4 :
			lumLevel.sprite = lvl4;	
			break;
			
		}
		
	}

}
