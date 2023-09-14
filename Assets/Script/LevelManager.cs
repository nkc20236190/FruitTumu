using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /// <summary>�t���[�cPrefab���X�g </summary>
    public GameObject[] FruitPrefabs;

    void Start()
    {
        FruitSpawn(40);
    }

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
        var MaxX = 5;

        for (int i = 0; i < count; i++)
        {
            var Position = new Vector3(StartX + X, StartY + Y, 0);
            Instantiate(FruitPrefabs[Random.Range(0, FruitPrefabs.Length)], Position, Quaternion.identity);

            X++;
            if (X == MaxX)
            {
                X = 0;
                Y++;
            }
        }
    }
}
