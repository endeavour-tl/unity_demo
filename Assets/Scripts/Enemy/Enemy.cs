using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //�������ֵ
    private int maxHealth = 2;
    private int curHealth;

    //��ȡ���˶���״̬��
    private Animator animator;

    //ͨ��enemyAnim�ű�����
    private EnemyAnim enemyAnim;

    //����Ҿ������6ʱ������վ������
    public float idleDis = 6f;

    //����Ҿ����ڡ�2��6��֮�䲥�����߶���
    public float walkdis = 2f;

    public bool canAttack = true;
    //false ��ʾδ����
    public bool isDie = false;

    //���õ��˹���ʱ��
    private float timer = 0;

    //���ù������ 1s
    private float attackTime = 1f;


    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<EnemyAnim>();
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Patrol())
        {
            enemyAnim.PlayWalkAnim();
        }
        else if (Attack() && canAttack == true)
        {
            enemyAnim.PlayKillAnim();
        }
        else
        {
            enemyAnim.PlayIdleAnim();
        }

    }

    public void Hurt()
    {
        canAttack = false;
        curHealth--;
        if (curHealth <= 0)
        {
            isDie = true;
            SoundManege.Instance.PlayerMusicName("enemyDie");
            enemyAnim.PlayDieAnim();
        }
    }

    public void Destory()
    {
        GameObject.Destroy(gameObject);
    }

    //
    public bool Patrol()
    {
        GameObject GO = GameObject.FindGameObjectWithTag("Player");
        if (GO != null)
        {
            if (GO.GetComponent<Player>().GetCurState() == false)
            {
                return false;
            }
            Transform player = GO.transform;
            Vector2 r = new Vector2(0, 0);
            if (player.transform.position.x - transform.position.x > 0)
            {
                r.y = 180;
            }
            //�޸ĵ��˽Ƕ�
            transform.rotation = Quaternion.Euler(r);

            if (Mathf.Abs(player.position.x - transform.position.x) > idleDis)
            {
                return false;   //���˲���Ѳ��
            }
            else if(Mathf.Abs(player.position.x - transform.position.x) < walkdis)
            {
                return false;   //���˿�ʼ����
            }
            else if (isDie==false)
            {
                //����û��������������ƶ�
                transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public bool Attack()
    {
        timer += Time.deltaTime;
        if (timer >= attackTime)
        {
            GameObject GO = GameObject.FindGameObjectWithTag("Player");
            if (GO != null)
            {
                if (canAttack == false || GO.GetComponent<Player>().GetCurState()==false)
                {
                    return false;
                }
                else if ((this.transform.position - GO.transform.position).magnitude <= 1.5f)
                {
                    //TODO
                    HurtPlayer();
                    SoundManege.Instance.PlayerMusicName("knife");

                    timer = 0;
                    return true;
                }
            }
        }


        return false;
    }
    public void HurtPlayer()
    {
        GameObject GO = GameObject.FindGameObjectWithTag("Player");
        GO.GetComponent<Player>().Hurt();
    }
}
