using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject Camera;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float MovementSpeed = 1f;
    [SerializeField] private int health = 10;


    private float CameraYAngle;
    private float SideMovement;
    private float ForwardMovement;
    private float realMovementSpeed;
    private Vector3 Movement;
    private Rigidbody Rb;
    

    
    private void SpawnBullet()
    {
        Vector3 BulletAddedPosition = new Vector3(Mathf.Sin(CameraYAngle), 0f, Mathf.Cos(CameraYAngle));

        GameObject bullet = Instantiate(
                                Bullet, 
                                Camera.transform.position + 2 * BulletAddedPosition, 
                                Camera.transform.rotation * Quaternion.Euler(90f,0f,0f)
                                );

        bullet.GetComponent<Rigidbody>().AddForce(1000 * Camera.transform.forward); ;  //on récupère le rigidbody et on lui applique une force

    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnBullet();
        }
    }

    private void MovePlayer()
    {
        SideMovement = Input.GetAxis("Horizontal");
        ForwardMovement = Input.GetAxis("Vertical");

        Movement = new Vector3(
                                ForwardMovement * realMovementSpeed * Mathf.Sin(CameraYAngle) + SideMovement * realMovementSpeed * Mathf.Cos(CameraYAngle),
                                0f,
                                ForwardMovement * realMovementSpeed * Mathf.Cos(CameraYAngle) - SideMovement * realMovementSpeed * Mathf.Sin(CameraYAngle)
                                );

        transform.position += Movement;
    }
    
    private void jump()
    {
        Rb.AddForce(new Vector3(0f,5f,0f),ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Entity"))
        {
            Rb.AddForce(- 15f * transform.forward,ForceMode.Impulse);
            health--;

            if (collision.gameObject.tag == "bullet")
            {
                Destroy(collision.gameObject);
            }

            if(health <= 0)
            {
                KillPlayer();
            }
        }
    }

    private void check_out_limits()
    {
        if (transform.position.y <= -1)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        Debug.Log("Vous avez perdu !!!");
        Camera.transform.parent = null;
        Destroy(gameObject);
    }

    private void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        realMovementSpeed = MovementSpeed * Time.deltaTime *15f;

        CameraYAngle = 2 * Mathf.PI * Camera.transform.eulerAngles.y / 360;

        MovePlayer();
        Fire();
        check_out_limits();
        if (Input.GetButtonDown("Jump")){
            jump();
        }
    }
}
