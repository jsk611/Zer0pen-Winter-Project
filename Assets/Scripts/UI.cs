using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject Slime;
    public void shopTrigger()
    {
        //�ٸ� UI ��Ȱ��ȭ �� ���� UI Ȱ��ȭ
        if (shopUI.activeSelf == false)
            shopUI.SetActive(true);
        else
        {
            shopUI.SetActive(false);
        }
    }

    public void SummonSlime()
    {
        GameObject s = Instantiate(Slime, new Vector2(0,Random.Range(-3.5f,0f)), transform.rotation);
        s.GetComponent<Slime>().gameManager = GetComponent<GameManager>();
    }
}
