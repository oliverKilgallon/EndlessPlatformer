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
}
