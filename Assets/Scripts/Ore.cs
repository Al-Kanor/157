using UnityEngine;
using System.Collections;

public class Ore : MonoBehaviour {
    #region Méthodes privées
    void OnTriggerEnter (Collider other) {
        if ("Player" == other.tag) {
            Destroy (gameObject);
        }
    }
    #endregion
}
