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
        if(Input.GetKeyDown(KeyCode.X))
        {
            AttackEffect("Perfect");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            AttackEffect("Bad");
        }
    }
    public void LightningEffect()
    {
        Effect[2].Play();
    }
    public void NightEffect(bool b)
    {
        Night.SetActive(b);
    }
    public void AttackEffect(string str)
    {
        switch (str)
        {
            case "Bad":
                Effect[0].Play();
                break;
            case "Perfect":
                Effect[1].Play();
                break;
            default:
                break;
        }
    }
}
