using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DailyButton : MonoBehaviour {

	public Text Top1Text, Top2Text, Top3Text, Top4Text, Top5Text, Top6Text, Top7Text, Top8Text, Top9Text, Top10Text;
	public Image Top1Sprite, Top2Sprite, Top3Sprite, Top4Sprite, Top5Sprite, Top6Sprite, Top7Sprite, Top8Sprite, Top9Sprite, Top10Sprite;
	public Sprite Icon1, Icon2, Icon3, Icon4, Icon5, Icon6, Icon7, Icon8, Icon9;
	public Scrollbar scrollbar;

	// Use this for initialization
	void Start () {
		OnClickDaily ("Daily");
	}

	public void OnClickDaily (string ScoreboardType ) {
		scrollbar.value = 1;
		if (ScoreboardType == "Daily") {
			Top1Text.text = "150 000";
			Top2Text.text = "140 000";
			Top3Text.text = "125 000";
			Top4Text.text = "100 000";
			Top5Text.text = "80 000";
			Top6Text.text = "50 000";
			Top7Text.text = "25 000";
			Top8Text.text = "15 000";
			Top9Text.text = "10 000";
			Top10Text.text = "5 000";

			Top1Sprite.sprite = Icon1;
			Top2Sprite.sprite = Icon2;
			Top3Sprite.sprite = Icon3;
			Top4Sprite.sprite = Icon4;
			Top5Sprite.sprite = Icon5;
			Top6Sprite.sprite = Icon6;
			Top7Sprite.sprite = Icon7;
			Top8Sprite.sprite = Icon8;
			Top9Sprite.sprite = Icon9;
			Top10Sprite.sprite = Icon5;
		}
		if (ScoreboardType == "Monthly") {
			Top1Text.text = "173 000";
			Top2Text.text = "171 000";
			Top3Text.text = "170 000";
			Top4Text.text = "168 000";
			Top5Text.text = "150 000";
			Top6Text.text = "140 000";
			Top7Text.text = "125 000";
			Top8Text.text = "100 000";
			Top9Text.text = "80 000";
			Top10Text.text = "50 000";
			
			Top1Sprite.sprite = Icon7;
			Top2Sprite.sprite = Icon9;
			Top3Sprite.sprite = Icon8;
			Top4Sprite.sprite = Icon7;
			Top5Sprite.sprite = Icon1;
			Top6Sprite.sprite = Icon2;
			Top7Sprite.sprite = Icon3;
			Top8Sprite.sprite = Icon4;
			Top9Sprite.sprite = Icon5;
			Top10Sprite.sprite = Icon6;
		}
		if (ScoreboardType == "Ever") {
			Top1Text.text = "999 999";
			Top2Text.text = "800 000";
			Top3Text.text = "500 000";
			Top4Text.text = "350 000";
			Top5Text.text = "200 000";
			Top6Text.text = "173 000";
			Top7Text.text = "171 000";
			Top8Text.text = "170 000";
			Top9Text.text = "168 000";
			Top10Text.text = "150 000";
			
			Top1Sprite.sprite = Icon2;
			Top2Sprite.sprite = Icon3;
			Top3Sprite.sprite = Icon4;
			Top4Sprite.sprite = Icon5;
			Top5Sprite.sprite = Icon6;
			Top6Sprite.sprite = Icon7;
			Top7Sprite.sprite = Icon9;
			Top8Sprite.sprite = Icon8;
			Top9Sprite.sprite = Icon7;
			Top10Sprite.sprite = Icon1;
		}
	}
}
