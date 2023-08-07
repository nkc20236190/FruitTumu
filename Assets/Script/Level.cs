using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    /// <summary>�t���[�cPrefab���X�g</summary>
    public GameObject[] FruitPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        FruitSpawn(50);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Instantiate(FruitPrefabs[Random.Range(0, FruitPrefabs.Length)],Position,Quaternion.identity);

            X++;
            if(X == MaxX)
            {
                X = 0;
                Y++;
            }
        }
    }
}
