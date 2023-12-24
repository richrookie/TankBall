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

    public enum TextMeshs
    {
        TMP_ShootCount
    }

    private List<GameObject> _ballList = new List<GameObject>(64);
    private List<GameObject> _TMPBallList = new List<GameObject>(64);
    private List<Block> _blockList = new List<Block>(4);
    private List<GameObject> _TMPValueList = new List<GameObject>(64);

    private readonly float _xBoundary = 5f;
    private readonly Vector3 _leftCornerVec = new Vector3(-5f, 1.25f, 0);
    private readonly Vector3 _rightCornerVec = new Vector3(5f, 1.25f, 0);
    private LayerMask _layerTouch = 1 << 9;

    private Camera _cam = null;
    private Canvas _canvas = null;

    private void Awake()
    {
        Bind<UnityEngine.UI.Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<TMPro.TextMeshProUGUI>(typeof(TextMeshs));

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

        _layerTouch = 1 << LayerMask.NameToLayer("Touch");

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
            GameObject TMP_PingPongValue = Managers.Resource.Instantiate("TMP_PingPongValue", GetObject(GameObjects.CubeInfo).transform);
            TMP_PingPongValue.transform.position = p.transform.position;
            TMP_PingPongValue.transform.rotation = Quaternion.identity;
            TMP_PingPongValue.transform.localScale = new Vector3(1, 1, 1);

            if (p._isMoving)
            {
                _blockList.Add(p);
                _TMPValueList.Add(TMP_PingPongValue);
            }

            switch (p._pingpongType)
            {
                case Define.ePingpongType.Plus:
                    TMP_PingPongValue.GetComponent<TMPro.TextMeshProUGUI>().text = $"+{p._value}";
                    break;

                case Define.ePingpongType.Muliply:
                    TMP_PingPongValue.GetComponent<TMPro.TextMeshProUGUI>().text = $"x{p._value}";
                    break;
            }
        }
    }
    private void SetPingPongValueTextPos()
    {
        if (_TMPValueList.Count > 0)
            for (int i = 0; i < _TMPValueList.Count; i++)
                _TMPValueList[i].transform.position = _blockList[i].transform.position;
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
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, _layerTouch);

        if (hit.collider != null)
        {
            hit.transform.GetComponent<SpriteRenderer>().enabled = true;

            await Task.Delay(100);

            hit.transform.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void CreateBall()
    {
        Vector2 touchPos = (Vector2)_cam.ScreenToWorldPoint(Input.mousePosition);

        GameObject TMP_Ball = Managers.Resource.Instantiate("TMP_Ball", this.transform);
        TMP_Ball.transform.localScale = new Vector3(1, 1, 1);
        _TMPBallList.Add(TMP_Ball);

        GameObject ball = Managers.Resource.Instantiate("Ball");
        ball?.GetComponent<Ball>().Init(TMP_Ball.GetComponent<TMPro.TextMeshProUGUI>());
        _ballList.Add(ball);

        if (touchPos.x < -_xBoundary)
            ball.transform.position = _leftCornerVec;
        else if (touchPos.x > _xBoundary)
            ball.transform.position = _rightCornerVec;
        else
            ball.transform.position = new Vector3(touchPos.x, 1.25f, 0);
    }

    private void SetBallTextPos()
    {
        if (_TMPBallList.Count > 0)
            for (int i = 0; i < _ballList.Count; i++)
                _TMPBallList[i].transform.position = _ballList[i].transform.position;
    }

    public void BallReset(GameObject ball, GameObject ballCountText)
    {
        if (_ballList.Contains(ball))
        {
            _TMPBallList.Remove(ballCountText);
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
        GetTextMesh(TextMeshs.TMP_ShootCount).text = _shootCount.ToString();
    }
    #endregion



    public void Clear()
    {
        GetObject(GameObjects.Lobby).SetActive(true);

        Managers.Clear();
        _ballList.Clear();
        _TMPBallList.Clear();
        _blockList.Clear();
        _TMPValueList.Clear();
    }
}
