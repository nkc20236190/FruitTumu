using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    /// <summary>���ʗpID</summary>
    public string ID;

    /// <summary>�I����ԕ\��Sprite</summary>
    public GameObject SelcetSprite;

    /// <summary>�I�����</summary>
    public bool IsSelcet { get; private set; }

    /// <summary>
    /// MouseDown�C�x���g
    /// </summary>
    private void OnMouseDown()
    {
        Level.Instance.FruitDown(this);
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
}
