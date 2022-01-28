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
    public GameObject Slime;
    public Text[] texts;
    public Image slimeBtnDelay;
    public Text slimeAmountText;
    public AchieveManager achieveManager;
    public GameObject warning;
    [SerializeField] GameObject warning2;
    [SerializeField] GameObject moneyParticle;

    [SerializeField] TextMesh fusionT;
    [SerializeField] GameObject fusionMessage;
    bool isDelay;
    int summonSlimeCost = 0;
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

    int slimeDivisionNum;
    int slimeDivisionCost;
    int summonSpeedNum;
    int summonSpeedCost;
    int maxSlimeNum;
    int maxSlimeCost;
    int slimeStartLevelNum;
    int slimeStartLevelCost;

    // Start is called before the first frame update
    void Start()
    {
        _coin = 0;
        _DNA = 0;
        autoProduceTime = 50f;
        fillNum = 0;
        maxSlime = 5;
        //1단계 레벨/가격
        slimeDivisionNum = 1;
        SetSlimeDivision();
        summonSpeedNum = 1;
        SetSummonSpeed();
        maxSlimeNum = 1;
        SetMaxSlime();
        slimeStartLevelNum = 1;
        SetSlimeStartLevel();

        
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
            coin += 100000;
            DNA += 100;
        }
        int m = GameObject.FindGameObjectsWithTag("Slime").Length;
        slimeAmountText.text = m.ToString() + "/" + maxSlime.ToString();
        slimeAmountText.color = m == maxSlime ? Color.red : new Color(0.09f, 0.47f, 0);
    }

    public void shopTrigger()
    {
        //다른 UI 비활성화 후 상점 UI 활성화
        if (shopUI.activeSelf == false)
            shopUI.SetActive(true);
        else
        {
            shopUI.SetActive(false);
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

    void SetSlimeDivision()
    {
        switch (slimeDivisionNum)
        {
            case 1:
                autoProduceTime = 50f;
                slimeDivisionCost = 100;
                break;
            case 2:
                autoProduceTime = 45f;
                slimeDivisionCost = 1000;
                break;
            case 3:
                autoProduceTime = 40f;
                slimeDivisionCost = 10000;
                break;
            case 4:
                autoProduceTime = 35f;
                slimeDivisionCost = 20000;
                break;
            case 5:
                autoProduceTime = 30f;
                break;

        }
        if(slimeDivisionNum == 5)
        {
            texts[0].text = "분열속도(Lv.Max)";
        }
        else
        {
            texts[0].text = "분열속도(Lv." + slimeDivisionNum.ToString() + ")";
            texts[0].text += "\n" + slimeDivisionCost.ToString() + "코인";

        }
    }
    void SetSummonSpeed()
    {
        switch (summonSpeedNum)
        {
            case 1:
                summonDelay = 5f;
                summonSpeedCost = 100;
                break;
            case 2:
                summonDelay = 4.5f;
                summonSpeedCost = 1000;
                break;
            case 3:
                summonDelay = 4f;
                summonSpeedCost = 10000;
                break;
            case 4:
                summonDelay = 3.5f;
                summonSpeedCost = 100000;
                break;
            case 5:
                summonDelay = 3f;
                break;

        }
        if(summonSpeedNum == 5)
        {
            texts[1].text = "슬라임 생성속도(Lv.Max)";
        }
        else
        {
            texts[1].text = "슬라임 생성속도(Lv." + summonSpeedNum.ToString() + ")";
            texts[1].text += "\n" + summonSpeedCost.ToString() + "코인";

        }
    }
    void SetMaxSlime()
    {
        maxSlime = 5 + (maxSlimeNum - 1);
        maxSlimeCost = 50 * maxSlimeNum * maxSlimeNum * maxSlimeNum;

        texts[2].text = "수용량(Lv." + maxSlimeNum.ToString() + ")";
        texts[2].text += "\n" + maxSlimeCost.ToString() + "코인";
    }
    void SetSlimeStartLevel()
    {
        slimeStartLevelCost = 20000 * slimeStartLevelNum * slimeStartLevelNum;

        texts[3].text = "생성 시작단계(Lv." + slimeStartLevelNum.ToString() + ")";
        texts[3].text += "\n" + slimeStartLevelCost.ToString() + "코인";
    }


    public void UpgradeSlimeDivision()
    {
        if(coin >= slimeDivisionCost && slimeDivisionNum < 5)
        {
            coin -= slimeDivisionCost;
            slimeDivisionNum++;
            Instantiate(moneyParticle);
            SetSlimeDivision();
        }
        else if(coin < slimeDivisionCost)
        {
            warning.SetActive(false);
            warning.SetActive(true);
        }
    }
    public void UpgradeSummonSpeed()
    {
        if (coin >= summonSpeedCost && summonSpeedNum < 5)
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
        if (coin >= slimeStartLevelCost)
        {
            coin -= slimeStartLevelCost;
            slimeStartLevelNum++;
           
            Instantiate(moneyParticle);
            SetSlimeStartLevel();
        }
        else if (coin < slimeStartLevelCost)
        {
            warning.SetActive(false);
            warning.SetActive(true);
        }
    }

    
}
