using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text coinText;
    public Text DNAText;

    public GameObject shopUI;
    public GameObject upgradeUI;
    public GameObject optionUI;
    public GameObject Slime;
    public Text[] texts;
    public Image slimeBtnDelay;
    public Text slimeAmountText;
    public AchieveManager achieveManager;
    public GameObject warning;
    [SerializeField] GameObject warning2;
    [SerializeField] GameObject moneyParticle;

    public TextMesh fusionT;
    public GameObject fusionMessage;
    [SerializeField] SpriteRenderer background;
    [SerializeField] Sprite[] backs;
    bool isDelay;
    public int summonSlimeCost = 0;
    int _coin;

    float summonDelay;

    int maxSlime;

    float fillNum;

    int cp = 10;
    public int coin
    {
        get { return _coin; }
        set { _coin = value; }
    }
    int _DNA;
    public int DNA
    {
        get { return _DNA; }
        set { _DNA = value; }
    }

    public float autoProduceTime;

    public int isBackChange;
    public int summonSpeedNum;
    int summonSpeedCost;
    public int maxSlimeNum;
    int maxSlimeCost;
    public int slimeStartLevelNum;
    int slimeStartLevelCost;
    public int slimeLvNum;
    int slimeLvCost;

    // Start is called before the first frame update
    void Start()
    {
        autoProduceTime = 50f;
        fillNum = 0;

        //1단계 레벨/가격

        SetBackground();
        SetSummonSpeed();
        SetMaxSlime();
        SetSlimeStartLevel();
        SetSlimeLevel();

}

    // Update is called once per frame
    void Update()
    {
        coinText.text = _coin.ToString();
        DNAText.text = _DNA.ToString();

        if(fillNum > 0)
        {
            fillNum += -1 / summonDelay *Time.deltaTime;
            if (fillNum < 0)
                fillNum = 0;
        }
        slimeBtnDelay.fillAmount = fillNum;
      
        if(Input.GetKeyDown(KeyCode.D))
        {
            coin += 10000000;
            DNA += 100;
            summonDelay = 0.1f;
            maxSlime = 100;
        }
        int m = GameObject.FindGameObjectsWithTag("Slime").Length;
        slimeAmountText.text = m.ToString() + "/" + maxSlime.ToString();
        slimeAmountText.color = m == maxSlime ? Color.red : new Color(0.09f, 0.47f, 0);
    }

    public void shopTrigger()
    {
        //다른 UI 비활성화 후 상점 UI 활성화
        if (shopUI.activeSelf == false)
        {
            upgradeUI.SetActive(false);
            optionUI.SetActive(false);
            shopUI.SetActive(true);
        }
        else
        {
            shopUI.SetActive(false);
        }
    }
    public void upgradeTrigger()
    {
        //다른 UI 비활성화 후 업그레이드 UI 활성화
        if (upgradeUI.activeSelf == false)
        {
            shopUI.SetActive(false);
            optionUI.SetActive(false);
            upgradeUI.SetActive(true);
        }
        else
        {
            upgradeUI.SetActive(false);
        }
    }
    public void optionTrigger()
    {
        //다른 UI 비활성화 후 업그레이드 UI 활성화
        if (optionUI.activeSelf == false)
        {
            shopUI.SetActive(false);
            upgradeUI.SetActive(false);
            optionUI.SetActive(true);
        }
        else
        {
            optionUI.SetActive(false);
        }
    }

    public void SummonSlime()
    {
        if (!isDelay && coin >= summonSlimeCost)
        {
            int m = GameObject.FindGameObjectsWithTag("Slime").Length;
            if (m >= maxSlime)
            {
                warning2.SetActive(false);
                warning2.SetActive(true);
                return;
            }

            Slime s = Instantiate(Slime, new Vector2(0, Random.Range(-3.5f, 0f)), transform.rotation).GetComponent<Slime>();
            s.gameManager = this;
            s.achieveManager = achieveManager;
            s.fusionMessage = fusionMessage;
            s.fusionT = fusionT;
            
            
            s.GetComponent<Slime>().slimeLevel = slimeStartLevelNum;

            coin -= summonSlimeCost;
            if (summonSlimeCost == 0)
                summonSlimeCost = 10;
            else
                summonSlimeCost += cp;

            texts[4].text = summonSlimeCost.ToString();

            isDelay = true;
            fillNum = 1;
            if (!achieveManager.achieves[0])
                achieveManager.GetAchievement(1);
            Invoke("SummonDelay", summonDelay);

        }
        else if(coin < summonSlimeCost && !isDelay)
        {
            warning.SetActive(false);
            warning.SetActive(true);
        }
    }
    
    void SummonDelay()
    {
        isDelay = false;
    }

    void SetBackground()
    {
        background.sprite = backs[isBackChange];
        
        if(isBackChange == 1)
        {
            texts[0].text = "연구소로 업그레이드(엔딩 조건)(o)";
            background.color = new Color(1, 1, 1, 0.35f);
        }
        else
        {
            texts[0].text = "연구소로 업그레이드(엔딩 조건)(x)";
            texts[0].text += "\n10000000코인";

        }
    }
    void SetSummonSpeed()
    {
        summonSpeedCost = (int)(10000 * Mathf.Pow(1.8f, summonSpeedNum - 1));
        summonDelay = 10 - 0.4f * (summonSpeedNum - 1);
        if(summonSpeedNum == 20)
        {
            texts[1].text = "슬라임 생성기 스피드(Lv.Max)";
        }
        else
        {
            texts[1].text = "슬라임 생성기 스피드(Lv." + summonSpeedNum.ToString() + ")";
            texts[1].text += "\n" + summonSpeedCost.ToString() + "코인";

        }
    }
    void SetMaxSlime()
    {
        maxSlime = 5 + (maxSlimeNum - 1);
        maxSlimeCost = (int)(10000 * Mathf.Pow(1.8f,maxSlimeNum -1));

        texts[2].text = "슬라임 농장 규모(Lv." + maxSlimeNum.ToString() + ")";
        texts[2].text += "\n" + maxSlimeCost.ToString() + "코인";
    }
    void SetSlimeStartLevel()
    {
        slimeStartLevelCost = (int)(1900000 * Mathf.Pow(1.9f,slimeStartLevelNum -1));


        if(slimeStartLevelNum == 5)
        {
            texts[3].text = "슬라임 생성기 버전(Lv.Max)";
        }
        else
        {
            texts[3].text = "슬라임 생성기 버전(Lv." + slimeStartLevelNum.ToString() + ")";
            texts[3].text += "\n" + slimeStartLevelCost.ToString() + "코인";
        }


    }
    void SetSlimeLevel()
    {
        slimeLvCost = (int)(500 * Mathf.Pow(2f,slimeLvNum - 1));

        texts[5].text = "슬라임 품질 개선(Lv." + slimeLvNum.ToString() + ")";
        texts[5].text += "\n" + slimeLvCost.ToString() + "코인";
    }


    public void UpgradeBack()
    {
        if(coin >= 10000000 && isBackChange == 0)
        {
            coin -= 10000000;
            Instantiate(moneyParticle);
            isBackChange = 1;
            SetBackground();
        }
        else if(coin < 10000000)
        {
            warning.SetActive(false);
            warning.SetActive(true);
        }
    }
    public void UpgradeSummonSpeed()
    {
        if (coin >= summonSpeedCost && summonSpeedNum < 15)
        {
            coin -= summonSpeedCost;
            summonSpeedNum++;
            Instantiate(moneyParticle);
            SetSummonSpeed();
        }
        else if (coin < summonSpeedCost)
        {
            warning.SetActive(false);
            warning.SetActive(true);
        }
    }
    public void UpgradeMaxSlime()
    {
        if (coin >= maxSlimeCost)
        {
            coin -= maxSlimeCost;
            maxSlimeNum++;
            Instantiate(moneyParticle);
            SetMaxSlime();
        }
        else if (coin < maxSlimeCost)
        {
            warning.SetActive(false);
            warning.SetActive(true);
        }
    }
    public void UpgradeSlimeStartLevel()
    {
        if (slimeStartLevelNum >= 5)
            return;

        if (coin >= slimeStartLevelCost)
        {
            coin -= slimeStartLevelCost;
            slimeStartLevelNum++;
            summonSlimeCost += 10000;
            Instantiate(moneyParticle);
            SetSlimeStartLevel();
        }
        else if (coin < slimeStartLevelCost)
        {
            warning.SetActive(false);
            warning.SetActive(true);
        }
    }

    public void UpgradeSlime()
    {
        if (coin >= slimeLvCost)
        {
            coin -= slimeLvCost;
            slimeLvNum++;

            Instantiate(moneyParticle);
            SetSlimeLevel();
        }
        else if (coin < slimeLvCost)
        {
            warning.SetActive(false);
            warning.SetActive(true);
        }
    }
}
