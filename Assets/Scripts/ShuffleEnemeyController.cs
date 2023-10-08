using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleEnemeyController : EnemyController
{
    public float shuffleTime = 4f;
    private float timePassedSinceLastShuffle = 0;


    public override void MoveEnemy()
    {
        timePassedSinceLastShuffle += Time.deltaTime;
        if (timePassedSinceLastShuffle>shuffleTime)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * pushForce, ForceMode.Impulse);
            Vector3 torqueDirection = Vector3.up;
            enemyRb.AddTorque(moveTorque * torqueDirection, ForceMode.Impulse);
            timePassedSinceLastShuffle = Random.Range(0, shuffleTime-1);
        }
    }
}
