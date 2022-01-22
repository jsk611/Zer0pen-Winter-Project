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
    public Image slimeBtnDelay;
    bool isDelay;
    int _coin;

    int maxSlime;

    float fillNum;
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



    // Start is called before the first frame update
    void Start()
    {
        _coin = 0;
        _DNA = 0;
        autoProduceTime = 50f;
        fillNum = 0;
        maxSlime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = _coin.ToString();
        DNAText.text = _DNA.ToString();

        if(fillNum > 0)
        {
            fillNum += -1 / 5f *Time.deltaTime;
            if (fillNum < 0)
                fillNum = 0;
        }
        slimeBtnDelay.fillAmount = fillNum;
      
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
        if(!isDelay)
        {
            int m = GameObject.FindGameObjectsWithTag("Slime").Length;
            if (m >= maxSlime) return;

            GameObject s = Instantiate(Slime, new Vector2(0, Random.Range(-3.5f, 0f)), transform.rotation);
            s.GetComponent<Slime>().gameManager = this;
            isDelay = true;
            fillNum = 1;
            Invoke("SummonDelay", 5f);

        }
    }
    
    void SummonDelay()
    {
        isDelay = false;
    }

    public void UpgradeSlime()
    {

    }

}
