using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;

public class BGMReset : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.BGMPlayer.clip = null;
    }
}
