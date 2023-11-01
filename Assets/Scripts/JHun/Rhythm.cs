using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class Rhythm : MonoBehaviour
{
    [SerializeField] private ParticleSystem RhythmEffect; // 리듬을 맞추기 위한 이펙트
    [SerializeField] private ParticleSystem PerfectEffect; // 퍼펙트 시 발생하는 이펙트
    ParticleSystemRenderer RhythmRenderer; // 이펙트 렌더러
    [SerializeField] private Material[] RythmMaterial; // 이펙트 매테리얼
    public float Speed; // 이펙트 속도
    private float EffectTime; // 현재 이펙트의 시간
    [SerializeField] private float Sync; // 리듬 싱크
    [SerializeField] float Range; // 퍼펙트 체크 범위
    [SerializeField] private Player player;
    void Awake()
    {
        //Speed = 60 / GameManager.Instance.Stages[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name].bpm;
        Speed = 128f / 60f;
        RhythmEffect.playbackSpeed = Speed;
        RhythmRenderer = RhythmEffect.GetComponent<ParticleSystemRenderer>();
    }


    void Update()
    {
        EffectTime = RhythmEffect.time;
        Sync = RhythmManager.Instance.RhythmSyncValue;
        if(Input.GetKeyDown(KeyCode.X))
        {
            InputAction("X");
        }
    }
    public void InputAction(string input)
    {
        if (EffectTime < Sync + Speed * Range && EffectTime > Sync - Speed * Range)
        {
            Perfect(input);
        }
        else
        {
            Bad(input);
        }
    }
    void Perfect(string input)
    {
        Debug.Log("Perfect!");
        PerfectEffect.Play();
        switch(input)
        {
            case "Attack":
                player.attackPowerUP = true;
                break;
            case "Dash":
                player.SkillInterface.powerUp = true;
                break;
            case "Slash":
                player.SkillInterface.powerUp = true;
                break;
            case "FireBall":
                player.fireBallPowerUP = true;
                break;
            default:
                break;
        }
    }

    void Bad(string input)
    {
        Debug.Log("Bad!");
        switch (input)
        {
            case "Attack":
                player.attackPowerUP = false;
                break;
            case "Dash":
                player.SkillInterface.powerUp = false;
                break;
            case "Slash":
                player.SkillInterface.powerUp = false;
                break;
            case "FireBall":
                player.fireBallPowerUP = false;
                break;
            default:
                break;
        }
    }  
}
