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

        if (null != vehiclePrefab) {
			Debug.Log ("Vehicle!");
			Instantiate (vehiclePrefab, transform.position, Quaternion.identity);
			SoundManager.Instance.PlaySound (SoundManager.SoundName.CAISSE);
            
		} else {
			int rand = Random.Range(0, 7);
			switch (rand) {
			case 0: 
				SoundManager.Instance.PlaySound (SoundManager.SoundName.HITROCK1);
				break;
			case 1: 
				SoundManager.Instance.PlaySound (SoundManager.SoundName.HITROCK2);
				break;
			case 2: 
				SoundManager.Instance.PlaySound (SoundManager.SoundName.HITROCK3);
				break;
			case 3: 
				SoundManager.Instance.PlaySound (SoundManager.SoundName.HITROCK4);
				break;
			case 4: 
				SoundManager.Instance.PlaySound (SoundManager.SoundName.HITROCK5);
				break;
			case 5: 
				SoundManager.Instance.PlaySound (SoundManager.SoundName.HITROCK6);
				break;
			case 6: 
				SoundManager.Instance.PlaySound (SoundManager.SoundName.HITROCK7);
				break;
			}
		}



        // Bye
        BlocksManager.Instance.DestroyBlock(gameObject);
    }
    #endregion

    #region Méthodes privées


    #endregion
}
