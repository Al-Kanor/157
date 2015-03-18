using UnityEngine;
using System.Collections;

public class BoomManager : Singleton<BoomManager> {
    #region Attributs publics
    public Vector3 velocity = new Vector3 (0, 10, 0);
    public GameObject explosionPrefab;
    #endregion

    #region Attributs privés
    private GameObject explosionObject;
    #endregion

    #region Méthodes publiques
    public void Boom (Vector3 pos) {
        //explosionObject.transform.position = new Vector3 (pos.x, 1.5f, pos.z);
        //explosionObject.transform.position = pos;
        explosionObject.GetComponent<ParticleSystem> ().Emit (pos, velocity, 10, 2, Color.white);
    }
    #endregion

    #region Méthodes privées
    void Start () {
        explosionObject = Instantiate (explosionPrefab) as GameObject;
    }
    #endregion
}
