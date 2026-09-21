using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class Ritzy : MonoBehaviour
{
    [SerializeField] private GameObject success_canvas;
    [SerializeField] private GameObject prompt_canvas;
    [SerializeField] private GameObject interact_canvas;
    [SerializeField] private float run_speed = 5;
    private Rigidbody2D rb;
    private CapsuleCollider2D collider;
    private Vector2 move_input;
    private Animator animator;
    private interact_script nearby_interactable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nearby_interactable = null;
        prompt_canvas.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        interact_canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = move_input * run_speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            if (!interact_canvas.activeSelf)
            {
                prompt_canvas.SetActive(true);
            }
            nearby_interactable = collision.GetComponent<interact_script>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            prompt_canvas.SetActive(false);
            nearby_interactable = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("End"))
        {
            StartCoroutine(PlayEnd());
        }
    }


    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("is_running", true);

        if (context.canceled)
        {
            animator.SetBool("is_running", false);
        }
        move_input = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", move_input.x);
        animator.SetFloat("InputY", move_input.y);
    }

    private IEnumerator PlayEnd()
    {
        success_canvas.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(4f);
        SceneManager.LoadSceneAsync("StartMenu");
    }

    #region Interact Methods

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (nearby_interactable != null)
            {
                prompt_canvas.SetActive(false);
                nearby_interactable.StartInteract();
            }
        }
    }
    
    // public void StartInteract()
    // {
    //     StartCoroutine(ShowInteract());
    // }
    //
    // private IEnumerator ShowInteract()
    // {
    //     interact_canvas.SetActive(true);
    //     TMP_Text interact_text = interact_canvas.GetComponentInChildren<TMP_Text>();
    //     interact_text.SetText(nearby_interactable.prompt);
    //     nearby_interactable.door.GetComponent<Doors>().OpenDoor();
    //     yield return new WaitForSeconds(5f);
    //     interact_canvas.SetActive(false);
    // }
    #endregion
    
}