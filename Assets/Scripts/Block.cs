using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
    #region Enums publics
    /*
    public enum Type {
        EMPTY,
        GOLD,
        GAZ
    };
    */
    #endregion

    #region Attributs publics
    public GameObject explosionPrefab;
    #endregion

    #region Attributs privés
    //private Type type;
    #endregion

    #region Méthodes publiques
    public void Die () {
        GameObject explosion = Instantiate (explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy (explosion, 2);
        Destroy (gameObject);
    }
    #endregion
}
