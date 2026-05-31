using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject ctrlMenu;
    public GameObject ctrlCam;


    public void OpenControls()
    {
        Debug.Log("OpenControls()");
        mainMenu.SetActive(false);
        ctrlCam.SetActive(true);
        ctrlMenu.SetActive(true);
    }

    public void CloseControls()
    {
        Debug.Log("CloseControls()");
        ctrlMenu.SetActive(false);
        ctrlCam.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void StartGame()
    {
        Debug.Log("StartGame()");
        SceneManager.LoadScene(1);
    }
}
