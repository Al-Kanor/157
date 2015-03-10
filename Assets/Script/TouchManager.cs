using UnityEngine;
using System.Collections;

public class TouchManager : Singleton<TouchManager>
{
    Vector2 oldPos;
    public Vector2 SwipeAxis;
    float timer = -0.4f;
    public bool CheckSwips = true;
    public enum Gestures { DoubleTap, SwipeLeft, SwipeRight, SwipeUp, SwipeDown, None };
    public Gestures CurrentGesture = Gestures.None;
    public float TouchSensibilityRatio;
    public float MouseSensibilityRatio;
    public float SwipeTolerance;
    public float DoubleTapWait;

    void Update()
    {
        if ((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            if (Input.touchCount > 0)
                oldPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            CheckSwips = true;
        }

        if (CheckSwips)
        {
            if ((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved) || Input.GetMouseButton(0))
            {
                float _height = Screen.height;
                if (_height == 0 || _height == float.NaN)
                    _height = 1280.0f;

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
                Vector2 delta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                delta *=  MouseSensibilityRatio;

                if (delta == Vector2.zero && Input.touchCount > 0)
                {
                    // delta = Input.touches[0].deltaPosition * TouchSensibilityRatio * 1280.0f / _height;
                    delta = ((Vector2)Camera.main.ScreenToWorldPoint(Input.touches[0].position) - oldPos) * TouchSensibilityRatio;
                    oldPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                }
#else
               // Vector2 delta = Input.touches[0].deltaPosition * TouchSensibilityRatio * 1280.0f / _height;
               Vector2 delta = ((Vector2)Camera.main.ScreenToWorldPoint(Input.touches[0].position) - oldPos) * TouchSensibilityRatio;
               oldPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.touches[0].position);
#endif

                if (delta.x > SwipeTolerance )
                {
                    Debug.Log("right");
                    CurrentGesture = Gestures.SwipeRight;
                    SwipeAxis = Vector2.right;
                    CheckSwips = false;
                }

                else if (delta.x < -SwipeTolerance  )
                {
                    Debug.Log("left");
                    CurrentGesture = Gestures.SwipeLeft;
                    SwipeAxis = -Vector2.right;
                    CheckSwips = false;
                }

                else if (delta.y < -SwipeTolerance)
                {
                    Debug.Log("down");
                    CurrentGesture = Gestures.SwipeDown;
                    SwipeAxis = -Vector2.up;
                    CheckSwips = false;
                }

                else if (delta.y > SwipeTolerance)
                {
                    Debug.Log("up");
                    CurrentGesture = Gestures.SwipeUp;
                    SwipeAxis = Vector2.up;
                    CheckSwips = false;
                }
            }

            if ((Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)) || Input.GetMouseButtonUp(0))
            {
                if (timer <= 0)
                {
                    CheckDoubleTap();
                }
                CheckSwips = true;
            }
        }
    }

    void LateUpdate()
    {
        HandleGesture();
    }

    public void HandleGesture()
    {
        if (CurrentGesture != Gestures.None)
            CurrentGesture = Gestures.None;
    }


    void CheckDoubleTap()
    {
        StopAllCoroutines();
        StartCoroutine(DoubleTap());
    }

    IEnumerator DoubleTap()
    {
        timer = DoubleTapWait;
        yield return new WaitForEndOfFrame();
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            if ((Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Stationary || Input.touches[0].phase == TouchPhase.Moved)) || Input.GetMouseButton(0))
            {
                CurrentGesture = Gestures.DoubleTap;
                Debug.Log("doubleTap");
                timer = -1;
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }

    }
}
