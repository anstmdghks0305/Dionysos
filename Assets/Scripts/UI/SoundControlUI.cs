using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;

public class SoundControlUI : MonoBehaviour
{
    public Slider AllSlider;
    public Slider PlayerSlider;
    public Slider BGMSlier;
    public void VolumeAll()
    {
        AudioListener.volume = AllSlider.value;
    }
    public void VolumeBGM()
    {
        SoundManager.VolumeBGM = BGMSlier.value;
    }
    public void VolumePlayer()
    {
        SoundManager.VolumeBGM = PlayerSlider.value;
    }

    public void ExitButtonDown()
    {
        this.gameObject.SetActive(false);
    }
}
