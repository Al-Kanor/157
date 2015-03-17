using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
    #region Attributs publics
    public int ores = 0;
    
    public GameObject explosionPrefab;
    public GameObject orePrefab;
    #endregion

    #region Attributs privés
    #endregion

    #region Accesseurs
    
    #endregion

    #region Méthodes publiques
    public void Die () {
        // Boom !
        GameObject explosion = Instantiate (explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy (explosion, 2);

        // Ore
        if (null != orePrefab) {
            Instantiate (orePrefab, transform.position, Quaternion.identity);
        }

        // Bye bye
        BlocksManager.Instance.DestroyBlock (gameObject);
    }
    #endregion

    #region Méthodes privées
    void FixedUpdate () {
        
    }

    void Start () {
        
    }
    #endregion
}
