using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public void Init()
    {
        Block[] block = this.transform.GetComponentsInChildren<Block>();
        foreach (Block b in block)
        {
            b.Init();
        }
    }
}
