using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using TMPro;

public class DragonPicker : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += GetLoadSave;
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoadSave;
    public GameObject energyShieldPrefab;
    public int numEnergyShield = 3;
    public float energyShieldBottomY = -10f;
    public float energyShieldRadius = -5f;
    public List<GameObject> shieldList;
    public TextMeshProUGUI scoreGT;
    private TextMeshProUGUI playerName;

    void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoadSave();
        }
        shieldList = new List<GameObject>();
        
        
        for (int i = 1; i <= numEnergyShield; i++){
            GameObject tShieldGo = Instantiate<GameObject>(energyShieldPrefab);
            tShieldGo.transform.position = new Vector3(0, energyShieldBottomY, 0);
            tShieldGo.transform.localScale = new Vector3(1*i, 1*i, 1*i);
            shieldList.Add(tShieldGo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DragonEggDestroyed(){
        GameObject[] tDragonEggArray = GameObject.FindGameObjectsWithTag("Dragon Egg");
        foreach (GameObject tGO in tDragonEggArray){
            Destroy(tGO);
        }
        int shieldIndex = shieldList.Count - 1;
        GameObject tShieldGo = shieldList[shieldIndex];
        shieldList.RemoveAt(shieldIndex);
        Destroy(tShieldGo);

        if (shieldList.Count == 0){
            GameObject scoreGO = GameObject.Find("Score");
            scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();
            string[] achieveList;
            achieveList = YandexGame.savesData.achivMent;
            achieveList[0] = "Береги щиты!";
            UserSave(int.Parse(scoreGT.text), YandexGame.savesData.bestScore, achieveList);
            YandexGame.NewLeaderboardScores("TOPPLayerScore", int.Parse(scoreGT.text));
            SceneManager.LoadScene("_0scene");
            GetLoadSave();
        }
    }
    public void GetLoadSave()
    {
        Debug.Log(YandexGame.savesData.score);
        GameObject playerNamePreFabGUI = GameObject.Find("PlayerName");
        playerName = playerNamePreFabGUI.GetComponent<TextMeshProUGUI>();
        playerName.text = YandexGame.playerName;
    }

    public void UserSave(int currentScore, int currentBestScore, string[] currentAchieve)
    {
        YandexGame.savesData.score = currentScore;
        if (currentScore > currentBestScore)
        {
            YandexGame.savesData.bestScore = currentScore;
        }
        YandexGame.savesData.achivMent = currentAchieve;
        YandexGame.SaveProgress();
    }
}
