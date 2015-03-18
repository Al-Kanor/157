using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour
{
    
    public Color color1 = new Color(0, 255, 0, 0.5f);
    public Color color2 = new Color(255, 159, 0, 0.5f);
    public Color color3 = new Color(255, 0, 0, 0.5f);
    public Color normalColor = new Color(255, 0, 214, 0.5f);
    private bool inPreparation = false;
    private bool tropTard = false;
    private bool inVehicle = false;
    private bool isRiding=false;
    public float movementSpeed = 10f;
    new Vector3 vehiculeDirection = Vector2.up;
    Player _player;
    private bool activated = false;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }


    void Update()
    {

        if (_player != null && Vector3.Distance(transform.position, _player.transform.position) <= 0.1f && activated == false)
        {
            activated = true;
            if(inVehicle==true)
            {
                StopCoroutine("Ride");
            }

            Debug.Log("c'est parti");
            StartCoroutine("VehiclePreparation");
            inVehicle = true;

        }


        if (inPreparation && TouchManager.Instance.CurrentGesture != TouchManager.Gestures.None || tropTard == true)
        {
            tropTard = false;
            inPreparation = false;
            if (TouchManager.Instance.CurrentGesture != TouchManager.Gestures.None)
            vehiculeDirection = TouchManager.Instance.SwipeAxis;
            Ride();
            Debug.Log(vehiculeDirection);
        }

        if (isRiding)
        {
            Debug.Log("i'mriding");
            _player.transform.position = Vector3.Lerp(transform.position, vehiculeDirection, movementSpeed * Time.deltaTime);
        }
    }

    IEnumerator VehiclePreparation()
    {

        inPreparation = true;
        yield return new WaitForSeconds(1);
        _player.transform.FindChild("torche").GetComponent<Light>().color = color1;
        yield return new WaitForSeconds(1);
        _player.transform.FindChild("torche").GetComponent<Light>().color = color2;
        yield return new WaitForSeconds(1);
        _player.transform.FindChild("torche").GetComponent<Light>().color = color3;
        yield return new WaitForSeconds(1);
        tropTard = true;
    }



    IEnumerator Ride()
    {
        isRiding = true;
        yield return new WaitForSeconds(10);
        isRiding = false;
        inVehicle = false;
        inPreparation = false;
        Destroy(gameObject);
        
    }

}
