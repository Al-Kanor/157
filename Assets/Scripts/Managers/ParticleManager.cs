using UnityEngine;
using System.Collections;

public class ParticleManager : Singleton<ParticleManager> {
    #region Attributs publics
    //public Vector3 velocity = new Vector3 (0, 10, 0);
    public GameObject blastPrefab;
    public GameObject explosionPrefab;
	public float xploHeight;
	public float xploHeightBlast;
    #endregion

    #region Attributs privés
    private GameObject[] blastObjects;
    private GameObject explosionObject;
    private int currentBlastIndex = 0;
    #endregion

    #region Méthodes publiques
    public void Blast (Vector3 pos) {
        blastObjects[currentBlastIndex].transform.position = pos + Vector3.up * xploHeightBlast;
        blastObjects[currentBlastIndex].GetComponent<ParticleSystem> ().Play ();
        currentBlastIndex = (currentBlastIndex + 1) % 3;
    }

    public void Boom (Vector3 pos) {
        //explosionObject.transform.position = new Vector3 (pos.x, 1000f, pos.z);
        explosionObject.transform.position = pos + Vector3.up * xploHeight;
//        explosionObject.GetComponent<ParticleSystem> ().Emit (pos, velocity, 10, 2, Color.white);
        explosionObject.GetComponent<ParticleSystem> ().Play ();
    }
    #endregion

    #region Méthodes privées
    void Start () {
        blastObjects = new GameObject[3];
        for (int i = 0; i < 3; ++i) {
            blastObjects[i] = Instantiate (blastPrefab) as GameObject;
        }
        explosionObject = Instantiate (explosionPrefab) as GameObject;
        //transform.position += Vector3.up * 10;
    }
    #endregion
}
