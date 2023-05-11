using UnityEngine;

public class Grid : MonoBehaviour
{
    private Define.eColorType _colorType;
    private SpriteRenderer _spriteRdr = null;

    public void Init(Define.eColorType colorType)
    {
        _colorType = colorType;
        _spriteRdr = this.GetComponent<SpriteRenderer>();

        SetGridSprite();
    }

    private void SetGridSprite()
    {
        switch (_colorType)
        {
            case Define.eColorType.Red:
                _spriteRdr.sprite = Managers.Resource.Load<Sprite>("RedBlock");
                break;

            case Define.eColorType.Blue:
                _spriteRdr.sprite = Managers.Resource.Load<Sprite>("BlueBlock");
                break;
        }
    }

    public bool CheckOccupation(Define.eColorType colorType)
    {
        if (_colorType == colorType)
            return false;
        else
        {
            _colorType = colorType;
            SetGridSprite();
            return true;
        }
    }
}
