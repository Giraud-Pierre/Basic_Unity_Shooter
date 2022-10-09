using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnnemyController : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private float MovementSpeed = 1f;

    private Transform player;
    private float realMovementSpeed;

    // Start is called before the first frame update
    public void check_out_limits()
    {
        if (transform.position.y <= -1)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Entity") && collision.gameObject.tag == "bullet")
        {
            gameObject.GetComponent<Rigidbody>().AddForce(2f * collision.gameObject.transform.forward);
            Destroy(collision.gameObject);
            health--;
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void MoveTowardPlayer()
    {
        transform.LookAt(player);
        transform.position += transform.forward * realMovementSpeed;
    }


    void Start ()
    {
        player = GameObject.Find("PlayerBody").transform;
    }

    // Update is called once per frame
    void Update()
    {
        realMovementSpeed = MovementSpeed * Time.deltaTime * 7f;
        MoveTowardPlayer();
        check_out_limits();
    }

}
