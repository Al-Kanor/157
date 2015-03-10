using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    void Update () {
        Vector3 movement = Vector3.zero;

        if (Input.GetKeyDown (KeyCode.UpArrow)) {
            movement = Vector3.forward;
        }
        else if (Input.GetKeyDown (KeyCode.DownArrow)) {
            movement = -Vector3.forward;
        }
        else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
            movement = -Vector3.right;
        }
        else if (Input.GetKeyDown (KeyCode.RightArrow)) {
            movement = Vector3.right;
        }

        transform.position += movement;
    }
}
