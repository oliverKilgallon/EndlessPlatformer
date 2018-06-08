using UnityEngine;

public class UI_PausePanel : MonoBehaviour {
    
    public void Resume()
    {
        AudioManager.instance.UnPauseSound("Theme");
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        GameManager.instance.Quit();
    }

    public void Restart()
    {
        GameManager.instance.Restart();
    }

    public void EnableOptions()
    {
        gameObject.SetActive(false);

        GameManager.instance.GetComponent<UI_ShowPanels>().optionsPanel.SetActive(true);
    }

    public void ResetScore()
    {
        GameManager.instance.player.GetComponent<PlayerStats>().ResetScore();
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");

        Time.timeScale = 1f;

        gameObject.SetActive(false);
    }
}
