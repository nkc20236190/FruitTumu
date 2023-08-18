using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Level : MonoBehaviour
{
    // <summary>全フルーツ</summary>
    private List<Fruit> _AllFruits = new List<Fruit>();
    /// <summary>選択中のフルーツ</summary>
    private List<Fruit> _SelcetFruits = new List<Fruit>();
    /// <summary>選択中のフルーツID</summary>
    private string _SelcetID = "";

    /// <summary>スコア</summary>
    private int _Score = 0;

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

    /// <summary>全部ボムPrefab</summary>
    public GameObject AllBomPrefab;
    
    /// <summary>フルーツを消すために必要な数</summary>
    public int FruitDestroyCount = 3;
    /// <summary>フルーツをつなぐ範囲</summary>
    public float FruitConnectRange = 1.5f;
    /// <summary>ボムを生成するために必要なフルーツの数</summary>
    public int BomSpawnCount = 5;
    /// <summary>ボムで消す範囲</summary>
    public float BomDestroyRange = 1.5f;
    /// <summary>全部ボムを生成するために必要なフルーツの数</summary>
    public int AllBomSpawnCount = 4;
    /// <summary>全部ボムで消す範囲</summary>
    public float AllBomDestroyRange = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        FruitSpawn(50);
        ScoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        LineRendererUpdate();
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
        var StartY = 5;
        var X = 0;
        var Y = 0;
        var MaxX = 6;

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
        _SelcetFruits.Add(fruit);
        fruit.SetSelcet(true);

        _SelcetID = fruit.ID;
    }

    /// <summary>
    /// フルーツEnterイベント
    /// </summary>
    /// <param name="fruit"></param>
    public void FruitEnter(Fruit fruit)
    {
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
        if (_SelcetFruits.Count >= FruitDestroyCount)
        {
            DestroyFruits(_SelcetFruits);
            if(_SelcetFruits.Count >= BomSpawnCount)
                Instantiate(BomPrefab, _SelcetFruits[_SelcetFruits.Count -1].transform.position, Quaternion.identity);
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
    /// フルーツを消す
    /// </summary>
    /// <param name="fruits">消すフルーツ</param>
    private void DestroyFruits(List<Fruit> fruits)
    {
        foreach(var FruitItem in fruits)
        {
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
    }
}
