using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperController : EnemyController
{
    public float jumpForce = 10f;


    private void OnCollisionEnter(Collision collision)
    {
        //check if cannon ball touches enemy
        if (collision.gameObject.CompareTag("Ground"))
        {
            enemyRb.AddForce(jumpForce*Vector3.up, ForceMode.Impulse);
        }
    }
}
