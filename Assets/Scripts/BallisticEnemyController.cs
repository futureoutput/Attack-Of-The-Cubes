using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallisticEnemyController : EnemyController
{
    public float launchForce = 1;
    private void Start()
    {
        float magVect = (transform.position- player.transform.position).magnitude/3;
        Vector3 launchV = (-transform.position + player.transform.position)+magVect*Vector3.up;
        enemyRb.AddForce(launchV* launchForce, ForceMode.Impulse);
    }

}
