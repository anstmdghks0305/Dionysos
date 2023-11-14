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
    public override void Copy(Projectile projectile)
    {
        ThrustProjectile temp =  projectile as ThrustProjectile;
        base.Copy(temp);
        this.ThrustDamage = temp.ThrustDamage;
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
        this.Copy(ProjectileInputer.FindProjectile(this));
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
                    other.GetComponent<Player>().Damaged(base.Damage);
                    
                }
            }
            else
            {
                if (other.gameObject.CompareTag(Target))
                {
                    other.GetComponent<Player>().Damaged(ThrustDamage);
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

    public void Initialize(int serialNum, float speed, int damage, int Sencond_damage)
    {
        base.Initialize(serialNum, speed, damage);
        ThrustDamage = Sencond_damage;
    }
}
