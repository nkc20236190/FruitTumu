using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public AudioClip soundClip; // ���ʉ��t�@�C��
    private Button button;
    private AudioSource audioSource;

    private void Start()
    {
        // �{�^����AudioSource�R���|�[�l���g���擾
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();

        // �{�^���̃N���b�N�C�x���g�Ɋ֐���ǉ�
        button.onClick.AddListener(PlaySound);
    }

    private void PlaySound()
    {
        // ���ʉ����Đ�
        audioSource.PlayOneShot(soundClip);
    }
}
