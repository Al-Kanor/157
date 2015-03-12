using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
    #region Attributs publics
    public int ores = 0;
    public GameObject explosionPrefab;
    public GameObject orePrefab;
    #endregion

    #region Attributs privés
    private int id;
    #endregion

    #region Accesseurs
    public int Id {
        get { return id; }
        set { id = value; }
    }
    #endregion

    #region Méthodes publiques
    public void Die () {
        // Boom !
        GameObject explosion = Instantiate (explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy (explosion, 2);

        // Ore
        if (null != orePrefab) {
            GameObject ore = Instantiate (orePrefab, transform.position, Quaternion.identity) as GameObject;
        }

        // Bye
        Destroy (gameObject);
    }
    #endregion

    #region Méthodes privées
    void FixedUpdate () {
        if (true) {
            //transform.GetChild (0).gameObject.SetActive (false);
        }
    }
    #endregion
}
