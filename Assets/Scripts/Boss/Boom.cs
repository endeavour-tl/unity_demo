using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        SoundManege.Instance.PlayerMusicName("bombFall");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")  //�������ײ���
        {
            if (collision.GetComponent<Player>().GetCurState() == false)
            {
                return;
            }
            collision.GetComponent<Player>().Hurt();
            //��ײ�����棬������Ϊ0
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            //�����ٶ�
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            animator.SetBool("Boom", true);
        }
        if (collision.tag == "Ground")
        {
            //��ײ�����棬������Ϊ0
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            //�����ٶ�
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            animator.SetBool("Boom", true);
        }
    }

    public void BoomDestory()
    {
        GameObject.Destroy(gameObject);
    }
}
