using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhythm : MonoBehaviour
{
    [SerializeField] private ParticleSystem RhythmEffect; // ������ ���߱� ���� ����Ʈ
    [SerializeField] private ParticleSystem PerfectEffect; // ����Ʈ �� �߻��ϴ� ����Ʈ
    ParticleSystemRenderer RhythmRenderer; // ����Ʈ ������
    [SerializeField] private Material[] RythmMaterial; // ����Ʈ ���׸���
    public float Speed; // ����Ʈ �ӵ�
    private float EffectTime; // ���� ����Ʈ�� �ð�
    private float Per; // ���� ��ũ
    [SerializeField] float Range; // ����Ʈ üũ ����
    // Start is called before the first frame update
    void Awake()
    {
        RhythmEffect.playbackSpeed = Speed;
        RhythmRenderer = RhythmEffect.GetComponent<ParticleSystemRenderer>();
        Per = RhythmManager.Instance.RhythmSync;
    }

    // Update is called once per frame
    void Update()
    {
        EffectTime = RhythmEffect.time;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(EffectTime);
            if (EffectTime < Per + Speed * Range && EffectTime > Per - Speed * Range)
            {
                Debug.Log("Perfect!");
                PerfectEffect.Play();
                int i = Random.RandomRange(0, RythmMaterial.Length);
                RhythmRenderer.material = RythmMaterial[i];
            }
            else
            {
                Debug.Log("Bad!");
                int i = Random.RandomRange(0, RythmMaterial.Length);
                RhythmRenderer.material = RythmMaterial[i];
            }
        }
    }
}