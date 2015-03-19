using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    #region Attributs publics
    public float speed = 10;
    public float vehicleSpeed = 20;
    public float stunDuration = 2;
    public float invincibilityDuration = 1;
    public float cameraSpeed = 3;
    public float cameraHeight = 5.75f;
    public float cameraDist = 3.5f;
    public GameObject dynamitePrefab;
    public LayerMask layerMaskBlock;
    public LayerMask oreMaskBlock;
    public LayerMask dynamiteLayerMask;
    public VehicleScript vehicle = null;
    public Block exploding = null;
    public Color color1 = new Color(0, 255, 0, 0.5f);
    public Color normalColor = new Color(255, 0, 0, 1);
    public int vehicleBlockLimit = 30;
    public bool needsToBeRed = false;
    public int vehicleBlockCount = 0;
    //public bool destroy = false;



    #endregion

    #region Attributs privés
    private Vector3 targetPos;
    private bool isMovable = true;
    private bool isStunable = true;
    private bool stuned = false;
   
    //private bool isGone = false;
    private Quaternion dynamiteRotation = Quaternion.identity;

    #endregion

    #region Accesseurs
    public bool Stuned
    {
        get { return stuned; }
        set
        {
            if (isStunable)
            {
                stuned = value;
            }
            if (stuned)
            {
                StartCoroutine("UpdateStun");
            }
        }
    }
    #endregion

    #region Méthodes publiques

    #endregion

    #region Méthodes privées
    void Move()
    {
        // Minerai catch before movement
        RaycastHit hit;
        // Forward
        if (Physics.Raycast (targetPos, transform.forward, out hit, 1, oreMaskBlock)) {
            hit.collider.GetComponent<Ore> ().Target = transform;
        }
        // Left
        if (Physics.Raycast (targetPos, -transform.right, out hit, 1, oreMaskBlock)) {
            hit.collider.GetComponent<Ore> ().Target = transform;
        }
        // Right
        if (Physics.Raycast (targetPos, transform.right, out hit, 1, oreMaskBlock)) {
            hit.collider.GetComponent<Ore> ().Target = transform;
        }
        
        targetPos += new Vector3(TouchManager.Instance.SwipeAxis.x, 0, TouchManager.Instance.SwipeAxis.y);

        // Rotation
        float y = TouchManager.Instance.SwipeAxis.x * 90;
        if (-1 == TouchManager.Instance.SwipeAxis.y)
        {
            y = 180;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));

        // Minerai catch after movement
        // Forward
        if (Physics.Raycast (targetPos, transform.forward, out hit, 1, oreMaskBlock)) {
            hit.collider.GetComponent<Ore> ().Target = transform;
        }
        // Left
        if (Physics.Raycast (targetPos, -transform.right, out hit, 1, oreMaskBlock)) {
            hit.collider.GetComponent<Ore> ().Target = transform;
        }
        // Right
        if (Physics.Raycast (targetPos, transform.right, out hit, 1, oreMaskBlock)) {
            hit.collider.GetComponent<Ore> ().Target = transform;
        }

        isMovable = false;
        BlocksManager.Instance.UpdateBlocks(targetPos, TouchManager.Instance.SwipeAxis);
    }

    void Start()
    {
        targetPos = transform.position;
        normalColor = GameManager.Instance.player.transform.FindChild("torche").GetComponent<Light>().color;
        vehicleBlockCount = vehicleBlockLimit;



    }

    void ThrowDynamite()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1, dynamiteLayerMask))
        {
            return;
        }

        dynamiteRotation.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        //GameObject dynamiteObject = Instantiate (dynamitePrefab, transform.position - Vector3.up / 2, dynamiteRotation) as GameObject;
        Instantiate(dynamitePrefab, transform.position - Vector3.up / 2, dynamiteRotation);

    }

    void Update()
    {
        if(vehicle!=null && vehicleBlockCount>=vehicleBlockLimit)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else if(vehicle == null && transform.GetChild(0).gameObject.activeSelf==false)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        if (vehicle != null && needsToBeRed == true)
        {
            GameManager.Instance.player.transform.FindChild("torche").GetComponent<Light>().color = color1;
            needsToBeRed = false;
        }

        #region Mouvement sans véhicule
        RaycastHit hit;
        if (isMovable && !stuned && TouchManager.Instance.CurrentGesture != TouchManager.Gestures.None && vehicle == null)
        {
            if (Physics.Raycast(targetPos, new Vector3(TouchManager.Instance.SwipeAxis.x, 0, TouchManager.Instance.SwipeAxis.y), out hit, 1, layerMaskBlock))
            {

                if ("Empty Block" == hit.collider.tag)
                {
                    hit.collider.gameObject.GetComponent<Block>().Die();
                    if (exploding == null)
                    {
                        ThrowDynamite();
                        Move();
                        transform.GetChild(0).GetComponent<Animation>().Stop();
                        transform.GetChild(0).GetComponent<Animation>().Play("Blast");
                    }
                }
                if ("Vehicle Block" == hit.collider.tag)
                {
                    hit.collider.gameObject.GetComponent<Block>().Die();
                    transform.GetChild(0).GetComponent<Animation>().Stop();
                    transform.GetChild(0).GetComponent<Animation>().Play("Blast");
                    vehicleBlockCount = vehicleBlockLimit;
                    //destroy = false;
                }
            }
            else
            {
                ThrowDynamite();
                Move();
                transform.GetChild(0).GetComponent<Animation>().Play("Walk");
            }
        }
        #endregion

        #region Mouvement véhicule

        if (isMovable && !stuned && vehicle != null)
        {

            if (needsToBeRed == false)
            {
                needsToBeRed = true;
            }


            if (Physics.Raycast(targetPos, new Vector3(TouchManager.Instance.SwipeAxis.x, 0, TouchManager.Instance.SwipeAxis.y), out hit, 1, layerMaskBlock))
            {
                if ("Empty Block" == hit.collider.tag)
                {
                    hit.collider.gameObject.GetComponent<Block>().Die();
                    ThrowDynamite();
                    Move();
                    transform.GetChild(0).GetComponent<Animation>().Stop();
                    transform.GetChild(0).GetComponent<Animation>().Play("Blast");
                    vehicleBlockCount--;
                }
                if ("Vehicle Block" == hit.collider.tag)
                {
                    //BlocksManager.Instance.DestroyBlock (hit.collider.gameObject);
                    hit.collider.gameObject.GetComponent<Block>().Die();
                    transform.GetChild(0).GetComponent<Animation>().Stop();
                    transform.GetChild(0).GetComponent<Animation>().Play("Blast");
                    vehicleBlockCount = vehicleBlockLimit;
                }
                if ("Ore Block" == hit.collider.tag)
                {
                    hit.collider.gameObject.GetComponent<Block>().Die();
                    Move();
                    vehicleBlockCount--; //minerai ici

                }
                if ("Explosive Block" == hit.collider.tag)
                {
                    hit.collider.gameObject.GetComponent<Block>().Die();
                    vehicleBlockCount--;
                }
            }
            else
            {
                ThrowDynamite();
                Move();
                transform.GetChild(0).GetComponent<Animation>().Play("Walk");
                vehicleBlockCount--;
            }
            if (vehicleBlockCount < 0)
            {
                GameManager.Instance.player.transform.FindChild("torche").GetComponent<Light>().color = normalColor;
                vehicle = null;
                Transform _vehicleTransform = transform.FindChild("Vehicle(Clone)");
                Destroy(_vehicleTransform.gameObject);
            }

        }
        #endregion


        if (vehicle != null)
            transform.position = Vector3.Lerp(transform.position, targetPos, vehicleSpeed * Time.deltaTime);
        else
            transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            transform.position = targetPos; // Clamp

            // Minerai catch
            /*
            //RaycastHit hit;
            if (Physics.Raycast(transform.position - Vector3.up, Vector3.up, out hit, 1, oreMaskBlock))
            {
                hit.collider.GetComponent<Ore>().Target = transform;
            }

            if (0 == Random.Range(0, 10))
            {
                transform.GetChild(0).GetComponent<Animation>().PlayQueued("Look");
            }
            else
            {
                transform.GetChild(0).GetComponent<Animation>().PlayQueued("Idle");
            }
            */
            isMovable = true;
        }

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(transform.position.x, cameraHeight, transform.position.z - cameraDist), cameraSpeed * Time.deltaTime);
    }

    IEnumerator UpdateStun()
    {
        if (null == vehicle)
        {


            stuned = true;
            isStunable = false;
            transform.GetChild(0).GetComponent<Animation>().Play("Stun");
            yield return new WaitForSeconds(stunDuration);
            stuned = false;
            transform.GetChild(0).GetComponent<Animation>().Play("Look");
            yield return new WaitForSeconds(invincibilityDuration);
            isStunable = true;
        }
    }
    #endregion

    /*IEnumerator VehiculePreparation()
    {
              
                GameManager.Instance.player.transform.FindChild("torche").GetComponent<Light>().color = color1;
                yield return new WaitForSeconds(1);
                GameManager.Instance.player.transform.FindChild("torche").GetComponent<Light>().color = color2;
                yield return new WaitForSeconds(1);
                GameManager.Instance.player.transform.FindChild("torche").GetComponent<Light>().color = color3;
                yield return new WaitForSeconds(1);
                isGone = true;
    }*/
}
