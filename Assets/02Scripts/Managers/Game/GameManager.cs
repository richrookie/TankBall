using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Define.eGameState _cureGameState = Define.eGameState.Ready;
    public bool GameStateReady => _cureGameState == Define.eGameState.Ready;
    public bool GameStatePlay => _cureGameState == Define.eGameState.Play;
    public bool GameStateEnd => _cureGameState == Define.eGameState.End;

    private UI_GameScene _uiGameScene = null;

    public UI_GameScene uiGameScene { get { CheckNull(); return _uiGameScene; } }

    private Transform _playerCannon = null;
    public Transform PlayerCannon
    {
        get => _playerCannon;
    }

    public void Init()
    {
        CheckNull();

        StageInit();

        _playerCannon = GameObject.FindGameObjectWithTag("Player").transform;

        _cureGameState = Define.eGameState.Play;
    }

    private void CheckNull()
    {

    }

    private void StageInit()
    {
        int stageNum = Managers.Data.StageNum;
        GameObject stage = Managers.Resource.Instantiate($"Stage{stageNum.ToString("D2")}");
        stage.transform.localPosition = new Vector3(0, -3.5f, 0);
        stage.transform.localRotation = Quaternion.identity;
        stage.GetOrAddComponent<Stage>().Init();
    }

    public void Clear()
    {

    }
}
