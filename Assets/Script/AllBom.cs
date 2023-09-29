using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBom : MonoBehaviour
{
    public AudioClip explosionSound; // �������̂��߂̃I�[�f�B�I�N���b�v

    private void OnMouseDown()
    {
        Level.Instance.AllBomDown(this); // �S���{���̌��ʂ����s

        // ���ʉ����Đ�
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && explosionSound != null)
        {
            audioSource.clip = explosionSound;
            audioSource.Play();
        }
    }
}
