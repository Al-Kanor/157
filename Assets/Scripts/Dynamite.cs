using UnityEngine;
using System.Collections;

public class Dynamite : MonoBehaviour {
    #region Attributs publics
    public int countdown = 5;
    public GameObject explosionPrefab;
    public LayerMask layerMaskBlock;
    #endregion

    #region Méthodes privées
    IEnumerator Boom () {
        yield return new WaitForSeconds (countdown);
        GameObject explosion = Instantiate (explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy (explosion, 2);

        #region Destruction des blocs alentours
        RaycastHit hit;
        if (Physics.Raycast (transform.position, Vector3.forward, out hit, 1, layerMaskBlock)) {
            hit.collider.gameObject.GetComponent<Block> ().Die ();
        }
        if (Physics.Raycast (transform.position, -Vector3.forward, out hit, 1, layerMaskBlock)) {
            hit.collider.gameObject.GetComponent<Block> ().Die ();
        }
        if (Physics.Raycast (transform.position, -Vector3.right, out hit, 1, layerMaskBlock)) {
            hit.collider.gameObject.GetComponent<Block> ().Die ();
        }
        if (Physics.Raycast (transform.position, Vector3.right, out hit, 1, layerMaskBlock)) {
            hit.collider.gameObject.GetComponent<Block> ().Die ();
        }
        #endregion

        Destroy (gameObject);
    }
    void Start () {
        StartCoroutine ("Boom");
    }
    #endregion
}
