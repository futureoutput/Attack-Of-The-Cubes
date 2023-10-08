using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float movementX;
    private float movementY;
    public float moveForce = 50;
    public float fireForce = 100;
    public float fireDelay = 1;
    private bool isReadyToFire = true;

    [SerializeField]
    private GameObject cannonBallPrefab;
    [SerializeField]
    private AudioClip cannonShootSound;

    private AudioSource audioSource;

    private bool _isAlive = true;
    public bool isAlive {get{return _isAlive;}}

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        _isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            _isAlive = false;
        }
    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnFire()
    {
        if (isReadyToFire && _isAlive)
        {
            Camera mainCamera = Camera.main;
            Vector2 screenPosition = Mouse.current.position.ReadValue();
            Vector3 mousePosition;

            // Create a ray from the camera to the mouse position
            Ray ray = mainCamera.ScreenPointToRay(screenPosition);

            // Declare a variable to store the hit information
            RaycastHit hit;

            // Perform a raycast and check if it hits anything
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                audioSource.PlayOneShot(cannonShootSound, 1.0f);
                // Get the point where the ray hits
                mousePosition = hit.point;

                Vector3 fireVector = (mousePosition - transform.position).normalized;
                GameObject newCannonBall = Instantiate(cannonBallPrefab, transform.position + fireVector, Quaternion.identity);
                newCannonBall.GetComponent<Rigidbody>().AddForce(fireVector * fireForce);
                isReadyToFire = false;
                StartCoroutine(CannonBallTimer());
            }
        }
    }

    IEnumerator CannonBallTimer()
    {
        yield return new WaitForSeconds(fireDelay);
        isReadyToFire = true;
    }
    private void FixedUpdate()
    {
        if (_isAlive)
        {
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);
            Vector3 torqueDirection = -Vector3.Cross(movement, Vector3.up).normalized;
            playerRb.AddTorque(Time.deltaTime * moveForce * torqueDirection);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check if enemy touches player
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _isAlive = false;
        }

    }
}
