using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text killCountText; // Assign this in the Inspector

    private int killCount = 0;

    void Start()
    {
        UpdateKillCountUI();
    }

    public void IncreaseKillCount()
    {
        killCount++;
        UpdateKillCountUI();
    }

    void UpdateKillCountUI()
    {
        if (killCountText != null)
        {
            killCountText.text = "Kills: " + killCount.ToString();
        }
    }
}
