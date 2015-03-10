using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
    public GameObject Plane;
    public Vector3 DesiredPos;
    public LayerMask layerMask;
    public LayerMask layerMaskRed;

    void Start()
    {
        DesiredPos = transform.position;
    }

    void Update()
    {
        RaycastHit _hit;
        if (TouchManager.Instance.CurrentGesture != TouchManager.Gestures.None && TouchManager.Instance.CurrentGesture != TouchManager.Gestures.DoubleTap)
        {
            GameObject _plane = Instantiate(Plane, transform.position, Quaternion.identity) as GameObject;
            if (Physics.Raycast(DesiredPos, new Vector3(TouchManager.Instance.SwipeAxis.x, 0, TouchManager.Instance.SwipeAxis.y), out _hit, 1, layerMask))
            {
                
                Destroy(_hit.collider.gameObject);
                DesiredPos += new Vector3(TouchManager.Instance.SwipeAxis.x, 0, TouchManager.Instance.SwipeAxis.y);
                
            }
            else if(!Physics.Raycast(DesiredPos, new Vector3(TouchManager.Instance.SwipeAxis.x, 0, TouchManager.Instance.SwipeAxis.y), out _hit, 1, layerMaskRed))
            {
                DesiredPos += new Vector3(TouchManager.Instance.SwipeAxis.x, 0, TouchManager.Instance.SwipeAxis.y);
            }

            
        }
        transform.position = Vector3.Lerp(transform.position, DesiredPos, 10f * Time.deltaTime);
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transform.position + Vector3.up * 9.5f, 10f * Time.deltaTime);
    }
}
