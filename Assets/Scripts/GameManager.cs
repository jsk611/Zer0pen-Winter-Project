using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text coinText;
    public Text DNAText;

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
}
