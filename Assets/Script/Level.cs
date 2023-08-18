using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Level : MonoBehaviour
{
    // <summary>�S�t���[�c</summary>
    private List<Fruit> _AllFruits = new List<Fruit>();
    /// <summary>�I�𒆂̃t���[�c</summary>
    private List<Fruit> _SelcetFruits = new List<Fruit>();
    /// <summary>�I�𒆂̃t���[�cID</summary>
    private string _SelcetID = "";

    /// <summary>�X�R�A</summary>
    private int _Score = 0;

    /// <summary>�V���O���g���C���X�^���X</summary>
    public static Level Instance { get; private set; }

    /// <summary>�t���[�cPrefab���X�g</summary>
    public GameObject[] FruitPrefabs;

    /// <summary>�I����`��I�u�W�F�N�g</summary>
    public LineRenderer LineRenderer;

    /// <summary>�{��Prefab</summary>
    public GameObject BomPrefab;

    /// <summary>�X�R�A�\���e�L�X�g</summary>
    public TextMeshProUGUI ScoreText;

    /// <summary>�S���{��Prefab</summary>
    public GameObject AllBomPrefab;
    
    /// <summary>�t���[�c���������߂ɕK�v�Ȑ�</summary>
    public int FruitDestroyCount = 3;
    /// <summary>�t���[�c���Ȃ��͈�</summary>
    public float FruitConnectRange = 1.5f;
    /// <summary>�{���𐶐����邽�߂ɕK�v�ȃt���[�c�̐�</summary>
    public int BomSpawnCount = 5;
    /// <summary>�{���ŏ����͈�</summary>
    public float BomDestroyRange = 1.5f;
    /// <summary>�S���{���𐶐����邽�߂ɕK�v�ȃt���[�c�̐�</summary>
    public int AllBomSpawnCount = 4;
    /// <summary>�S���{���ŏ����͈�</summary>
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
    /// �I�𒆂̃t���[�c���Ȃ����̕`����X�V
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
    /// �t���[�c����
    /// </summary>
    /// <param name="count">������</param>
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
    /// �t���[�cDown�C�x���g
    /// </summary>
    /// <param name="fruit"></param>
    public void FruitDown(Fruit fruit)
    {
        _SelcetFruits.Add(fruit);
        fruit.SetSelcet(true);

        _SelcetID = fruit.ID;
    }

    /// <summary>
    /// �t���[�cEnter�C�x���g
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
    /// �t���[�cUp�C�x���g
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
    /// �{����������
    /// </summary>
    /// <param name="bom">�{��</param>
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
    /// �t���[�c������
    /// </summary>
    /// <param name="fruits">�����t���[�c</param>
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
    /// �X�R�A��ǉ�
    /// </summary>
    /// <param name="fruitCount">�������t���[�c�̐�</param>
    private void AddScore(int fruitCount)
    {
        _Score += (int)(fruitCount * 100 * (1 + (fruitCount - 3) * 0.1f));
        ScoreText.text = _Score.ToString();
    }
}
