using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //permet de détruire la balle s'elle est tombée par terre (sur le terrain)
    private void OnCollisionEnter(Collision collision) 
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
