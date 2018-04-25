using UnityEngine;

public class UI_ShowPanels : MonoBehaviour {

    public GameObject pausePanel;
    public GameObject optionsPanel;
    public GameObject gameOverPanel;

    public void SetAllPanelsActive(bool isActive)
    {
        pausePanel.SetActive(isActive);
        optionsPanel.SetActive(isActive);
        gameOverPanel.SetActive(isActive);
    }

    public void ToggleMenus()
    {
        if (!pausePanel.activeInHierarchy && !optionsPanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            optionsPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
