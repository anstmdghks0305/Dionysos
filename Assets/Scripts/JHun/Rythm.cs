using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rythm : MonoBehaviour
{
    public ParticleSystem RythmEffect;
    public float Speed;
    [SerializeField] private float EffectTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RythmEffect.playbackSpeed = Speed;
        EffectTime = RythmEffect.time;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (EffectTime < 1.1f && EffectTime > 0.8f)
            {
                Debug.Log("Perfect!");
            }
            else
            {
                Debug.Log("Bad!");
            }
        }
    }
}
