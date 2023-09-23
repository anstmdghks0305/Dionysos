using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private Player player;
    public ParticleSystem[] Effect;
    [SerializeField] private GameObject Night;

    private void Update()
    {
    }
    public void LightningEffect()
    {
        Effect[0].Play();
    }
    public void NightEffect(bool b)
    {
        Night.SetActive(b);
    }
}
