using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Define.eGameState _cureGameState = Define.eGameState.Ready;
    public bool GameStateReady => _cureGameState == Define.eGameState.Ready;
    public bool GameStatePlay => _cureGameState == Define.eGameState.Play;
    public bool GameStateEnd => _cureGameState == Define.eGameState.End;

    private UI_GameScene _uiGameScene = null;

    public UI_GameScene uiGameScene { get { CheckNull(); return _uiGameScene; } }


    public void Init()
    {
        CheckNull();

        _cureGameState = Define.eGameState.Play;
    }

    private void CheckNull()
    {

    }

    public void Clear()
    {

    }
}
