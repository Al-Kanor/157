using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
    #region Attributs publics
    //public int ores = 0;
    //public GameObject destroyedBlockPrefab;
    public GameObject orePrefab;
    public GameObject vehiclePrefab;
    public GameObject explodingPrefab;
    public int explosionChance = 20;

    #endregion

    #region Attributs privés
    //private Transform blocksContainerTransform;
    
    #endregion

    #region Accesseurs
    
    #endregion

    #region Méthodes publiques
    
    public void Die () 
    {
      float _random = Random.Range(0,100);
        if(_random<explosionChance && this.tag=="Empty Block")
        {
            GameManager.Instance.player.exploding = this;
            Instantiate(explodingPrefab, transform.position, Quaternion.identity);  
        }
        else
        ParticleManager.Instance.Blast (transform.position);

        // Ore
        if (null != orePrefab) {
            Instantiate (orePrefab, transform.position, Quaternion.identity);
            
        }

        if (null != vehiclePrefab)
        {
            Instantiate (vehiclePrefab, transform.position, Quaternion.identity);
            
        }

        // Bye
        BlocksManager.Instance.DestroyBlock(gameObject);
    }
    #endregion

    #region Méthodes privées


    #endregion
}
