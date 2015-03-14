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
    private int timer;
    #endregion

    #region Accesseurs
    public int Timer {
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
            timer--;
            if (timer == emergencyCountdown) {
                Instantiate (emergencyLightPrefab);
            }
            else if (timer <= 0) {
                Application.LoadLevel ("Main_Menu");
            }
            yield return new WaitForSeconds (1);
        } while (true);
    }
    #endregion
}
