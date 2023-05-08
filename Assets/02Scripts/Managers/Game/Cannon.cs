using UnityEngine;
using DG.Tweening;

public class Cannon : MonoBehaviour
{
    private Transform _cannonLaunchPad = null;

    private void Awake()
    {
        _cannonLaunchPad = this.transform.GetChild(0).transform;

        _cannonLaunchPad.transform.DORotateQuaternion(Quaternion.Euler(0, 0, -35), 1.5f)
                                .SetEase(Ease.Linear)
                                .SetLoops(-1, LoopType.Yoyo);
    }
}
