using UnityEngine;
using DG.Tweening;

public class GoalProduction : MonoBehaviour
{
    private void OnEnable()
    {
        DOTween.Sequence()
                .Append(this.transform.DOMove(Managers.Game.PlayerCannon.position, 1.25f)
                                        .SetEase(Ease.OutQuart))
                .OnComplete(() =>
                {
                    this?.GetComponent<Poolable>().Destroy();
                });
    }
}
