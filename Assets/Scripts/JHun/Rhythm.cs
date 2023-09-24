using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhythm : MonoBehaviour
{
    [SerializeField] private ParticleSystem RhythmEffect; // 리듬을 맞추기 위한 이펙트
    [SerializeField] private ParticleSystem PerfectEffect; // 퍼펙트 시 발생하는 이펙트
    ParticleSystemRenderer RhythmRenderer; // 이펙트 렌더러
    [SerializeField] private Material[] RythmMaterial; // 이펙트 매테리얼
    public float Speed; // 이펙트 속도
    private float EffectTime; // 현재 이펙트의 시간
    private float Per; // 리듬 싱크
    [SerializeField] float Range; // 퍼펙트 체크 범위

    void Awake()
    {
        RhythmEffect.playbackSpeed = Speed;
        RhythmRenderer = RhythmEffect.GetComponent<ParticleSystemRenderer>();
        Per = RhythmManager.Instance.RhythmSync;
    }


    void Update()
    {
        EffectTime = RhythmEffect.time;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(EffectTime);
            if (EffectTime < Per + Speed * Range && EffectTime > Per - Speed * Range)
            {
                Debug.Log("Perfect!");
                PerfectEffect.Play();
                /*int i = Random.RandomRange(0, RythmMaterial.Length);
                RhythmRenderer.material = RythmMaterial[i];*/
            }
            else
            {
                Debug.Log("Bad!");
                /*int i = Random.RandomRange(0, RythmMaterial.Length);
                RhythmRenderer.material = RythmMaterial[i];*/
            }
        }
    }
}
