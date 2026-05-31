using UnityEngine;
using TMPro;

public class TimerTextUpdater : MonoBehaviour
{
    private TMP_Text textComp;

    private void Start()
    {
        textComp = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        textComp.SetText(Mathf.RoundToInt(GameManager.instance.Timer).ToString());
    }
}
