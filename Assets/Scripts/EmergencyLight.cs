using UnityEngine;
using System.Collections;

public class EmergencyLight : MonoBehaviour {
    #region Attributs publics
    public float blinkDuration = 1;
    #endregion

    #region Attributs privés
    private bool isLighted = true;
    #endregion

    #region Méthodes privées
    IEnumerator Blink() {
        do {
            GetComponent<Light> ().enabled = isLighted;
            isLighted = !isLighted;
            yield return new WaitForSeconds (blinkDuration);
        } while (true);
    }

    void Start () {
        StartCoroutine ("Blink");
    }
    #endregion
}
