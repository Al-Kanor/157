using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
    #region Attributs publics
    public int ores = 0;
    //public GameObject destroyedBlockPrefab;
    public GameObject orePrefab;
    public GameObject vehiclePrefab;
    public GameObject explosingPrefab;
    public int explosionChance = 20;
    public Material matdynamite;
   
    public float explosionCountdown = 3;
    public GameObject explosionPrefab;
    public LayerMask layerMaskPlayer;
    public LayerMask layerMaskBlock;
    public enum Type
    {
        LINEAR, // Les blocs adjacents
        CLOSE   // Les blocs ajacents + en diagonale
    }

    public Type type;
    public AnimationCurve BlinkSpeed;
    public Gradient BlinkGradient;
   
    #endregion

    #region Attributs privés
    //private Transform blocksContainerTransform;
    private bool exploding = false;
    private float blink = 0f;
    private float maxCooldown = 0;
    private float truecooldown = 0;
    #endregion

    #region Accesseurs
    
    #endregion

    #region Méthodes publiques
    
    public void Die () {
      float _random = Random.Range(0,100);
        if(_random<explosionChance && this.tag=="Empty Block")
        {
            Instantiate(explosingPrefab, transform.position, Quaternion.identity);
        }
        
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
        BlocksManager.Instance.DestroyBlock (gameObject);
    }
    #endregion

    #region Méthodes privées


    #endregion
}
