using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInput : MonoBehaviour
{
    [SerializeField] private GameObject detected_canvas;
    [SerializeField] private GameObject success_canvas;
    [SerializeField] private GameObject credits_canvas;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        detected_canvas.SetActive(false);
        success_canvas.SetActive(false);
        credits_canvas.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Quit()
    {
        SceneManager.LoadSceneAsync("StartMenu");
    }

    public void TryAgain()
    {
        detected_canvas.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }

    public void PlayCredits()
    {
        credits_canvas.SetActive(true);
    }

    public void Detected()
    {
        detected_canvas.SetActive(true);
        Time.timeScale = 0;
    }
    
}
