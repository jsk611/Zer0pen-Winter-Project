using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject shopUI;
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
}
