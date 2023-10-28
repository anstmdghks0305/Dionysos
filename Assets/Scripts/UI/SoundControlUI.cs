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
        //SoundManager.Instance.listenner.volume = AllSlider.value;
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
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
