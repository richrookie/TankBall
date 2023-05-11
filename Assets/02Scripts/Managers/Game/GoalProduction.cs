using UnityEngine;
using DG.Tweening;

public class GoalProduction : MonoBehaviour
{
    private void OnEnable()
    {
        DOTween.Sequence()
                .Append(this.transform.DOMove(Managers.Game.PlayerCannon.transform.position, 1.25f)
                                        .SetEase(Ease.OutQuart))
                .OnComplete(() =>
                {
                    Managers.Game.PlayerCannon.Fire();
                    this?.GetComponent<Poolable>().Destroy();
                });
    }
}
