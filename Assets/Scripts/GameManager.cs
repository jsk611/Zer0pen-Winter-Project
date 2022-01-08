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
    int _coin;
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


    // Start is called before the first frame update
    void Start()
    {
        _coin = 0;
        _DNA = 0;
    }

    // Update is called once per frame
    void Update()
    {
        

        coinText.text = _coin.ToString();
        DNAText.text = _DNA.ToString();
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
        GameObject s = Instantiate(Slime, new Vector2(0, Random.Range(-3.5f, 0f)), transform.rotation);
        s.GetComponent<Slime>().gameManager = this;
    }

    public void UpgradeSlime()
    {

    }

}
