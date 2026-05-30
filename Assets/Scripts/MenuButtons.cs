using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject ctrlMenu;
    public GameObject ctrlCam;


    public void OpenControls()
    {
        mainMenu.SetActive(false);
        ctrlCam.SetActive(true);
        ctrlMenu.SetActive(true);
    }

    public void CloseControls()
    {
        ctrlMenu.SetActive(false);
        ctrlCam.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
