using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ThrustProjectile : Projectile
{
    private ParticleSystem particle;
    public int ThrustDamage = 3;
    private bool thrust = false;
    private float EffectDestroyTime = 0.5f;
    public void Awake()
    {
        particle = transform.GetChild(1).GetComponent<ParticleSystem>();
    }
    public override Projectile Copy(Projectile value)
    {
        base.Copy(value);
        return this;
    }
    public override void DirectionControl(Transform targetpos)
    {
        base.DirectionControl(targetpos);
    }
    protected override void OnEnable()
    {
        particle.Pause();
        particle.gameObject.SetActive(false);
        thrust = false;
        base.OnEnable();
    }

    public override void ReUse(Transform Pos)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        particle.Pause();
        particle.gameObject.SetActive(false);
        base.ReUse(Pos);
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Target)|| other.gameObject.CompareTag("Obstacle"))
        {
            if (thrust==false)
            {
                particle.gameObject.SetActive(true);
                particle.Play();
                thrust = true;
                transform.GetChild(0).gameObject.SetActive(false);
                if (other.gameObject.CompareTag(Target))
                {
                    other.GetComponent<ICharacterData>().Damaged(base.Damage);
                    
                }
            }
            else
            {
                if (other.gameObject.CompareTag(Target))
                {
                    other.GetComponent<ICharacterData>().Damaged(ThrustDamage);
                }
                StartCoroutine(Destroy(EffectDestroyTime));
            }
        }
    }
    protected IEnumerator Destroy(float Time)
    {
        yield return new WaitForSeconds(Time);
        particle.Pause();
        particle.gameObject.SetActive(false);
        ProjectileController.Instance.UsedProjectilePooling(this);
    }
}
