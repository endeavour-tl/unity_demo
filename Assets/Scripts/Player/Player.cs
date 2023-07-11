using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum Dir
{
    Up,
    Forward,
}

public class Player : MonoBehaviour
{
    private float speed = 300f;
    //����
    private Rigidbody2D rd;
    public PlayerAnim playerAnim;

    //Ĭ���ڵ�����
    private bool isOnGround = true;

    //������Ծ����
    private int jumpNum = 2;

    public Transform[] point; //0 ��ǰ����    1���Ϸ���

    public Transform curPoint; //��ǰ�ӵ����ɵ�

    //������Ѫ��
    public int maxHealth = 3;
    public int curHealth;
    
    //falseδ����
    public bool isDead = false;

    //false ��ʾ���λ��������״̬
    public bool isResume = false;
    
    //���ù��������false��ʾû�м��
    public bool wait = false;

    private GameUI gameUI;

    //��������ļ�ʱ��
    private float timer = 0;

    //�����ս������
    private PlayerAttack playerAttack;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnim>();
        playerAttack = GetComponent<PlayerAttack>();
        curHealth = maxHealth;
        gameUI = GameObject.Find("Canvas").GetComponent<GameUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isResume == true || isDead == true)
        {
            return;
        }
        PlayerMove();
        PlayerJump();

        if (Input.GetKeyDown(KeyCode.L) && wait == false)
        {
            wait = true;
            Fire(Dir.Forward);
            playerAnim.PlayShootAnim();
        }
        if (Input.GetKeyDown(KeyCode.W) && wait == false)
        {
            wait = true;
            Fire(Dir.Up);
            playerAnim.PlayShootupAnim();
        }
        if (Input.GetKeyDown(KeyCode.I) && wait == false)
        {
            wait = true;
            Throw();
        }
        if (Input.GetKeyDown(KeyCode.J) && wait == false)
        {
            playerAttack.HurtEnemys();
            playerAnim.PlayAttackAnim();
            SoundManege.Instance.PlayerMusicName("tieguo");
        }
        if (wait)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                wait = false;
                timer = 0;
            }
        }

    }
    public void PlayerMove()
    {
        if (isOnGround==false)
        {
            return;  //���ﲻ�ٵ��ϣ��򲻿��Խ��������ƶ�
        }

        float h = Input.GetAxis("Horizontal");  //��ȡA D ������
        if (h > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rd.velocity = new Vector2(h * speed * Time.fixedDeltaTime, rd.velocity.y);
            playerAnim.PlayWalkAnim();
        }
        else if(h<0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rd.velocity = new Vector2(h * speed * Time.fixedDeltaTime, rd.velocity.y);
            playerAnim.PlayWalkAnim();
        }
        else
        {
            playerAnim.PlayIdleAnim();
        }
        
    }

    public void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.K) && jumpNum > 0)
        {
            isOnGround = false;  //���ٵ���
            jumpNum--;
            rd.AddForce(Vector2.up*250F); //������������ϵ���
            playerAnim.PlayJumpAnim();
        }
    }

    //��ȡϵͳ��ײ������
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground") //�Ӵ���ײ�壬״̬����
        {
            isOnGround = true;
            jumpNum = 2;
        }
    }

    public void Fire(Dir dir)
    {
        //������Ӧ������
        SoundManege.Instance.PlayerMusicName("shoot");


        GameObject temp = Resources.Load<GameObject>("Prefabs/Bullet");  //����Resources��Prefabs/Bullet���ӵ�Ԥ����

        switch (dir)
        {
            case Dir.Forward:
                curPoint = point[0];    //0 ��ǰ����
                break;
            case Dir.Up:
                curPoint = point[1];    //1���Ϸ���
                break;
        }
        //������Ӧ������
        GameObject GO = Instantiate(temp, curPoint.transform.position, Quaternion.identity);

        //��ʼ���ӵ�����
        GO.GetComponent<Bullet>().InitDir(dir);
    }

    public void Throw()
    {
        GameObject temp = Resources.Load<GameObject>("Prefabs/Grenade");
        GameObject GO = Instantiate(temp, point[2].transform.position, Quaternion.identity);
    }
    public void Hurt()
    {
        curHealth--;
        if (gameUI != null)
        {
            gameUI.UpdateHealth(curHealth);
        }
        if (curHealth <= 0)
        {
            isDead = true;
            playerAnim.PlayDieAnim();
            SoundManege.Instance.PlayerMusicName("soliderDie");
            //
            SceneManager.LoadScene(1);
        }
        else
        {
            isResume = true;
            StartCoroutine(Resume());           //����Э��
        }
    }

    public IEnumerator Resume()
    {
        yield return new WaitForSeconds(1);  //�ȴ�һ����
        playerAnim.PlayResumeAnim();

        transform.position = new Vector3(transform.position.x - 3f, transform.position.y + 5f);

        playerAnim.PlayIdleAnim();

        StartCoroutine(ResetState());


    }

    public IEnumerator ResetState()
    {
        yield return new WaitForSeconds(1f);

        isResume = false;
    }

    public bool GetCurState()
    {
        if (isDead == true)
        {
            return false;
        }
        else if (isResume == true)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
