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
    private float timer;
	private bool emergency = false;
	private int comboCounter;
	private float timerCombo;
    #endregion

    #region Accesseurs
    public int Score {
        get { return score; }
        set { score = value; }
    }

    public float Timer {
        get { return timer; }
    }
    #endregion

    #region Méthodes privées
    void Start () {
        BlocksManager.Instance.GenerateBase ();
        timer = gameDuration;
        StartCoroutine ("UpdateTimer");
    }

    IEnumerator UpdateTimer () {
        do {
            timer -= Time.deltaTime;
			timerCombo += Time.deltaTime;

			if (timerCombo >= comboTimeMax) {
				comboCounter = 1;
			}

			if (!emergency && timer <= emergencyCountdown) {
                Instantiate (emergencyLightPrefab);
				emergency = true;
            }
            else if (timer <= 0) {
                Application.LoadLevel ("Main_Menu");
            }
			yield return new WaitForEndOfFrame();
        } while (true);
    }
    #endregion

	#region Méthodes publiques
	public void Scoring (int valeur) {
		timerCombo = 0.0f;
		score = valeur + comboCounter;
		comboCounter ++;
	}
	#endregion
}
