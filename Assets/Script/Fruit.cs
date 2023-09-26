using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    /// <summary>���ʗpID</summary>
    public string ID;

    //private List<Fruit> _SelectedFruits = new List<Fruit>();

    /// <summary>�I����ԕ\��Sprite</summary>
    public GameObject SelcetSprite;

    /// <summary>�I�����</summary>
    public bool IsSelcet { get; private set; }

    //private bool hasPlayedSound = false;

    // �N���b�N���̉���
    public AudioClip clickSound;

    // �����ꂽ�Ƃ��̉���
    public AudioClip destroySound;

    private AudioSource audioSourceClick;

    private AudioSource audioSourceDestroy;

    private void Start()
    {
        // �N���b�N���̃I�[�f�B�I�\�[�X���쐬���ݒ�
        audioSourceClick = gameObject.AddComponent<AudioSource>();
        audioSourceClick.clip = clickSound;

        // �����ꂽ�Ƃ��̃I�[�f�B�I�\�[�X���쐬���ݒ�
        audioSourceDestroy = gameObject.AddComponent<AudioSource>();
        audioSourceDestroy.clip = destroySound;
    }


    /// <summary>
    /// MouseDown�C�x���g
    /// </summary>
    private void OnMouseDown()
    {
        Level.Instance.FruitDown(this);
        PlayClickSound();
        //// �I�[�f�B�I���Đ�
        //if (clickSound != null)
        //{
        //    audioSourceClick.Play();
        //}
    }

    /// <summary>
    /// MouseEnter�C�x���g
    /// </summary>
    private void OnMouseEnter()
    {
        Level.Instance.FruitEnter(this);
    }

    /// <summary>
    /// MouseUp�C�x���g
    /// </summary>
    private void OnMouseUp()
    {
        Level.Instance.FruitUp();
    }

    /// <summary>
    /// �I����Ԃ�ݒ�
    /// </summary>
    /// <param name="isSelcet">�I�����</param>
    public void SetSelcet(bool isSelcet)
    {
        IsSelcet = isSelcet;
        SelcetSprite.SetActive(isSelcet);
    }

    // Fruit.cs �ɐV�������\�b�h��ǉ�
    public void PlayClickSound()
    {
        // �I�[�f�B�I���Đ�
        if (clickSound != null)
        {
            audioSourceClick.PlayOneShot(clickSound);
        }
    }

    public void PlayDestroySound()
    {
        if (destroySound != null)
        {
            AudioSource.PlayClipAtPoint(destroySound, transform.position); // �t���[�c��������ʒu�ŉ����Đ�
        }
    }
}
