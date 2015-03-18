using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
    #region Attributs publics
    public int ores = 0;
    public GameObject destroyedBlockPrefab;
    public GameObject explosionPrefab;
    public GameObject orePrefab;
    public GameObject vehiclePrefab;
    #endregion

    #region Attributs privés
    private Transform blocksContainerTransform;
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

        if (null != vehiclePrefab)
        {
            Instantiate(vehiclePrefab, transform.position, Quaternion.identity);
        }

        // Bye
        BlocksManager.Instance.DestroyBlock (gameObject);
    }
    #endregion

    #region Méthodes privées
    void FixedUpdate () {
        
    }


    void Start () 
	{
        blocksContainerTransform = GameObject.Find ("BlocksContainer").transform;

    }


    #endregion
}
