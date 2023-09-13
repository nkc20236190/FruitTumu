using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBom : MonoBehaviour
{
    /// <summary>
    /// MouseDownƒCƒxƒ“ƒg
    /// </summary>
    private void OnMouseDown()
    {
        Level.Instance.AllBomDown(this);
    }
}
