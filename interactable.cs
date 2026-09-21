using System.Collections;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "interactable", menuName = "Scriptable Objects/interactable")]
public class interactable : ScriptableObject
{
    public string type;
    public bool is_locked;
    public string prompt;
    public GameObject door;
    
}
