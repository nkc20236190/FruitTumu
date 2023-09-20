using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get { return instance; }
    }

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �V�[���J�ڎ��ɃI�u�W�F�N�g��j�����Ȃ��悤�ɂ���
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // ���̃C���X�^���X�����݂���ꍇ�A�����j������
        }
    }

    public void PlaySound(AudioClip soundClip)
    {
        audioSource.PlayOneShot(soundClip);
    }
}
