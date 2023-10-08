using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Rigidbody enemyRb;
    protected GameObject player;

    public float moveTorque = 200;
    public float pushForce = 100;
    public float health = 100;
    public int scoreValue = 10;

    // Start is called before the first frame update
    void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

    }
    private void FixedUpdate()
    {
        MoveEnemy();
    }

    private void Update()
    {
        if (health <= 0 || transform.position.y < -10)
        {
            KillEnemy();
        }
    }

    public virtual void MoveEnemy()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        Vector3 torqueDirection = -Vector3.Cross(lookDirection, Vector3.up).normalized;
        enemyRb.AddTorque(Time.deltaTime * moveTorque * torqueDirection);
        enemyRb.AddForce(Time.deltaTime * lookDirection * pushForce);
    }

    private void KillEnemy()
    {
        if (DataManager.Instance.isGameActive)
        {
            DataManager.Instance.currentScore += scoreValue;
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check if cannon ball touches enemy
        if (collision.gameObject.CompareTag("CannonBall"))
        {
            float damage = collision.relativeVelocity.magnitude;
            Debug.Log(damage);
            health -= damage;
        }
    }



}
