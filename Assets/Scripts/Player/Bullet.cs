using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 1;   //�ӵ��˺�

    private float speed = 6f;

    private Rigidbody2D rd;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();  //��ȡ�ӵ��ϵ�Rigidbody2D���
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, 5f);     //ÿ��5���Զ�����
    }

    public void InitDir(Dir dir)
    {
        Vector2 v2;

        switch (dir)
        {
            case Dir.Up:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                v2 = new Vector2(0, speed);
                rd.velocity = v2;       //�����ٶ�
                break;
            case Dir.Forward:
                //��ȡ����λ��
                Transform player = GameObject.FindGameObjectWithTag("Player").transform;
                //����������
                if (player.rotation.y == 0)
                {
                    v2 = new Vector2(speed, 0);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    v2 = new Vector2(-speed, 0);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                rd.velocity = v2;       //�����ٶ�
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Hurt();
            Destroy(gameObject);
        }
        if (collision.tag == "Boss")
        {
            collision.GetComponent<Boss>().Hurt();
            Destroy(gameObject);
        }
    }
}
