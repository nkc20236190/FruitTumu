using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundScript : MonoBehaviour
{
    public static MainSoundScript instance; // �C���X�^���X���V���O���g���Ƃ��ĕێ�

    public bool DontDestroyEnabled = true;

    // BGM�Đ��p��AudioSource
    private AudioSource bgmAudioSource;

    void Awake()
    {
        // �V���O���g���̊m��
        if (instance == null)
        {
            instance = this;
            if (DontDestroyEnabled)
            {
                // Scene��J�ڂ��Ă��I�u�W�F�N�g�������Ȃ��悤�ɂ���
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            // �����̃C���X�^���X������ꍇ�́A�V�����C���X�^���X��j��
            Destroy(gameObject);
        }

        // AudioSource�̎擾
        bgmAudioSource = GetComponent<AudioSource>();
    }

    // BGM�Đ�
    public void PlayBGM(AudioClip bgmClip)
    {
        bgmAudioSource.clip = bgmClip;
        bgmAudioSource.Play();
    }

    // BGM��~
    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    // BGM�ĊJ
    public void ResumeBGM()
    {
        bgmAudioSource.Play();
        bgmAudioSource.UnPause();
    }

    // BGM�ꎞ��~
    public void PauseBGM()
    {
        bgmAudioSource.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        // �����ǉ��̏������K�v�ȏꍇ�͂����ɒǉ�
    }
}
