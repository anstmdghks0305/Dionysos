using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ExplosionProjectile : Projectile
{
    private ParticleSystem particle;
    public int ExplosionDamage = 3;
    private bool Explosioned = false;
    private float EffectDestroyTime = 1f;
    // Start is called before the first frame update
    public override void Copy(Projectile value)
    {
        ExplosionProjectile temp = value as ExplosionProjectile;
        base.Copy(temp);
        ExplosionDamage = temp.ExplosionDamage;
    }

    public override void DirectionControl(Transform targetpos)
    {
        base.DirectionControl(targetpos);
    }
    void Awake()
    {
        particle = transform.GetChild(1).GetComponent<ParticleSystem>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();
        Copy(ProjectileInputer.FindProjectile(this));
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Explosioned == false)
            base.Update();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Target) || other.gameObject.CompareTag("Obstacle"))
        {
            if (Explosioned == false)
            {
                this.gameObject.transform.rotation = Quaternion.identity;
                particle.gameObject.SetActive(true);
                particle.Play();
                Explosioned = true;
                transform.GetChild(0).gameObject.SetActive(false);
                if (other.gameObject.CompareTag(Target))
                {
                    other.GetComponent<ICharacterData>().Damaged(base.Damage + ExplosionDamage);
                }
                Collider[] cols = Physics.OverlapBox(this.gameObject.transform.position, new Vector3(1, 1, 1));
                foreach (Collider col in cols)
                {
                    if (col.gameObject.CompareTag(Target))
                    {
                        col.GetComponent<ICharacterData>().Damaged(ExplosionDamage);

                    }

                }
                StartCoroutine(Destroy(EffectDestroyTime));
            }
        }
    }

    public override void ReUse(Transform Pos)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        particle.Stop();
        particle.gameObject.SetActive(false);
        Explosioned = false;
        base.ReUse(Pos);
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
        ExplosionDamage = Sencond_damage;
    }
}





