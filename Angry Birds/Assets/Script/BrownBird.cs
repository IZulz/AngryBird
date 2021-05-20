using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownBird : Birds
{
    public float impactRadius;
    public float forceImpcat;

    public LayerMask targetLayer;

    private void Update()
    {        
        if(_state == BirdState.HitSomething)
        {
            Explod();
        }
    }

    void Explod()
    {
        Collider2D[] collObj = Physics2D.OverlapCircleAll(transform.position, impactRadius, targetLayer);
        foreach(Collider2D obj in collObj)
        {
            Vector2 direction = obj.transform.position - transform.position;

            obj.GetComponent<Rigidbody2D>().AddForce(direction * forceImpcat);
            --impactRadius;
            --forceImpcat;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, impactRadius);
    }
}
