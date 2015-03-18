using UnityEngine;
using System.Collections;

public class VehicleScript : MonoBehaviour
{

    private bool inPreparation = false;
    private bool tropTard = false;
    private bool inVehicle = false;
    private bool isRiding = false;
    public float movementSpeed = 10f;
    public bool activated = false;


    void Start()
    {

    }


    void Update()
    {

        if (GameManager.Instance.player != null && Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) <= 0.1f)
        {
            GameManager.Instance.player.vehicle = this;
            Debug.Log("c'est parti");

          

        }





    }


    IEnumerator Ride()
    {
        isRiding = true;

        yield return new WaitForSeconds(10);

        Destroy(gameObject);

    }

}
