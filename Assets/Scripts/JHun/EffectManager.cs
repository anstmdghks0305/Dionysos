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
        Lightning();
    }
    public void Lightning()
    {
        if (player.slashTime > 0.235f)
        {
            Effect[0].Play();
        }

        if (player.slash)
        {
            Night.SetActive(true);
        }
        else
        {
            Night.SetActive(false);
        }
    }
}
