using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom : MonoBehaviour
{
    public AudioSource audioSource; // �{���̃I�[�f�B�I �\�[�X
    public AudioClip bombSound;     // �{���̉����N���b�v

    /// <summary>
    /// MouseDown�C�x���g
    /// </summary>
    private void OnMouseDown()
    {
        Level.Instance.BomDown(this);
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.clip = bombSound;
            audioSource.Play();
        }
    }
}
