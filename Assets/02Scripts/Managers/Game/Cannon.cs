using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Define.eColorType _cannonColorType;
    private bool IsAi = false;
    private Transform _launchTf = null;
    private int _dir = 1;
    private float _speed = 50f;

    private float _timeCheck = 0f;
    private float _timeRange = 3.0f;


    public void Init()
    {
        _launchTf = Util.FindChild<Transform>(this.gameObject, "Root");

        switch (_cannonColorType)
        {
            case Define.eColorType.Red:
                IsAi = false;
                break;

            case Define.eColorType.Blue:
                IsAi = true;
                _timeRange = 3.0f;
                break;
        }
    }

    private void Update()
    {
        if (Managers.Game.GameStatePlay)
        {
            _launchTf.Rotate(Vector3.forward * Time.deltaTime * _dir * _speed);

            if (IsAi)
            {
                _timeCheck += Time.deltaTime;

                if (_timeCheck > _timeRange)
                {
                    _timeCheck = 0f;
                    _timeRange = Random.Range(3, 7);
                    Managers.Game.EnemyCannon.Fire();
                }
            }

            if (_launchTf.eulerAngles.z < 125)
                _dir = 1;
            else if (_launchTf.eulerAngles.z > 235)
                _dir = -1;
        }
        else
        {
            if (_corFire != null)
            {
                StopCoroutine(_corFire);
                _corFire = null;
            }
        }
    }

    private Coroutine _corFire = null;
    public void Fire()
    {
        if (_corFire != null)
        {
            StopCoroutine(_corFire);
            _corFire = null;
        }

        _corFire = StartCoroutine(CorFire());
    }

    private IEnumerator CorFire()
    {
        int shootCount = 0;
        if (IsAi)
            shootCount = Random.Range(10, 35);
        else
            shootCount = Managers.Game.uiGameScene.ShootCount;

        for (int i = shootCount; i > 0; i--)
        {
            GameObject bullet = Managers.Resource.Instantiate("Bullet");
            bullet.transform.position = _launchTf.position;
            float z = _launchTf.eulerAngles.z - 180;
            bullet.transform.eulerAngles = new Vector3(_launchTf.eulerAngles.x,
                                                        _launchTf.eulerAngles.y,
                                                        z);
            bullet?.GetComponent<Bullet>().Init(_cannonColorType);

            if (!IsAi)
                Managers.Game.uiGameScene.SetShootCount(-1);

            yield return Util.WaitGet(.06f);
        }
    }
}
