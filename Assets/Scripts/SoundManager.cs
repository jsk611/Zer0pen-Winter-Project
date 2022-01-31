using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image effectBtn;
    [SerializeField] Image BGMBtn;
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] Slider slider;

    int BGMMute = -1;
    int EffectMute = -1;
    private void Update()
    {
        float sound = slider.value*100f -80f;
        masterMixer.SetFloat("Master", sound);
    }

    public void MuteBGM()
    {
        BGMMute *= -1;
        if(BGMMute == 1)
        {
            masterMixer.SetFloat("BGM", -80);
            BGMBtn.color = new Color(0.5f,0.5f,0.5f);
        }
        else
        {
            masterMixer.SetFloat("BGM", 0);
            BGMBtn.color = new Color(1f, 0.85f, 0f);
        }
    }
    public void MuteEffect()
    {
        EffectMute *= -1;
        if (EffectMute == 1)
        {
            masterMixer.SetFloat("Effect", -80);
            effectBtn.color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            masterMixer.SetFloat("Effect", 0);
            effectBtn.color = new Color(1f, 0.85f, 0f);
        }
    }
}
