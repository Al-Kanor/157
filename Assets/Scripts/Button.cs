using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
    public Callback[] Callbacks;
    public bool CheckIfVisible = true;
    public bool isVisible;
    public bool CheckOnPressed = false;
    public bool pressed;
    public bool CheckImmediatly = false;
    public Vector3 ScaleMofid;
    Vector3 IniPos;

    void Start()
    {
        IniPos = transform.localScale;
    }

    void Update()
    {
        if (CheckIfVisible)
            isVisible = GetComponent<Renderer>().isVisible;
        else
            isVisible = true;

        if (isVisible)
        {
            CheckButton();
        }
    }

    IEnumerator Go()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (var item in Callbacks)
        {
            if (item.Function != "" && item.Script != null)
                item.Script.Invoke(item.Function, 0);
        }
    }

    void CheckButton()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;



        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 1000f) && hit.collider == GetComponent<Collider>())
            {
                if (CheckOnPressed)
                    pressed = true;
            }
        }

        else if (Input.GetMouseButton(0))
        {
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

            if (Physics.Raycast(ray, out hit, 1000f) && hit.collider == GetComponent<Collider>())
            {
                if (CheckOnPressed && pressed)
                {
                    if (transform.localScale != Vector3.Scale(ScaleMofid, IniPos))
                        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.Scale(ScaleMofid, IniPos), 10 * Time.deltaTime);
                }
                else if (!CheckOnPressed)
                    if (transform.localScale != Vector3.Scale(ScaleMofid, IniPos))
                        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.Scale(ScaleMofid, IniPos), 10 * Time.deltaTime);


                if (CheckImmediatly)
                {
                    if (!pressed)
                    {
                        StartCoroutine(Go());
                        pressed = true;
                    }
                }
            }
            else if (hit.collider == null || hit.collider != GetComponent<Collider>())
            {
                transform.localScale = Vector3.Lerp(transform.localScale, IniPos, 10 * Time.deltaTime);
                if (CheckImmediatly)
                {
                    pressed = false;
                }
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(ray, out hit, 1000f) && hit.collider == GetComponent<Collider>())
            {
                if ((!CheckImmediatly && !CheckOnPressed) || (CheckOnPressed && pressed))
                    StartCoroutine(Go());
            }

            pressed = false;
        }
        else if (Physics.Raycast(ray, out hit, 1000f) && hit.collider == GetComponent<Collider>())
        {
            if (transform.localScale != Vector3.Scale(ScaleMofid * 2, IniPos))
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.Scale(ScaleMofid * 2, IniPos), 10 * Time.deltaTime);
        }

        if (!Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit, 1000f) && hit.collider == GetComponent<Collider>())
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.Scale(ScaleMofid * 2, IniPos), 10 * Time.deltaTime);
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, IniPos, 10 * Time.deltaTime);
            }
        }
    }
}
