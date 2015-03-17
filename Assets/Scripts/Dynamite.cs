using UnityEngine;
using System.Collections;

public class Dynamite : MonoBehaviour {
    #region Enums publics
    public enum Type {
        LINEAR, // Les blocs adjacents
        CLOSE   // Les blocs ajacents + en diagonale
    }
    #endregion

    #region Attributs publics
    public int countdown = 5;
    public GameObject explosionPrefab;
    public LayerMask layerMaskPlayer;
    public LayerMask layerMaskBlock;
    #endregion

    #region Attributs privés
    public Type type;
    #endregion

    #region Méthodes privées
    IEnumerator Boom () {
        yield return new WaitForSeconds (countdown);
        GameObject explosion = Instantiate (explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy (explosion, 2);

        #region Destruction des blocs alentours
        BlocksManager.Instance.DestroyBlocksAround (transform.position, type);
        #endregion

        #region Player stun
        if (Vector3.Distance (transform.position, GameManager.Instance.player.transform.position) <= 1.5f) {
            GameManager.Instance.player.Stuned = true;
        }
        #endregion

        Destroy (gameObject);
    }
    void Start () {
        StartCoroutine ("Boom");
    }
    #endregion
}
