using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreboardItem : MonoBehaviour {

    [SerializeField]
    Text usernameText;

    [SerializeField]
    Text KillsText;

    [SerializeField]
    Text deathsText;

    public void Setup (string username, int kills, int deaths)
    {
        usernameText.text = username;
        KillsText.text = "Kills: " + kills;
        deathsText.text = "Deaths: " + deaths; 
    }

}
