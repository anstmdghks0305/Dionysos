using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class Projectile : MonoBehaviour
{
    public int SerialNum;
    private Vector3 Direction;
    private float Speed;
    private int Damage;
    public Projectile Copy(Projectile value)
    {
        Speed = value.Speed;
        Damage = value.Damage;
        return this;
    }
    public void DirectionControl(Transform targetpos)
    {
        Direction = targetpos.position - this.transform.position;
        Direction -= Vector3.up * Direction.y-Vector3.right;
        Debug.Log(Direction);
        Debug.Log(Mathf.Cos(Vector3.Dot(Direction.normalized, Vector3.right)));
        float temp;
        if (Direction.x < 0&& Direction.z <0)
            this.transform.rotation = Quaternion.Euler(90, -(Mathf.Cos(Vector3.Dot(Direction.normalized, Vector3.left)) + 0.5f) * 180, 0);
        else if(Direction.x < 0 && Direction.z >0)
            this.transform.rotation = Quaternion.Euler(90, +(Mathf.Cos(Vector3.Dot(Direction.normalized, Vector3.left)) +0.5f) * 180, 0);
        else if (Direction.x > 0&&Direction.z > 0)
        {
            this.transform.rotation = Quaternion.Euler(90, -(Mathf.Cos(Vector3.Dot(Direction.normalized, Vector3.right)) - 0.5f) * 180,0);
        }
        else
        {
            this.transform.rotation = Quaternion.Euler(90, (Mathf.Cos(Vector3.Dot(Direction.normalized, Vector3.right)) - 0.5f) * 180, 0);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(Destory());
    }

    private IEnumerator Destory()
    {
        yield return new WaitForSeconds(10);
        ProjectileController.Instance.UsedProjectilePooling(this);
        this.gameObject.SetActive(false);
    }
    public void ReUse(Transform Pos)
    {
        this.gameObject.SetActive(true);
        this.transform.position = Pos.transform.position + 0.5f * Vector3.up;
    }
    private void Start()
    {
        Copy(ProjectileInputer.FindProjectile(this));
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += Direction.normalized * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ProjectileController.Instance.UsedProjectilePooling(this);
            this.gameObject.SetActive(false);
            other.GetComponent<ICharacterData>().Damaged(Damage);
        }
        else if (other.gameObject.tag == "Obstacle")
        {
            ProjectileController.Instance.UsedProjectilePooling(this);
            this.gameObject.SetActive(false);
        }
    }

    public void Initialize(int serialNum, float speed, int damage)
    {
        SerialNum = serialNum;
        Speed = speed;
        Damage = damage;
    }
}
