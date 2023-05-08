using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingpongBlockGlow : MonoBehaviour
{
    private Coroutine _timerCor = null;

    public void Timer(float timer)
    {
        if (_timerCor != null)
        {
            StopCoroutine(_timerCor);
            _timerCor = null;
            return;
        }

        _timerCor = StartCoroutine(CorTimer(timer));
    }
    private IEnumerator CorTimer(float timer)
    {
        yield return Util.WaitGet(timer);

        _timerCor = null;
        this.gameObject.SetActive(false);
    }
}
