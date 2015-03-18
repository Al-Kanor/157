using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {
    #region Attributs publics
    public int gameDuration = 157;
    public int emergencyCountdown = 30;
    public Player player;
    public GameObject emergencyLightPrefab;
	public float comboTimeMax;

    #endregion

    #region Attributs privés
    private int score = 0;
	private int comboCounter = 1;
    private float timer;
	private bool emergency = false;
	private float timerCombo;
    #endregion

    #region Accesseurs
    public int Score {
        get { return score; }
        set { score = value; }
    }
	public int ComboCounter {
		get { return comboCounter; }
		set { comboCounter = value; }
	}

    public float Timer {
        get { return timer; }
    }
    #endregion

    #region Méthodes privées
    void Start () {
        BlocksManager.Instance.GenerateBase ();
        timer = gameDuration;
		comboCounter = 1;
        StartCoroutine ("UpdateTimer");
    }

    IEnumerator UpdateTimer () {
        do {
            timer -= Time.deltaTime;
			timerCombo += Time.deltaTime;

			if (timerCombo >= comboTimeMax && comboCounter != 1) {
				comboCounter = 1;
			}

			if (!emergency && timer <= emergencyCountdown) {
                Instantiate (emergencyLightPrefab);
				emergency = true;
            }
            else if (timer <= 0) {
				UI_Manager_Game.Instance.FinalScoreMode ();
            }
			yield return new WaitForEndOfFrame();
        } while (true);
    }
    #endregion

	#region Méthodes publiques
	public void Scoring (int valeur) {
		timerCombo = 0.0f;
		score = score + (valeur * comboCounter);
		comboCounter ++;
	}
	#endregion
}
