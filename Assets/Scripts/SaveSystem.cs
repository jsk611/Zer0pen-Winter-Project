using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject slime;
    int[] levels = new int[15];
    // Start is called before the first frame update
    void Awake()
    {
        gameManager.coin = PlayerPrefs.GetInt("coin", 0);
        gameManager.DNA = PlayerPrefs.GetInt("dna", 0);
        gameManager.isBackChange = PlayerPrefs.GetInt("back", 0);
        gameManager.summonSlimeCost = PlayerPrefs.GetInt("summonSlimeCost", 0);
        gameManager.summonSpeedNum = PlayerPrefs.GetInt("summonSpeedNum", 1);
        gameManager.slimeStartLevelNum = PlayerPrefs.GetInt("slimeStartLevelNum", 1);
        gameManager.slimeLvNum = PlayerPrefs.GetInt("slimeLvNum", 1);
        gameManager.maxSlimeNum = PlayerPrefs.GetInt("maxSlimeNum", 1);

        for(int i =0; i<15; i++)
        {
            levels[i] = PlayerPrefs.GetInt("lv" + (i + 1).ToString(), 0);
            for(int j=0;j<levels[i]; j++)
            {
                Slime s = Instantiate(slime).GetComponent<Slime>();
                s.slimeLevel = i+1;
                s.gameManager = gameManager;
                s.achieveManager = gameManager.achieveManager;
                s.fusionMessage = gameManager.fusionMessage;
                s.fusionT = gameManager.fusionT;
            }
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("coin", gameManager.coin);
        PlayerPrefs.SetInt("dna", gameManager.DNA);
        PlayerPrefs.SetInt("back", gameManager.isBackChange);
        PlayerPrefs.SetInt("summonSlimeCost", gameManager.summonSlimeCost);
        PlayerPrefs.SetInt("summonSpeedNum", gameManager.summonSpeedNum);
        PlayerPrefs.SetInt("slimeStartLevelNum", gameManager.slimeStartLevelNum);
        PlayerPrefs.SetInt("slimeLvNum", gameManager.slimeLvNum);
        PlayerPrefs.SetInt("maxSlimeNum", gameManager.maxSlimeNum);

        for (int i = 0; i < 15; i++)
        {
            levels[i] = 0;
        }
        GameObject[] slimes = GameObject.FindGameObjectsWithTag("Slime");
        foreach(var s in slimes)
        {
            Slime slime = s.GetComponent<Slime>();
            levels[slime.slimeLevel - 1]++;
        }

        for (int i = 0; i < 15; i++)
        {
            PlayerPrefs.SetInt("lv" + (i + 1).ToString(), levels[i]);
        }
    }
    public void SaveAndQuit()
    {
        Save();
        Application.Quit();
    }

}
