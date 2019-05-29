using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerScore))]
public class PlayerScore : MonoBehaviour {

    int lastKills = 0;
    int lastDeaths = 0;

    Player player;

    void Start()
    {
        player = GetComponent<Player>();
        StartCoroutine(SyncScoreLoop());
    }

    private void OnDestroy()
    {
        if (player != null)
            SyncNow();
    }

    IEnumerator SyncScoreLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            SyncNow();
        }
    }

    void SyncNow()
    {
        if (UserAccountManager.IsLoggedIn)
        {
            UserAccountManager.instance.GetData(OnDataReceived);
        }
    }

    void OnDataReceived(string data)
    {
        if (player.kills <= lastKills && player.deaths <= lastDeaths)
            return;

        int killsSinceLast = player.kills - lastKills;
        int deathsSinceLast = player.deaths - lastDeaths;

        if (killsSinceLast == 0 && deathsSinceLast == 0)
            return;

        int kills = DataTranslator.DataToKills(data);
        int deaths = DataTranslator.DataToDeaths(data);

        int newKills = killsSinceLast + kills;
        int newDeaths = deathsSinceLast + deaths;

        string newData = DataTranslator.ValuesToData(newKills, newDeaths);

        //Debug.Log("syncing: " + newData);

        lastKills = player.kills;
        lastDeaths = player.deaths;

        UserAccountManager.instance.SendData(newData);
    }

}
