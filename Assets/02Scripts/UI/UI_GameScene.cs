using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UI_GameScene : UI_Scene
{
    private Camera _cam = null;
    private readonly float _xBoundary = 5f;
    private readonly float _yBoundary = 4.75f;
    private readonly Vector3 _leftCornerVec = new Vector3(-5f, 1.25f, 0);
    private readonly Vector3 _rightCornerVec = new Vector3(5f, 1.25f, 0);

    private void Awake()
    {
        // Bind<Button>(typeof(Buttons));
        // Bind<TextMeshProUGUI>(typeof(TextMeshs));
        // Bind<Image>(typeof(Images));
        base.Init();

        _cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 touchPos = (Vector2)_cam.ScreenToWorldPoint(Input.mousePosition);

            GameObject ball = Managers.Resource.Instantiate("Ball");
            if (touchPos.x < -_xBoundary)
            {
                ball.transform.position = _leftCornerVec;
            }
            else if (touchPos.x > _xBoundary)
            {
                ball.transform.position = _rightCornerVec;
            }
            else
            {
                ball.transform.position = new Vector3(touchPos.x, 1.25f, 0);
            }
        }
    }
}
