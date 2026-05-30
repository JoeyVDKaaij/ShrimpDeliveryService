using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Display display in Display.displays)
        {
            display.Activate(1280, 720, Screen.resolutions[0].refreshRateRatio);
            print(display);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
