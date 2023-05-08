using UnityEngine;

public abstract class Block : MonoBehaviour
{
    public Define.eBlockType _blockType = Define.eBlockType.None;

    public abstract void Init();
}
