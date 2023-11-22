using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Vector3 dir;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4); // 화염구를 4초 후에 파괴하도록
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * Time.deltaTime * 10; //// 지정된 방향으로 화염구 이동
    }
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 개체가 적인지 확인
        if (other.CompareTag("enemy"))
        {
            // 적에게 피해를 입힘
            other.GetComponent<ICharacterData>().Damaged(damage);
        }
        // 충돌한 개체가 장애물인지 확인
        else if (other.CompareTag("Obstacle"))
        {
            // 장애물에 부딪혔을 때 화염구를 일정시간 뒤에 파괴
            Destroy(this.gameObject.gameObject,0.3f);
        }
    }
}
