using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject PrefabEnnemy;
    [SerializeField] private GameObject SafeZone;
    [SerializeField] private GameObject terrain;

    private IEnumerator SpawnEnnemies()
    {
        while (true)
        {
            Instantiate(PrefabEnnemy, position: new Vector3(Random.Range(0, 100), 2f, Random.Range(0, 100)), Quaternion.identity);
            yield return new WaitForSeconds(10f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == terrain)
        {
            Destroy(SafeZone);
            StartCoroutine(SpawnEnnemies());
        }
    }
}
