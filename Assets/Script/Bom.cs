using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom : MonoBehaviour
{
    public AudioSource audioSource; // �{���̃I�[�f�B�I �\�[�X
    public AudioClip bombSound;     // �{���̉����N���b�v
    public SpriteRenderer spriteRenderer; // Bom�I�u�W�F�N�g��SpriteRenderer


    private bool hasBeenClicked = false;

    /// <summary>
    /// MouseDown�C�x���g
    /// </summary>
    private void OnMouseDown()
    {
        if (!hasBeenClicked)
        {
            hasBeenClicked = true;
            Level.Instance.BomDown(this);

            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.clip = bombSound;
                audioSource.Play();
                // �I�[�f�B�I���Đ�������\����ON�ɂ���
                audioSource.enabled = true;

                // �{����SpriteRenderer���\���ɂ���
                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = false;
                }

                // ���ʉ����Đ��������莞�Ԍ�ɂ��̃I�u�W�F�N�g��j������R���[�`�����J�n
                StartCoroutine(StopAudioAfterDelay());
            }
        }
    }

    // 2�b��ɃI�[�f�B�I���~���ASpriteRenderer���ĕ\������R���[�`��
    private IEnumerator StopAudioAfterDelay()
    {
        yield return new WaitForSeconds(2.0f); // 2�b�҂�

        // �I�[�f�B�I���~
        audioSource.Stop();

        // �\����OFF�ɂ���
        audioSource.enabled = false;

        // �{����SpriteRenderer���ĕ\������
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }
    }
}
