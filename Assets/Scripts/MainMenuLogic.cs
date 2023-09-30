using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene("PlayScene");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
