using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnnemyController : MonoBehaviour
{
    [SerializeField] private int health = 3; //on peut modifier les points de vie de l'ennemi ici
    [SerializeField] private float MovementSpeed = 1f; //on peut modifier la vitesse de d�placement de l'ennemi ici

    private Transform player;   //recueille le transform du joueur pour orienter le d�placement de l'ennemi
    private float realMovementSpeed; //recalcule la vitesse de d�placement de l'ennemi en prenant en compte d'autre facteur

    public void check_out_limits()//D�truit l'ennemi s'il est tomb� du terrain
    {
        if (transform.position.y <= -1)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) //g�re la collision des ennemis avec les balles
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Entity") && collision.gameObject.tag == "bullet")
        {
            //fait reculer l'ennemi
            gameObject.GetComponent<Rigidbody>().AddForce(-15f * transform.forward, ForceMode.Impulse);

            //d�truit la balle
            Destroy(collision.gameObject); 

            //diminue la vie de l'ennemi
            health--;
            if(health <= 0) //D�truit l'ennemi s'il est � cours de point de vie
            {
                Destroy(gameObject);
            }
        }
    }

    private void MoveTowardPlayer() //permet de bouger l'ennemi vers le joueur
    {
        transform.LookAt(player); //permet � l'ennemi de regarder le joueur
        transform.position += transform.forward * realMovementSpeed; //fait avancer l'ennemi
    }


    void Start ()
    {
        player = GameObject.Find("PlayerBody").transform; //va rechercher le PlayerBody du joueur
    }


    void Update()
    {
        //calcule la vitesse de d�placement r�elle de l'ennemi en prenant en compte les fps du joueurs
        //et en appliquant un facteur (comme pour la MovementSpeed dans le script PlayerController)
        //(il va un peu moins que 2 fois moins vite que le joueur pour qui le facteur est 15f)
        realMovementSpeed = MovementSpeed * Time.deltaTime * 7f;

        MoveTowardPlayer();
        check_out_limits();
    }

}
