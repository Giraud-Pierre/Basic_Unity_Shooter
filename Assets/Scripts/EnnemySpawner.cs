using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject PrefabEnnemy; //recueille le prefab ennemy
    [SerializeField] private GameObject SafeZone; //recueille le gameobject SafeZone
    [SerializeField] private GameObject terrain; //recueille le gameObject terrain

    private IEnumerator SpawnEnnemies() //fait appara�tre un ennemi toutes les 10 secondes
    {
        while (true)
        {
            //instantie un ennemi � une position al�atoire sur le terrain
            Instantiate(PrefabEnnemy, position: new Vector3(Random.Range(0, 100), 2f, Random.Range(0, 100)), Quaternion.identity);
            yield return new WaitForSeconds(10f);
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        //lorsque le joueur entre sur le terrain (il est donc sorti de la safezone)
        if (collision.gameObject == terrain)
        {
            Destroy(SafeZone); //d�tuit la safe zone
            StartCoroutine(SpawnEnnemies()); //d�marre l'apparition des ennemis
        }
    }
}
