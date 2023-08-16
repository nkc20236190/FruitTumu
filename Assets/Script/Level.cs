using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Level : MonoBehaviour
{
    /// <summary>選択中のフルーツ</summary>
    private List<Fruit> _SelcetFruits = new List<Fruit>();
    /// <summary>選択中のフルーツID</summary>
    private string _SelcetID = "";

    /// <summary>シングルトンインスタンス</summary>
    public static Level Instance { get; private set; }

    /// <summary>フルーツPrefabリスト</summary>
    public GameObject[] FruitPrefabs;

    /// <summary>選択線描画オブジェクト</summary>
    public LineRenderer LineRenderer;
    
    /// <summary>フルーツを消すために必要な数</summary>
    public int FruitDestroyCount = 3;
    /// <summary>フルーツをつなぐ範囲</summary>
    public float FruitConnectRange = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        FruitSpawn(50);
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
            Instantiate(FruitPrefabs[Random.Range(0, FruitPrefabs.Length)],Position,Quaternion.identity);

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
    /// フルーツを消す
    /// </summary>
    /// <param name="fruits">消すフルーツ</param>
    private void DestroyFruits(List<Fruit> fruits)
    {
        foreach(var FruitItem in fruits)
        {
            Destroy(FruitItem.gameObject);
        }

        FruitSpawn(fruits.Count);
    }
}
