using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //��ȡ���λ��
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector3(5.51f, -0.39f, -10);
    }

    // Update is called once per frame
    void Update()
    {
        //target==null��֤���������
        if (target != null)
        {
            float offset_x = target.position.x - transform.position.x;
            if (offset_x > 0)
            {
                if (offset_x > 4f && transform.position.x <= 27.57f && transform.position.x >= 5.51f)
                {
                    Debug.Log("1");
                    //������ƶ���Lerp���ؽ��:Interpolated value, equals to a + (b - a) * t.
                    transform.position = Vector3.Lerp(transform.position,
                        new Vector3(transform.position.x + 10, -0.39f, transform.position.z),
                        Time.deltaTime);
                }
                else if (offset_x > 4f && transform.position.x >= 27.57f && transform.position.x < 39f)
                {
                    Debug.Log("2");
                    //������ƶ���Lerp���ؽ��:Interpolated value, equals to a + (b - a) * t.
                    transform.position = Vector3.Lerp(transform.position,
                        new Vector3(transform.position.x+10, 2.35f, transform.position.z),
                        Time.deltaTime);
                }
                else if (target.transform.position.x >= 35f)
                {
                    Debug.Log("3");
                    transform.position = Vector3.Lerp(transform.position,
                        new Vector3(38.86f, 3.2f, transform.position.z),
                        Time.deltaTime);
                }
            }
            else
            {
                if(offset_x < -4f && transform.position.x >= 27.57f && transform.position.y> -0.39f)
                {
                    //������ƶ���Lerp���ؽ��:Interpolated value, equals to a + (b - a) * t.
                    transform.position = Vector3.Lerp(transform.position,
                        new Vector3(transform.position.x-1, -0.61f, transform.position.z),
                        Time.deltaTime);
                }
                if (offset_x < -4f && transform.position.x <= 27.57f && transform.position.x >= 6f)
                {
                    Debug.Log("11");
                    //������ƶ���Lerp���ؽ��:Interpolated value, equals to a + (b - a) * t.
                    transform.position = Vector3.Lerp(transform.position,
                        new Vector3(0, -0.39f, transform.position.z),
                        Time.deltaTime);
                }
                else if (offset_x < -4f && transform.position.x > 27.57f && transform.position.x < 39f)
                {
                    Debug.Log("22");
                    //������ƶ���Lerp���ؽ��:Interpolated value, equals to a + (b - a) * t.
                    transform.position = Vector3.Lerp(transform.position,
                        new Vector3(transform.position.x - 10, transform.position.y, transform.position.z),
                        Time.deltaTime);
                }
            }
        }
    }
}
