using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    #region Attributs publics
    public float speed = 10;
    public float cameraSpeed = 3;
    public float cameraHeight = 10;
    public GameObject dynamitePrefab;
    public LayerMask layerMaskBlock;
    #endregion

    #region Attributs privés
    private Vector3 targetPos;
    private bool isMovable = true;
    #endregion

    #region Méthodes privées
    void Move () {
        targetPos += new Vector3 (InputManager.Instance.SwipeAxis.x, 0, InputManager.Instance.SwipeAxis.y);
        isMovable = false;
        BlocksManager.Instance.UpdateBlocks (targetPos);
    }

    void Start()
    {
        targetPos = transform.position;
    }
    void ThrowDynamite () {
        GameObject dynamiteObject = Instantiate (dynamitePrefab, transform.position - Vector3.up / 2, Quaternion.identity) as GameObject;
    }

    void Update()
    {
        RaycastHit hit;
        if (isMovable && InputManager.Instance.CurrentGesture != InputManager.Gestures.None && InputManager.Instance.CurrentGesture != InputManager.Gestures.DoubleTap) {
            if (Physics.Raycast (targetPos, new Vector3 (InputManager.Instance.SwipeAxis.x, 0, InputManager.Instance.SwipeAxis.y), out hit, 1, layerMaskBlock))
            {
                if ("Empty Block" == hit.collider.tag) {
                    hit.collider.gameObject.GetComponent<Block> ().Die ();
                    ThrowDynamite ();
                    Move ();
                }
            }
            else {
                ThrowDynamite ();
                Move ();
            }
        }
        transform.position = Vector3.Lerp (transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance (transform.position, targetPos) < 0.05f) {
            transform.position = targetPos; // Clamp

            isMovable = true;
        }

        Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, new Vector3(transform.position.x, cameraHeight, transform.position.z), cameraSpeed * Time.deltaTime);
    }
    #endregion
}
