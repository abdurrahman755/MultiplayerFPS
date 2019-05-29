using System.Collections;
using UnityEngine;

public class ScoreBoard : MonoBehaviour {

    [SerializeField]
    GameObject playerScoreBoardItem;
    [SerializeField]
    Transform playerScoreboardList;

    private void OnEnable()
    {
        Player[] players = GameManager.GetAllPlayers();

        foreach(Player player in players)
        {
            GameObject itemGO = (GameObject)Instantiate(playerScoreBoardItem, playerScoreboardList);
            PlayerScoreboardItem item = itemGO.GetComponent<PlayerScoreboardItem>();
            if (item != null)
            {
                item.Setup(player.username, player.kills, player.deaths);
            }
        }
    }

    private void OnDisable()
    {
        foreach(Transform child in playerScoreboardList)
        {
            Destroy(child.gameObject);
        }
    }

}
