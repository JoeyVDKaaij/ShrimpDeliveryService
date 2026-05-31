using UnityEngine;
using TMPro;

public class ScoreTextUpdater : MonoBehaviour
{
    private TMP_Text scoreComp;

    private void Start()
    {
        scoreComp = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        scoreComp.SetText(Mathf.RoundToInt(GameManager.instance.Score).ToString());
    }
}
