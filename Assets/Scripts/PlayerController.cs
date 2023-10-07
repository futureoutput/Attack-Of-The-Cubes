using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float movementX;
    private float movementY;
    private bool isAlive = true;
    public float moveForce = 50;
    public float fireForce = 100;

    [SerializeField]
    private GameObject cannonBallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnFire()
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
            // Get the point where the ray hits
            mousePosition = hit.point;

            // Do something with the mouse position, for example print it to the console
            Debug.Log("Mouse position in 3D space: " + mousePosition);
            Vector3 fireVector = (mousePosition- transform.position).normalized;
            GameObject newCannonBall = Instantiate(cannonBallPrefab, transform.position + Vector3.up, Quaternion.identity);
            newCannonBall.GetComponent<Rigidbody>().AddForce(fireVector * fireForce);
        }




    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        Vector3 torqueDirection = -Vector3.Cross(movement, Vector3.up).normalized;
        playerRb.AddTorque(Time.deltaTime * moveForce * torqueDirection);

    }
}
