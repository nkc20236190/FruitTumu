using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    /// <summary>識別用ID</summary>
    public string ID;

    /// <summary>選択状態表示Sprite</summary>
    public GameObject SelcetSprite;

    /// <summary>選択状態</summary>
    public bool IsSelcet { get; private set; }

    /// <summary>
    /// MouseDownイベント
    /// </summary>
    private void OnMouseDown()
    {
        Level.Instance.FruitDown(this);
    }

    /// <summary>
    /// MouseEnterイベント
    /// </summary>
    private void OnMouseEnter()
    {
        Level.Instance.FruitEnter(this);
    }

    /// <summary>
    /// MouseUpイベント
    /// </summary>
    private void OnMouseUp()
    {
        Level.Instance.FruitUp();
    }

    /// <summary>
    /// 選択状態を設定
    /// </summary>
    /// <param name="isSelcet">選択状態</param>
    public void SetSelcet(bool isSelcet)
    {
        IsSelcet = isSelcet;
        SelcetSprite.SetActive(isSelcet);
    }
}
