using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Vector3 dir;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4); // ȭ������ 4�� �Ŀ� �ı��ϵ���
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * Time.deltaTime * 10; //// ������ �������� ȭ���� �̵�
    }
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ��ü�� ������ Ȯ��
        if (other.CompareTag("enemy"))
        {
            // ������ ���ظ� ����
            other.GetComponent<ICharacterData>().Damaged(damage);
        }
        // �浹�� ��ü�� ��ֹ����� Ȯ��
        else if (other.CompareTag("Obstacle"))
        {
            // ��ֹ��� �ε����� �� ȭ������ �����ð� �ڿ� �ı�
            Destroy(this.gameObject.gameObject,0.3f);
        }
    }
}
