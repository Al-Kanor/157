using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {
    #region Attributs publics
    public int gameDuration = 157;
    public int emergencyCountdown = 30;
    public Player player;
    public GameObject emergencyLightPrefab;
    #endregion

    #region Attributs privés
    private int score = 0;
    private float timer;
	private bool emergency = false;
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
}
