using System;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject rocketPfb;
    [SerializeField] private Transform rocketSpawnpoint;
    [SerializeField] private float rocketCount = 8;
    [SerializeField] private float rocketCooldown = 2;
    
    private float rocketTimer;
    [SerializeField] private float moveSpeed = 8;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    void Update()
    {
        RocketCooldwn();
    }

    private void FixedUpdate() //physics related
    {
        PlayerMovement();
    }

    void RocketCooldwn()
    {
        rocketTimer += Time.deltaTime;
        if (rocketTimer >= rocketCooldown)
        {
            RocketSpawner();
            rocketTimer = 0f;
        }
    }
    void RocketSpawner()
    {
        for (int i = 0; i < rocketCount; i++)
        {
            float angle = i * (360f / rocketCount); //Split rockets test
            Debug.Log("Angle = " + angle);
            float xCos = Mathf.Cos(angle * Mathf.Deg2Rad); // cos = (x)
            float ySin = Mathf.Sin(angle * Mathf.Deg2Rad); // sin = (y)
            Vector3 direction = new Vector3(xCos, ySin, 0);
            
            GameObject rocket = Instantiate(rocketPfb, rocketSpawnpoint.position, Quaternion.identity);

            rocket.GetComponent<Rocket>().RocketDirection(direction);//set when rocket starts spawning umay

        }
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = new Vector3(horizontalInput, verticalInput, 0);
        Vector3 updatePos = transform.position += movementInput * moveSpeed * Time.deltaTime;
        rb.MovePosition(updatePos); //trying with colliders xd

    }
   
}
