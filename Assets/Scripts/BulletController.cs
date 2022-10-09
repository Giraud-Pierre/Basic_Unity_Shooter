using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision) //permet de détruire la balle s'il est tombe par terre (sur le terrain)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(transform.position.y <= -1) //permet de détruire la balle si elle sort du terrain
        {
            Destroy(gameObject);
        }
    }
}
