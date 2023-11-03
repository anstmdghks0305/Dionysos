using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;

public class SoundControlUI : MonoBehaviour
{
    public Slider MasterSlider;
    public Slider BGMSlier;
    public Slider SFXSlider;
    public void VolumeAll()
    {
        AudioListener.volume = (MasterSlider.value * 20) / 100f;
    }
    public void VolumeBGM()
    {
        SoundManager.VolumeBGM = (BGMSlier.value * 20) / 100f;
    }
    public void VolumeSFX()
    {
        SoundManager.VolumeSFX = (SFXSlider.value * 20) / 100f;
    }

    public void ExitButtonDown()
    {
        this.gameObject.SetActive(false);
    }
}
