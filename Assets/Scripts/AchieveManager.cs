using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchieveManager : MonoBehaviour
{
    public bool[] achieves = new bool[11];
    string[] messages = { "�⺻ ","������ ", "���� ", "���̽�ũ�� ", "�Ϻ��� ", "���ϻ� ",
        "ȣ�� ", "�ؾ ", "� ", "���ǽ� ", "�δ�� " };
    [SerializeField] GameObject achieveUI;

  

    public void GetAchievement(int level)
    {
        achieves[level - 1] = true;
        achieveUI.SetActive(true);
        Text t = achieveUI.GetComponentInChildren<Text>();
        for(int i=0; i<messages.Length;i++)
        {
            if(i == level-1)
            {
                t.text = messages[i] + "������(Lv." + level.ToString() + ") ����!";
            }
        }
        Invoke("SetActiveF", 2.5f);
    }

    void SetActiveF()
    {
        achieveUI.SetActive(false);
    }
}
