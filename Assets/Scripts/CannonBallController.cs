using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallController : MonoBehaviour
{
    public float lifespan =5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CannonBallTimer());
    }
    IEnumerator CannonBallTimer()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }

}
