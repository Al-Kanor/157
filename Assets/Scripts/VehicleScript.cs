using UnityEngine;
using System.Collections;

public class VehicleScript : MonoBehaviour
{

    public float movementSpeed = 10f;
    public bool activated = false;
    GameObject _player;
    bool _destroy;


    void Start()
    {
        _player = GameObject.Find("Player");
        //_destroy = GameObject.Find("Player").GetComponent<Player>().destroy;
    }


    void Update()
    {

        if (GameManager.Instance.player != null && Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) <= 0.1f)
        {
            
                
            
            GameManager.Instance.player.vehicle = this;
            /*if(_destroy ==true)
            {
                Destroy(gameObject);
            }*/
           transform.parent = _player.transform;
            
            /*
            transform.rotation = _player.transform.rotation;
            Debug.Log(transform.rotation);*/
           
            //StartCoroutine ("Ride");

          

        }





    }


   /* IEnumerator Ride()
    {
        isRiding = true;

        yield return new WaitForSeconds(10);

        Destroy(gameObject);

    }*/

}
