using UnityEngine;

public class UI_PausePanel : MonoBehaviour {
    
    public void Resume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        GameManager.instance.EndGame();
    }

    public void Restart()
    {
        PlayerPrefs.SetInt("highScore", GameManager.instance.player.GetComponent<PlayerStats>().score);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");

        //Reset player position and velocity
        GameManager.instance.player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        GameManager.instance.player.transform.position = new Vector3(0, 1, 0);

        //Unpause game
        Time.timeScale = 1f;
        gameObject.SetActive(false);
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
