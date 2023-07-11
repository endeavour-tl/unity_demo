using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    private Animator anim;

    //�ж��Ƿ����Ӵ���false��δ�����Ӵ�
    private bool isTouched = false;
    // Start is called before the first frame update
    void Start()
    {
        //��ȡ���񵯵�Rigidbody2D
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Init();
    }

    private void Update()
    {
        //��ը�����Լ���ת
        if (isTouched == false)
        {
            rigidbody2D.transform.Rotate(new Vector3(0, 0, 1), Space.Self);
        }
    }

    private void Init()
    {
        //����ը������
        anim.enabled = false;
        //ͨ����ǩ�������
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player.rotation.y == 0)  //��ҳ�����
        {
            rigidbody2D.AddForce(new Vector2(200f, 300f));//���һ����
        }
        else        //��ҳ�����
        {
            rigidbody2D.AddForce(new Vector2(-200f, 300f));
        }
    }

    //�������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Hurt();
        }
        if (collision.tag == "Boss")
        {
            collision.GetComponent<Boss>().Hurt();
        }
        //�Ӵ�������
        isTouched = true;
        //��ը���ٶ�����Ϊ0
        rigidbody2D.velocity = Vector2.zero;
        //����ը�����ŽǶ�
        rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, 0);
        //����������С
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        //������ը����
        anim.enabled = true;
        //��������
        SoundManege.Instance.PlayerMusicName("GrenadeExplosion");
    }

    public void Destory()
    {
        GameObject.Destroy(gameObject);
    }
}
