using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject credits_canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        credits_canvas.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void Credits()
    {
        StartCoroutine(PlayCredits());
    }

    private IEnumerator PlayCredits()
    {
        credits_canvas.SetActive(true);
        yield return new WaitForSeconds(5f);
        credits_canvas.SetActive(false);
    }
}
