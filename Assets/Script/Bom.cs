using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom : MonoBehaviour
{
    /// <summary>
    /// MouseDown�C�x���g
    /// </summary>
    private void OnMouseDown()
    {
        Level.Instance.BomDown(this);
    }
}
