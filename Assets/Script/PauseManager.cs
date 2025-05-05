using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Tooltip("Inspector’dan atayacaðýnýz pause menüsü paneli")]
    public GameObject pausePanel;

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            TogglePause();

        if (isPaused && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X)))
            ResumeGame();
    }

    public void TogglePause()
    {
        if (isPaused) ResumeGame();
        else PauseGame();
    }

    public void PauseGame()
    {
        if (pausePanel != null) pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
