using UnityEngine;
using System.Collections;

public class Ore : MonoBehaviour {
    #region Attributs publics
    public float speed = 10;
    #endregion

    #region Attributs privés
    Transform target = null;
    #endregion

    #region Accesseurs
    public Transform Target {
        get { return target; }
        set {
            target = value;
            StartCoroutine ("MoveToTarget");
        }
    }
    #endregion

    #region Méthodes publiques

    #endregion

    #region Méthodes privées
    IEnumerator MoveToTarget () {
        do {
            transform.position = Vector3.Lerp (transform.position, target.position, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame ();
        } while (Vector3.Distance (target.position, transform.position) > 0.1f);
        Destroy (gameObject);
    }
    #endregion
}
