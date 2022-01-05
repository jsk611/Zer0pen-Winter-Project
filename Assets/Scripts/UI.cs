using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject shopUI;
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
}
