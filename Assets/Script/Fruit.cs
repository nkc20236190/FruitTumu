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

    private float timer = 0f;
    private bool canPlayClickSound = true;
    private bool _GameEnded = false;

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
        if (!_GameEnded) // �Q�[�����I�����Ă��Ȃ��ꍇ�ɃN���b�N�����Đ�
        {
            //Level.Instance.FruitDown(this);
            //PlayClickSound();
            // �N���b�N�����Đ��ł��������ǉ�
            if (canPlayClickSound)
            {
                Level.Instance.FruitDown(this);
                PlayClickSound();
            }
        }
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
        //// �I�[�f�B�I���Đ�
        //if (clickSound != null)
        //{
        //    audioSourceClick.PlayOneShot(clickSound);
        //}

        // �I�[�f�B�I���Đ�����O�� canPlayClickSound �t���O���m�F
        if (canPlayClickSound && clickSound != null)
        {
            audioSourceClick.PlayOneShot(clickSound);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 59f)
        {
            canPlayClickSound = false;
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
