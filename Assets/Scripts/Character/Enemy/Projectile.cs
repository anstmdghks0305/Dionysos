using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class Projectile : MonoBehaviour
{
    public string Target ="Player";
    public int SerialNum;
    private Vector3 Direction;
    protected float Speed;
    protected int Damage;
    private int DefaultDestroyTime = 5;
    public virtual void Copy(Projectile value)
    {
        Speed = value.Speed;
        Damage = value.Damage;
    }
    public virtual void DirectionControl(Transform targetpos)
    {
        Direction = targetpos.position - this.transform.position;
        Direction -= Vector3.up * Direction.y-Vector3.right;
        Debug.Log(Mathf.Cos(Vector3.Dot(Direction.normalized, Vector3.right)));
        if (Direction.x < 0&& Direction.z <0)
            this.transform.rotation = Quaternion.Euler(90, -(Mathf.Cos(Vector3.Dot(Direction.normalized, Vector3.left)) + 0.5f) * 180, 0);
        else if(Direction.x < 0 && Direction.z >0)
            this.transform.rotation = Quaternion.Euler(90, +(Mathf.Cos(Vector3.Dot(Direction.normalized, Vector3.left)) +0.5f) * 180, 0);
        else if (Direction.x > 0&&Direction.z > 0)
            this.transform.rotation = Quaternion.Euler(90, -(Mathf.Cos(Vector3.Dot(Direction.normalized, Vector3.right)) - 0.5f) * 180,0);
        else
            this.transform.rotation = Quaternion.Euler(90, (Mathf.Cos(Vector3.Dot(Direction.normalized, Vector3.right)) - 0.5f) * 180, 0);
    }
    protected virtual void OnEnable()
    {
        StartCoroutine(Destory(DefaultDestroyTime));
    }

    protected IEnumerator Destory(int delay)
    {
        yield return new WaitForSeconds(delay);
        ProjectileController.Instance.UsedProjectilePooling(this);
    }
    public virtual void ReUse(Transform Pos)
    {
        this.gameObject.SetActive(true);
        this.transform.position = Pos.transform.position + 0.5f * Vector3.up;
    }
    protected virtual void Start()
    {
        //Copy(ProjectileInputer.FindProjectile(this));
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        this.gameObject.transform.position += Direction.normalized * Speed * Time.deltaTime;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Target)
        {
            ProjectileController.Instance.UsedProjectilePooling(this);
            other.GetComponent<ICharacterData>().Damaged(Damage);
        }
        else if (other.gameObject.tag == "Obstacle")
        {
            ProjectileController.Instance.UsedProjectilePooling(this);
        }
    }

    public void Initialize(int serialNum, float speed, int damage)
    {
        SerialNum = serialNum;
        Speed = speed;
        Damage = damage;
    }
}
