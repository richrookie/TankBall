using System.Collections;
using UnityEngine;

public class GoalProduction : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(CorProduction());
    }

    private IEnumerator CorProduction()
    {
        while (true)
        {
            float dis = (this.transform.position - Managers.Game.PlayerCannon.transform.position).sqrMagnitude;

            if (dis <= 1f)
            {
                Managers.Game.PlayerCannon.Fire();
                this?.GetComponent<Poolable>().Destroy();

                yield break;
            }

            this.transform.position = Vector3.MoveTowards(this.transform.position, Managers.Game.PlayerCannon.transform.position, 20 * Time.deltaTime);

            yield return null;
        }
    }
}
