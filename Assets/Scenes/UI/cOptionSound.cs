using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cOptionSound : MonoBehaviour
{
    public GameObject scrObject;
    private Slider slider;

    void Start()
    {
        slider = scrObject.GetComponent<Slider>();

        if (this.gameObject.name == "Slider_Background")
        {
            if (PlayerPrefs.GetFloat("Bgm") == 0f)
                slider.value = AudioManager.instance.bgmVolume;
            else
                slider.value = PlayerPrefs.GetFloat("Bgm");

            slider.onValueChanged.AddListener(OnSliderValueChange_Bgm);
        }
        else if (this.gameObject.name == "Slider_Sfx")
        {
            if (PlayerPrefs.GetFloat("Sfx") == 0f)
                slider.value = AudioManager.instance.sfxVolume;
            else
                slider.value = PlayerPrefs.GetFloat("Sfx");

            slider.onValueChanged.AddListener(OnSliderValueChange_Sfx);
        }
    }

    void OnSliderValueChange_Bgm(float value)
    {
        AudioManager.instance.SetBgmVolume(value);

        PlayerPrefs.SetFloat("Bgm", value);
    }

    void OnSliderValueChange_Sfx(float value)
    {
        AudioManager.instance.SetSfxVolume(value);

        PlayerPrefs.SetFloat("Sfx", value);
    }
}
