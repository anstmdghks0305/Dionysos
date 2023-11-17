using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Vector3 dir;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * Time.deltaTime * 10;
        //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            other.GetComponent<ICharacterData>().Damaged(damage);
        }
        else if (other.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject.gameObject,0.3f);
        }
    }
}
