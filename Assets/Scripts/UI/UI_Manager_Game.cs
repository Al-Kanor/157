using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Manager_Game : MonoBehaviour {

	public Scrollbar scoreScrollbar;
	public Image scrollBarHandle;
	public Image lash;
	public int scroreBarLimit;

	public Sprite handle_1;
	public Sprite handle_2;
	public Sprite handle_3;
	public Sprite handle_4;
	public Sprite handle_5;
	public Sprite handle_6;

	private float timer1;
	private float timer2;
	private int score1;
	private float score2;
	private int stacks;
	private float scoreScrollbarValue;
	private float baseLashYPosition;

	// Use this for initialization
	void Start () {
		scoreScrollbar.value = 0;
		stacks = 0;
		score1 = 0;
		scoreScrollbarValue = 0.0f;
		baseLashYPosition = lash.rectTransform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update () {
		score1 = GameManager.Instance.Score ;
		score2 = (float)score1;
		timer1 = GameManager.Instance.Timer ;
		timer2 = 167.0f - timer1; 
		scoreScrollbarValue = (score2 - (scroreBarLimit*stacks))/scroreBarLimit;
		scoreScrollbar.size = scoreScrollbarValue;
		Vector3 newVector = lash.rectTransform.localPosition;
		newVector.y = (baseLashYPosition - (1.92f*timer2));
		lash.rectTransform.localPosition = newVector;
		//Debug.Log ("Score = "+ score2);
		if (score2 > (scroreBarLimit*(stacks+1))) 
		{
			stacks = stacks +1;
			ColorSwap ();
		}
	}

	void ColorSwap () {

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
}
