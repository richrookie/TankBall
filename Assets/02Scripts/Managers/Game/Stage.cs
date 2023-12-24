using UnityEngine;

public class Stage : MonoBehaviour
{
    private int _redGridCount = 0;
    public int RedGridCount => _redGridCount;
    private int _blueGridCount = 0;
    public int BlueGridCount => _blueGridCount;

    public bool RedTeamWin => _blueGridCount <= 0;
    public bool BlueTeamWin => _redGridCount <= 0;

    public void Init()
    {
        _redGridCount = 0;
        _blueGridCount = 0;

        Grid[] grid = this.transform.GetComponentsInChildren<Grid>();
        foreach (Grid g in grid)
        {
            if (g.GetComponent<SpriteRenderer>().sprite.name == "RedBlock")
            {
                _redGridCount += 1;
                g.Init(Define.eColorType.Red);
            }
            else if (g.GetComponent<SpriteRenderer>().sprite.name == "BlueBlock")
            {
                _blueGridCount += 1;
                g.Init(Define.eColorType.Blue);
            }
        }

        Block[] block = this.transform.GetComponentsInChildren<Block>();
        foreach (Block b in block)
        {
            b.Init();
        }

        Cannon[] cannon = this.transform.GetComponentsInChildren<Cannon>();
        foreach (Cannon c in cannon)
        {
            c.Init();
        }
    }

    public void SetGridCount(Define.eColorType colorType)
    {
        switch (colorType)
        {
            case Define.eColorType.Red:
                _redGridCount += 1;
                _blueGridCount -= 1;

                if (RedTeamWin)
                    Managers.Resource.Instantiate("UI_PopupVictory", Managers.Game.uiGameScene.transform);
                break;

            case Define.eColorType.Blue:
                _redGridCount -= 1;
                _blueGridCount += 1;

                if (BlueTeamWin)
                    Managers.Resource.Instantiate("UI_PopupDefeat", Managers.Game.uiGameScene.transform);
                break;
        }
    }
}
