using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManege : MonoBehaviour
{
    private static SoundManege instance;

    public static SoundManege Instance;

    private AudioSource audioSource;    //���ڲ�������


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        //���Ὺʼʱ�Զ�����
        audioSource.playOnAwake = false;
    }

    private void PlayerAudio(AudioClip ac)
    {
        AudioSource.PlayClipAtPoint(ac, Camera.main.transform.position);            //�����������λ�ò���

    }

    public void PlayerMusicName(string name)
    {
        //����ʲô����
        string path = "Sounds/" + name;
        AudioClip clip = Resources.Load<AudioClip>(path);
        PlayerAudio(clip);
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
