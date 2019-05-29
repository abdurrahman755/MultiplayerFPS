using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    public Text killCount;
    public Text deathCount;

    //
    private void Start() {
        if(UserAccountManager.IsLoggedIn)
            UserAccountManager.instance.GetData(OnReceiveData);
    }

    void OnReceiveData(string data)
    {
        if (killCount == null || deathCount == null)
            return;

        killCount.text = DataTranslator.DataToKills(data).ToString() + " Kills";
        deathCount.text = DataTranslator.DataToDeaths(data).ToString() + " Deaths";
    }

}
