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
    /// <summary>�ۑ��X�R�A</summary>
    private int _HighScore = 0;
    /// <summary>���ݎ���[s]</summary>
    private float _CurrentTime = 60;
    /// <summary>�v���C�����</summary>
    private bool _IsPlaying = true;

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

    /// <summary>�X�R�A�\���e�L�X�g</summary>
    public TextMeshProUGUI HighScoreText;

    /// <summary>���ԕ\���e�L�X�g</summary>
    public TextMeshProUGUI TimerText;
    /// <summary>�I�����</summary>
    public GameObject FinishDialod;

    /// <summary>�S���{��Prefab</summary>
    public GameObject AllBomPrefab;
    
    /// <summary>�t���[�c���������߂ɕK�v�Ȑ�</summary>
    public int FruitDestroyCount = 3;
    /// <summary>�t���[�c���Ȃ��͈�</summary>
    public float FruitConnectRange = 1.5f;
    /// <summary>�{���𐶐����邽�߂ɕK�v�ȃt���[�c�̐�</summary>
    public int BomSpawnCount = 4;
    /// <summary>�{���ŏ����͈�</summary>
    public float BomDestroyRange = 1.5f;
    /// <summary>�S���{���𐶐����邽�߂ɕK�v�ȃt���[�c�̐�</summary>
    public int AllBomSpawnCount = 6;
    /// <summary>�S���{���ŏ����͈�</summary>
    public float AllBomDestroyRange = 10f;
    /// <summary>�S���{���ŏ����͈�</summary>
    public float AllFruitDestroyRange = 10f;
    /// <summary>�v���C����[s]</summary>
    public float PlayTime = 60;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        FruitSpawn(50);
        ScoreText.text = "0";
        _CurrentTime = PlayTime;
    }

    // Update is called once per frame
    void Update()
    {
        LineRendererUpdate();
        TimerUpdate();
    }

    /// <summary>
    /// ���ԍX�V
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
                FinishDialod.SetActive(true);
            }
            TimerText.text = ((int) _CurrentTime).ToString();
        }
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
    /// �t���[�cDown�C�x���g
    /// </summary>
    /// <param name="fruit"></param>
    public void FruitDown(Fruit fruit)
    {
        if (!_IsPlaying) return;
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
    /// �t���[�cUp�C�x���g
    /// </summary>
    public void FruitUp()
    {
        if (!_IsPlaying) return;
        if (_SelcetFruits.Count >= FruitDestroyCount)
        {
            DestroyFruits(_SelcetFruits);

            // �{�������̏������������ꂽ�ꍇ�Ƀ{���𐶐�����
            if (_SelcetFruits.Count >= BomSpawnCount && _SelcetFruits.Count < AllBomSpawnCount)
                Instantiate(BomPrefab, _SelcetFruits[_SelcetFruits.Count - 1].transform.position, Quaternion.identity);

            // �����őS���{�������̏������m�F���A�������ꂽ�ꍇ�ɐ�������
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
    /// �{����������
    /// </summary>
    /// <param name="bom">�{��</param>
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
    /// �S���{����������
    /// </summary>
    /// <param name="allbom">�S���{��</param>
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

        _HighScore += (int)(fruitCount * 100 * (1 + (fruitCount - 3) * 0.1f));
        HighScoreText.text = _HighScore.ToString();

        // �ō��X�R�A��ۑ�����
        if (_Score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", _Score);
            PlayerPrefs.Save();
        }
    }
}
