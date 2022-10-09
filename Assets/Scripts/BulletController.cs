using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -1)
        {
            Destroy(gameObject);
        }
    }
}
