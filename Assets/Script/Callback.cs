using UnityEngine;
using System.Collections;

[System.Serializable]
public class Callback
{
    [SerializeField]
    public MonoBehaviour Script;
    [SerializeField]
    public string Function;
}
