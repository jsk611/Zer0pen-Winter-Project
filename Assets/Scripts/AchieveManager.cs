using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchieveManager : MonoBehaviour
{
    public bool[] achieves = new bool[15];
    string[] messages = new string[15]{"±âº» ","±º°í±¸¸¶ ", "ºñ¿ä¶ß ", "¾ÆÀÌ½ºÅ©¸² ", "ÆÏºù¼ö ", "´ÜÆÏ»§ ",
        "È£¶± ", "ºØ¾î»§ ", "¾î¹¬ ", "ÄðÇÇ½º ", "½ºÆÔ ", "ºÎ´ëÂî°³ ", "ÇÇÀÚ ", "ÇÜ¹ö°Å ", "Ä¡Å² " };
    [SerializeField] GameObject achieveUI;

    private void Awake()
    {
        
    }
 

public void GetAchievement(int level)
    {
        achieves[level - 1] = true;
        achieveUI.SetActive(true);
        Text t = achieveUI.GetComponentInChildren<Text>();
        for(int i=0; i<messages.Length;i++)
        {
            if(i == level-1)
            {
                t.text = messages[i] + "½½¶óÀÓ(Lv." + level.ToString() + ") »ý¼º!";
            }
        }
        Invoke("SetActiveF", 2.5f);
    }

    void SetActiveF()
    {
        achieveUI.SetActive(false);
    }
}
