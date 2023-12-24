using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Define.eColorType _cannonColorType;
    private bool _isAi = false;
    private Transform _cannonLaunchPad = null;
    private int _dir = 1;
    private float _speed = 50f;


    public void Init()
    {
        _cannonLaunchPad = this.transform.GetChild(0).transform;

        switch (_cannonColorType)
        {
            case Define.eColorType.Red:
                _isAi = false;
                break;

            case Define.eColorType.Blue:
                _isAi = true;
                break;
        }
    }

    private void Update()
    {
        if (Managers.Game.GameStatePlay)
        {
            _cannonLaunchPad.Rotate(Vector3.forward * _dir * _speed * Time.deltaTime);

            if (_cannonLaunchPad.eulerAngles.z < 125)
                _dir = 1;
            else if (_cannonLaunchPad.eulerAngles.z > 235)
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

        if (!_isAi)
            Managers.Game.EnemyCannon.Fire();

        _corFire = StartCoroutine(CorFire());
    }

    private IEnumerator CorFire()
    {
        int shootCount = 0;
        if (_isAi)
            shootCount = Random.Range(10, 35);
        else
            shootCount = Managers.Game.uiGameScene.ShootCount;

        for (int i = shootCount; i > 0; i--)
        {
            GameObject bullet = Managers.Resource.Instantiate("Bullet");
            bullet.transform.position = _cannonLaunchPad.position;
            float z = _cannonLaunchPad.eulerAngles.z - 180;
            bullet.transform.eulerAngles = new Vector3(_cannonLaunchPad.eulerAngles.x,
                                                        _cannonLaunchPad.eulerAngles.y,
                                                        z);
            bullet?.GetComponent<Bullet>().Init(_cannonColorType);

            if (!_isAi)
                Managers.Game.uiGameScene.SetShootCount(-1);

            yield return Util.WaitGet(.06f);
        }
    }
}
