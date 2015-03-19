using UnityEngine;
using System.Collections;

public class VehicleScript : MonoBehaviour
{

    public float movementSpeed = 10f;
    GameObject _player;
   private  bool activated;
    int _playerBlockCount = 0;
    


    void Start()
    {
        _player = GameObject.Find("Player");

        _playerBlockCount = GameObject.Find("Player").GetComponent<Player>().vehicleBlockCount;
        
    }


    void Update()
    {



        if (GameManager.Instance.player != null && Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) <= 0.1f && _playerBlockCount > 0)
        {
            if (GameManager.Instance.player.vehicle != this && transform.parent != _player.transform && activated==false)
            {
                transform.rotation = GameObject.Find("Player").transform.rotation;
                activated = true;
                GameManager.Instance.player.vehicle = this;
                transform.parent = _player.transform;
                Debug.Log("saucisse");
            }
      
        }
       
        
        




    }


   /* IEnumerator Ride()
    {
        isRiding = true;

        yield return new WaitForSeconds(10);

        Destroy(gameObject);

    }*/

}
