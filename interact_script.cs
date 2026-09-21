using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class interact_script : MonoBehaviour
{
    [SerializeField] private GameObject interact_canvas;
    [SerializeField] private interactable interact_obj;
    [SerializeField] private Doors obj_door;

    private TMP_Text interact_text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interact_canvas.SetActive(false);
        interact_text = interact_canvas.GetComponentInChildren<TMP_Text>();
    }

    public void StartInteract()
    {
        StartCoroutine(ShowInteract());
    }

    private IEnumerator ShowInteract()
    {
        interact_canvas.SetActive(true);
        TMP_Text interact_text = interact_canvas.GetComponentInChildren<TMP_Text>();
        interact_text.SetText(interact_obj.prompt);
        obj_door.OpenDoor();
        yield return new WaitForSeconds(4f);
        interact_canvas.SetActive(false);
    }
}