using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Define.eGameState _cureGameState = Define.eGameState.Ready;
    public bool GameStateReady => _cureGameState == Define.eGameState.Ready;
    public bool GameStatePlay => _cureGameState == Define.eGameState.Play;
    public bool GameStateEnd => _cureGameState == Define.eGameState.End;

    private UI_GameScene _uiGameScene = null;

    public UI_GameScene uiGameScene { get { CheckNull(); return _uiGameScene; } }

    private Cannon _playerCannon = null;
    public Cannon PlayerCannon
    {
        get => _playerCannon;
    }
    private Cannon _enemyCannon = null;
    public Cannon EnemyCannon
    {
        get => _enemyCannon;
    }

    private Stage _curStage = null;
    public Stage CurStage
    {
        get => _curStage;
    }

    public void Init()
    {
        ES3.DeleteFile();

        GameReady();

        _cureGameState = Define.eGameState.Ready;
    }

    private void CheckNull()
    {
        if (_uiGameScene == null) _uiGameScene = FindObjectOfType<UI_GameScene>() as UI_GameScene;
    }

    public void GameReady()
    {
        CheckNull();
        StageInit();

        if (_playerCannon == null) _playerCannon = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Cannon>();
        if (_enemyCannon == null) _enemyCannon = GameObject.FindGameObjectWithTag("Enemy")?.GetComponent<Cannon>();
    }

    public void GamePlay()
    {
        _cureGameState = Define.eGameState.Play;
    }

    public void GameEnd()
    {

    }

    private void StageInit()
    {
        int stageNum = Managers.Data.StageNum;

        _curStage = Managers.Resource.Instantiate($"Stage{stageNum.ToString("D2")}")?.GetComponent<Stage>();
        _curStage.transform.localPosition = Vector3.zero;
        _curStage.transform.localRotation = Quaternion.identity;
        _curStage.Init();
    }
    public void NextStage()
    {
        if (_curStage != null)
        {
            Destroy(_curStage.gameObject);
            _curStage = null;
        }

        Managers.Data.StageNum += 1;

        StageInit();
    }

    public void Clear()
    {

    }
}
