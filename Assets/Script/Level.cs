using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Level : MonoBehaviour
{
    private List<Fruit> _LastSelectedFruits = new List<Fruit>();

    // <summary>全フルーツ</summary>
    private List<Fruit> _AllFruits = new List<Fruit>();
    /// <summary>選択中のフルーツ</summary>
    private List<Fruit> _SelcetFruits = new List<Fruit>();
    /// <summary>選択中のフルーツID</summary>
    private string _SelcetID = "";

    /// <summary>スコア</summary>
    private int _Score = 0;
    /// <summary>保存スコア</summary>
    private int _HighScore = 0;
    /// <summary>現在時間[s]</summary>
    private float _CurrentTime = 60;
    /// <summary>プレイ中状態</summary>
    private bool _IsPlaying = true;

    /// <summary>シングルトンインスタンス</summary>
    public static Level Instance { get; private set; }

    /// <summary>フルーツPrefabリスト</summary>
    public GameObject[] FruitPrefabs;

    /// <summary>選択線描画オブジェクト</summary>
    public LineRenderer LineRenderer;

    /// <summary>ボムPrefab</summary>
    public GameObject BomPrefab;

    /// <summary>スコア表示テキスト</summary>
    public TextMeshProUGUI ScoreText;

    /// <summary>スコア表示テキスト</summary>
    public TextMeshProUGUI HighScoreText;

    /// <summary>時間表示テキスト</summary>
    public TextMeshProUGUI TimerText;
    /// <summary>終了画面</summary>
    public GameObject FinishDialod;

    /// <summary>全部ボムPrefab</summary>
    public GameObject AllBomPrefab;
    
    /// <summary>フルーツを消すために必要な数</summary>
    public int FruitDestroyCount = 3;
    /// <summary>フルーツをつなぐ範囲</summary>
    public float FruitConnectRange = 1.5f;
    /// <summary>ボムを生成するために必要なフルーツの数</summary>
    public int BomSpawnCount = 4;
    /// <summary>ボムで消す範囲</summary>
    public float BomDestroyRange = 1.5f;
    /// <summary>全部ボムを生成するために必要なフルーツの数</summary>
    public int AllBomSpawnCount = 6;
    /// <summary>全部ボムで消す範囲</summary>
    public float AllBomDestroyRange = 10f;
    /// <summary>全部ボムで消す範囲</summary>
    public float AllFruitDestroyRange = 10f;
    /// <summary>プレイ時間[s]</summary>
    public float PlayTime = 60;

    private bool _GameEnded = false;
    private float _ElapsedTime = 0f;
    private bool _CanPlayClickSound = true; // クリック音再生のフラグを追加

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        FruitSpawn(50);
        ScoreText.text = "0";
        _CurrentTime = PlayTime;
        _ElapsedTime = 0f; // 経過時間を初期化
    }

    // Update is called once per frame
    void Update()
    {
        LineRendererUpdate();
        TimerUpdate();

        _ElapsedTime += Time.deltaTime; // 経過時間を更新

        // 60秒を超えたらクリック音再生のフラグを無効化
        if (_ElapsedTime >= 60f)
        {
            _CanPlayClickSound = false;
        }

        // 選択中のフルーツリストが変化したかどうかを確認
        if (!ListsAreEqual(_LastSelectedFruits, _SelcetFruits))
        {
            // フルーツが選択されたらクリック音を再生 (フラグが true の場合のみ再生)
            if (_CanPlayClickSound)
            {
                foreach (var fruit in _SelcetFruits)
                {
                    fruit.PlayClickSound();
                }
            }
        }

        // 選択中のフルーツリストの前回の状態を更新
        _LastSelectedFruits.Clear();
        _LastSelectedFruits.AddRange(_SelcetFruits);
    }

    // 2つのリストが等しいかどうかを確認するユーティリティメソッド
    private bool ListsAreEqual(List<Fruit> list1, List<Fruit> list2)
    {
        if (list1.Count != list2.Count)
        {
            return false;
        }

        for (int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i])
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// 時間更新
    /// </summary>
    private void TimerUpdate()
    {
        if(_IsPlaying)
        {
            _CurrentTime -= Time.deltaTime;
            if(_CurrentTime <= 0)
            {
                _CurrentTime = 0;
                FruitUp();
                _IsPlaying = false;
                _GameEnded = true; // ゲーム終了フラグを設定
                FinishDialod.SetActive(true);
            }
            TimerText.text = ((int) _CurrentTime).ToString();
        }
    }

    /// <summary>
    /// 選択中のフルーツをつなぐ線の描画を更新
    /// </summary>
    private void LineRendererUpdate()
    {
        if(_SelcetFruits.Count >= 2)
        {
            LineRenderer.positionCount = _SelcetFruits.Count;
            LineRenderer.SetPositions(_SelcetFruits.Select(fruit => fruit.transform.position).ToArray());
            LineRenderer.gameObject.SetActive(true);
        }
        else LineRenderer.gameObject.SetActive(false);
    }

    /// <summary>
    /// フルーツ生成
    /// </summary>
    /// <param name="count">生成数</param>
    private void FruitSpawn(int count)
    {
        var StartX = -2;
        var StartY = 6;
        var X = 0;
        var Y = 0;
        var MaxX = 5;

        for(int i = 0; i < count; i++)
        {
            var Position = new Vector3(StartX + X, StartY + Y, 0);
            var FruitObject = Instantiate(FruitPrefabs[Random.Range(0, FruitPrefabs.Length)],Position,Quaternion.identity);
            _AllFruits.Add(FruitObject.GetComponent<Fruit>());

            X++;
            if(X == MaxX)
            {
                X = 0;
                Y++;
            }
        }
    }

    /// <summary>
    /// フルーツDownイベント
    /// </summary>
    /// <param name="fruit"></param>
    public void FruitDown(Fruit fruit)
    {
        if (!_IsPlaying) return;
        _SelcetFruits.Add(fruit);
        fruit.SetSelcet(true);

        _SelcetID = fruit.ID;

        //// フルーツが選択されたらクリック音を再生
        //fruit.PlayClickSound();
        // フルーツが選択されたらクリック音を再生 (フラグが true の場合のみ再生)
        if (_CanPlayClickSound)
        {
            fruit.PlayClickSound();
        }
    }
    
        /// <summary>
        /// フルーツEnterイベント
        /// </summary>
        public void FruitEnter(Fruit fruit)
    {
        if (!_IsPlaying) return;
        if (_SelcetID != fruit.ID) return;

        if (fruit.IsSelcet)
        {
            if(_SelcetFruits.Count >= 2 && _SelcetFruits[_SelcetFruits.Count -2] == fruit)
            {
                var RemoveFruit = _SelcetFruits[_SelcetFruits.Count - 1];
                RemoveFruit.SetSelcet(false);
                _SelcetFruits.Remove(RemoveFruit);
            }
        }
        else
        {
            var Lenght = (_SelcetFruits[_SelcetFruits.Count -1].transform.position - fruit.transform.position).magnitude;
            if(Lenght < FruitConnectRange)
            {
                _SelcetFruits.Add(fruit);
                fruit.SetSelcet(true);
            }
        }
    }

    /// <summary>
    /// フルーツUpイベント
    /// </summary>
    public void FruitUp()
    {
        if (!_IsPlaying) return;
        if (_SelcetFruits.Count >= FruitDestroyCount)
        {
            DestroyFruits(_SelcetFruits);

            // ボム生成の条件が満たされた場合にボムを生成する
            if (_SelcetFruits.Count >= BomSpawnCount && _SelcetFruits.Count < AllBomSpawnCount)
                Instantiate(BomPrefab, _SelcetFruits[_SelcetFruits.Count - 1].transform.position, Quaternion.identity);

            // ここで全部ボム生成の条件を確認し、満たされた場合に生成する
            if (_SelcetFruits.Count >= AllBomSpawnCount)
                Instantiate(AllBomPrefab, _SelcetFruits[_SelcetFruits.Count - 1].transform.position, Quaternion.identity);
        }
        else
        {
            foreach (var FruitItem in _SelcetFruits)
                FruitItem.SetSelcet(false);
        }

        _SelcetID = "";
        _SelcetFruits.Clear();
    }

    /// <summary>
    /// ボムを押した
    /// </summary>
    /// <param name="bom">ボム</param>
    public void BomDown(Bom bom)
    {
        if (!_IsPlaying) return;
        var RemoveFruits = new List<Fruit>();

        foreach(var FruitItem in _AllFruits)
        {
            var Lenght = (FruitItem.transform.position - bom.transform.position).magnitude;
            if (Lenght < FruitConnectRange)
                RemoveFruits.Add(FruitItem);
        }
        DestroyFruits(RemoveFruits);
        Destroy(bom.gameObject);
    }

    /// <summary>
    /// 全部ボムを押した
    /// </summary>
    /// <param name="allbom">全部ボム</param>
    public void AllBomDown(AllBom allbom)
    {
        if (!_IsPlaying) return;
        var RemoveFruits = new List<Fruit>();

        foreach (var FruitItem in _AllFruits)
        {
            var Lenght = (FruitItem.transform.position - allbom.transform.position).magnitude;
            if (Lenght < AllFruitDestroyRange)
                RemoveFruits.Add(FruitItem);
        }

        DestroyFruits(RemoveFruits);
        Destroy(allbom.gameObject);
    }

    /// <summary>
    /// フルーツを消す
    /// </summary>
    /// <param name="fruits">消すフルーツ</param>
    private void DestroyFruits(List<Fruit> fruits)
    {
        foreach(var FruitItem in fruits)
        {
            FruitItem.PlayDestroySound(); // フルーツが消されたときの音を再生
            Destroy(FruitItem.gameObject);
            _AllFruits.Remove(FruitItem);
        }

        FruitSpawn(fruits.Count);
        AddScore(fruits.Count);
    }

    /// <summary>
    /// スコアを追加
    /// </summary>
    /// <param name="fruitCount">消したフルーツの数</param>
    private void AddScore(int fruitCount)
    {
        _Score += (int)(fruitCount * 100 * (1 + (fruitCount - 3) * 0.1f));
        ScoreText.text = _Score.ToString();

        _HighScore += (int)(fruitCount * 100 * (1 + (fruitCount - 3) * 0.1f));
        HighScoreText.text = _HighScore.ToString();

        // 最高スコアを保存する
        if (_Score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", _Score);
            PlayerPrefs.Save();
        }
    }
}
