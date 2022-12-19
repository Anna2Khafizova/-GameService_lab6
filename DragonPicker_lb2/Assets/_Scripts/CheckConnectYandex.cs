using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using TMPro;


public class CheckConnectYandex : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += CheckSDK;
    private void OnDisable() => YandexGame.GetDataEvent += CheckSDK;
    private TextMeshProUGUI scoreBest;
    // Start is called before the first frame update
    void Start()
    {
        CheckSDK();
        
    }

    public void CheckSDK()
    {
        if (YandexGame.auth == true)
        {
            Debug.Log("User authorization ok");
        }
        else
        {
            Debug.Log("User not authorization");
            YandexGame.AuthDialog();
        }
        YandexGame.RewVideoShow(0);
        GameObject scoreGO = GameObject.Find("BestScore");
        scoreBest = scoreGO.GetComponent<TextMeshProUGUI>();
        scoreBest.text = "Best Score: " + YandexGame.savesData.bestScore.ToString();
        if (YandexGame.savesData.achivMent[0] == null & !GameObject.Find("ListAchieve"))
        {

        }
        else{
            foreach (string value in YandexGame.savesData.achivMent)
            {
                GameObject.Find("ListAchieve").GetComponent<TextMeshProUGUI>().text = GameObject.Find("ListAchieve").GetComponent<TextMeshProUGUI>().text + value + "\n";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
