using UnityEngine;
using System.Collections;

public class BoomManager : Singleton<BoomManager> {
    #region Attributs publics
    //public Vector3 velocity = new Vector3 (0, 10, 0);
    public GameObject explosionPrefab;
    #endregion

    #region Attributs privés
    private GameObject explosionObject;
    #endregion

    #region Méthodes publiques
    public void Boom (Vector3 pos) {
        //explosionObject.transform.position = new Vector3 (pos.x, 1000f, pos.z);
        explosionObject.transform.position = pos + Vector3.up * 1.5f;
//        explosionObject.GetComponent<ParticleSystem> ().Emit (pos, velocity, 10, 2, Color.white);
        explosionObject.GetComponent<ParticleSystem> ().Play ();
    }
    #endregion

    #region Méthodes privées
    void Start () {
        explosionObject = Instantiate (explosionPrefab) as GameObject;
        //transform.position += Vector3.up * 10;
    }
    #endregion
}
