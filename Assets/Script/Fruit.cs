using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    /// <summary>識別用ID</summary>
    public string ID;

    //private List<Fruit> _SelectedFruits = new List<Fruit>();

    /// <summary>選択状態表示Sprite</summary>
    public GameObject SelcetSprite;

    /// <summary>選択状態</summary>
    public bool IsSelcet { get; private set; }

    //private bool hasPlayedSound = false;

    // クリック時の音声
    public AudioClip clickSound;

    // 消されたときの音声
    public AudioClip destroySound;

    private AudioSource audioSourceClick;

    private AudioSource audioSourceDestroy;

    private float timer = 0f;
    private bool canPlayClickSound = true;
    private bool _GameEnded = false;

    private void Start()
    {
        // クリック時のオーディオソースを作成し設定
        audioSourceClick = gameObject.AddComponent<AudioSource>();
        audioSourceClick.clip = clickSound;

        // 消されたときのオーディオソースを作成し設定
        audioSourceDestroy = gameObject.AddComponent<AudioSource>();
        audioSourceDestroy.clip = destroySound;
    }


    /// <summary>
    /// MouseDownイベント
    /// </summary>
    private void OnMouseDown()
    {
        if (!_GameEnded) // ゲームが終了していない場合にクリック音を再生
        {
            //Level.Instance.FruitDown(this);
            //PlayClickSound();
            // クリック音が再生できる条件を追加
            if (canPlayClickSound)
            {
                Level.Instance.FruitDown(this);
                PlayClickSound();
            }
        }
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

    // Fruit.cs に新しいメソッドを追加
    public void PlayClickSound()
    {
        //// オーディオを再生
        //if (clickSound != null)
        //{
        //    audioSourceClick.PlayOneShot(clickSound);
        //}

        // オーディオを再生する前に canPlayClickSound フラグを確認
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
            AudioSource.PlayClipAtPoint(destroySound, transform.position); // フルーツが消える位置で音を再生
        }
    }
}
