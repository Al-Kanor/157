using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Manager_Main : MonoBehaviour {

	public Canvas mainMenu;
	public Canvas optionMenu;

	public Image soundLevel;
	public Image lumLevel;
	
	public Sprite lvl0;
	public Sprite lvl1;
	public Sprite lvl2;
	public Sprite lvl3;
	public Sprite lvl4;

	private int soundVolume;
	private int lumVolume;

	void Start () {
		mainMenu.enabled = true;
		optionMenu.enabled = false;

		soundVolume = 4;
		lumVolume = 4;
	}
	

	void Update () {
	
	}

	public void OptionMenu () {
		mainMenu.enabled = false;
		optionMenu.enabled = true;
	}

	public void UnOptionMenu () {
		mainMenu.enabled = true;
		optionMenu.enabled = false;
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
