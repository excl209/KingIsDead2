using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSystem : MonoBehaviour
{
    public void LoadStartMenu() => SceneManager.LoadScene(0);

    public void LoadGame()
    {
        GameObject player = FindObjectOfType<PlayerMovement>().gameObject;
        player.SendMessage("enablePlayer", true);
        SceneManager.LoadScene("WorldMap");
        FindObjectOfType<MusicPlayer>().SendMessage("ChangeMusic");
    }

    public void Instructions(int pageNum)
    {
        FindObjectOfType<MainMenu>().SendMessage("ReadInstructions", pageNum);
    }

    public void BacktoMainMenu()
    {
        FindObjectOfType<MainMenu>().SendMessage("BacktoMainMenu");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(0f);
        GameObject player = FindObjectOfType<PlayerMovement>().gameObject;
        player.SendMessage("enablePlayer", false);
        
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame() => Application.Quit();
}
