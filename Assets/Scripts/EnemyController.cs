using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;

    public float moveTorque = 200;
    public float pushForce = 100;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

    }
    private void FixedUpdate()
    {
        MoveEnemy();
    }

    public virtual void MoveEnemy()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        Vector3 torqueDirection = -Vector3.Cross(lookDirection, Vector3.up).normalized;
        enemyRb.AddTorque(Time.deltaTime * moveTorque * torqueDirection);
        enemyRb.AddForce(Time.deltaTime * lookDirection * pushForce);
    }

}
