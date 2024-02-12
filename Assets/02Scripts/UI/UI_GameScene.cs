using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UI_GameScene : UI_Scene
{
    public enum Buttons
    {
        Button_GameStart
    }

    public enum GameObjects
    {
        Lobby,
        CubeInfo
    }

    public enum Texts
    {
        Text_ShootCount
    }

    private List<GameObject> _ballList = new List<GameObject>(64);
    private List<GameObject> _textBallList = new List<GameObject>(64);
    private List<Block> _blockList = new List<Block>(4);
    private List<GameObject> _textValueList = new List<GameObject>(64);

    private Camera _cam = null;
    private Canvas _canvas = null;

    private void Awake()
    {
        Bind<UnityEngine.UI.Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<UnityEngine.UI.Text>(typeof(Texts));

        base.Init();

        GameReady();
        ButtonInit();
    }

    private void GameReady()
    {
        if (_cam == null)
            _cam = Camera.main;

        if (_canvas == null)
        {
            _canvas = this.GetComponent<Canvas>();
            _canvas.renderMode = RenderMode.ScreenSpaceCamera;
            _canvas.worldCamera = Camera.main;
            _canvas.planeDistance = 5f;
            _canvas.sortingLayerID = 0;
            _canvas.sortingLayerName = "UI";
        }

        _shootCount = 0;

        SetBlockValue();

        GetObject(GameObjects.Lobby).SetActive(true);
    }

    private void ButtonInit()
    {
        GetButton(Buttons.Button_GameStart).onClick.AddListener(() =>
        {
            GetObject(GameObjects.Lobby).SetActive(false);
            Managers.Game.GamePlay();
        });
    }

    private void SetBlockValue()
    {
        PingpongBlock[] pingpongBlock = FindObjectsOfType<PingpongBlock>();

        foreach (PingpongBlock p in pingpongBlock)
        {
            GameObject textPingPongValue = Managers.Resource.Instantiate("Text_PingPongValue", GetObject(GameObjects.CubeInfo).transform);
            textPingPongValue.transform.position = p.transform.position;
            textPingPongValue.transform.rotation = Quaternion.identity;
            textPingPongValue.transform.localScale = new Vector3(1, 1, 1);

            if (p._isMoving)
            {
                _blockList.Add(p);
                _textValueList.Add(textPingPongValue);
            }

            switch (p._pingpongType)
            {
                case Define.ePingpongType.Plus:
                    textPingPongValue.GetComponent<UnityEngine.UI.Text>().text = $"+{p._value}";
                    break;

                case Define.ePingpongType.Muliply:
                    textPingPongValue.GetComponent<UnityEngine.UI.Text>().text = $"x{p._value}";
                    break;
            }
        }
    }
    private void SetPingPongValueTextPos()
    {
        if (_textValueList.Count > 0)
            for (int i = 0; i < _textValueList.Count; i++)
                _textValueList[i].transform.position = _blockList[i].transform.position;
    }

    private void Update()
    {
        if (Managers.Game.GameStatePlay)
        {
            if (Input.GetMouseButtonDown(0))
            {
                TouchPoint();
                CreateBall();
            }

            SetBallTextPos();
            SetPingPongValueTextPos();
        }
    }

    #region Ball Part
    private async void TouchPoint()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, Define.LayerTouch);

        if (hit.collider != null)
        {
            SpriteRenderer sr = hit.transform?.GetComponent<SpriteRenderer>() ?? null;

            if (sr != null)
            {
                sr.enabled = true;

                await Task.Delay(100);

                sr.enabled = false;
            }
        }
    }

    private void CreateBall()
    {
        Vector2 touchPos = (Vector2)_cam.ScreenToWorldPoint(Input.mousePosition);

        GameObject textBall = Managers.Resource.Instantiate("Text_Ball", this.transform);
        textBall.transform.localScale = Vector3.one;
        _textBallList.Add(textBall);

        GameObject ball = Managers.Resource.Instantiate("Ball");
        ball.GetOrAddComponent<Ball>().Init(textBall.GetComponent<UnityEngine.UI.Text>());
        _ballList.Add(ball);

        if (touchPos.x < -Define.xBoundary)
            ball.transform.position = Define.LeftCornerVec;
        else if (touchPos.x > Define.xBoundary)
            ball.transform.position = Define.RightCornerVec;
        else
            ball.transform.position = new Vector3(touchPos.x, 1.25f, 0);
    }

    private void SetBallTextPos()
    {
        if (_textBallList.Count > 0)
            for (int i = 0; i < _ballList.Count; i++)
                _textBallList[i].transform.position = _ballList[i].transform.position;
    }

    public void BallReset(GameObject ball, GameObject ballCountText)
    {
        if (_ballList.Contains(ball))
        {
            _textBallList.Remove(ballCountText);
            _ballList.Remove(ball);

            SetShootCount(ball.GetComponent<Ball>().Number);

            ball?.GetComponent<Poolable>().Destroy();
            ballCountText?.GetComponent<Poolable>().Destroy();
        }
    }
    #endregion


    #region Shoot Part
    private int _shootCount = 0;
    public int ShootCount
    {
        get => _shootCount;
    }
    public void SetShootCount(int ballCount)
    {
        _shootCount += ballCount;
        GetText(Texts.Text_ShootCount).text = _shootCount.ToString();
    }
    #endregion



    public void Clear()
    {
        GetObject(GameObjects.Lobby).SetActive(true);

        Managers.Clear();
        _ballList.Clear();
        _textBallList.Clear();
        _blockList.Clear();
        _textValueList.Clear();
    }
}
