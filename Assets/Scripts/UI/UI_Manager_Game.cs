using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Manager_Game : MonoBehaviour {

	public Scrollbar scoreScrollbar;
	public Image Lash;
	private float Timer;
	private int score;
	private int stacks;
	private float scoreScrollbarValue;
	private float baseLashYPosition;

	// Use this for initialization
	void Start () {
		scoreScrollbar.value = 0;
		stacks = 0;
		score = 0;
		scoreScrollbarValue = 0.0f;
		Timer = 0.0f;
		baseLashYPosition = Lash.rectTransform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update () {
		//gameManager.score = score;
		scoreScrollbarValue = (score - (100*stacks))/100;
		scoreScrollbar.value = scoreScrollbarValue;
		Timer += Time.deltaTime;

		Vector3 newVector = Lash.rectTransform.localPosition;
		newVector.y = (baseLashYPosition - (1.92f*Timer));
		Lash.rectTransform.localPosition = newVector;
	}
}
