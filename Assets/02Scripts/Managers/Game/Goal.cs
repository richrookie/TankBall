using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            other?.GetComponent<Poolable>().Destroy();

            GameObject effect = Managers.Resource.Instantiate("Effect_Goal");
            effect.transform.position = other.transform.position;
            effect.transform.rotation = Quaternion.identity;
        }
    }
}
