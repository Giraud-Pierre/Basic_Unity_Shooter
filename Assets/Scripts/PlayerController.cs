using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject Camera;     //Contient la caméra
    [SerializeField] private GameObject Bullet;     //contient le prefab "bullet" 
    [SerializeField] private float MovementSpeed = 1f; //on peut changer la vitesse de déplacement du joueur ici
    [SerializeField] private int health = 10;   //on peut changer les points de vie du joueur ici


    private float CameraYAngle; //va recueillir la rotation de la caméra autour de l'axe Y pour permettre le déplacement du joueur dans le bon sens
    private float SideMovement; //recueille les entrée sur les touches permettant de se déplacer sur les côté (q,d et flèche gauche et droite)
    private float ForwardMovement; //recueille les entrée sur les touches permettant de se déplacer en avant ou en arrière (z,s et flèche du haut et du bas)
    private float realMovementSpeed; //recalcule la véritable vitesse du joueur à partir de la vitesse ci-dessus et d'autres paramètres 
    private Vector3 Movement; //calcule le mouvement effectué par le joueur lors de la frame
    private Rigidbody Rb;   //recueille le rigidbody du PlayerBody
    

    
    private void SpawnBullet() //permet de générer une balle
    {
        //permet de positionner la balle suffisamment loin pour qu'il n'y ait pas de collision entre le joueur
        //et la balle à l'apparition de celle-ci
        Vector3 BulletAddedPosition = new Vector3(Mathf.Sin(CameraYAngle), 0f, Mathf.Cos(CameraYAngle)); 

        GameObject bullet = Instantiate(
                                Bullet, 
                                //permet de positionner la balle devant la caméra
                                Camera.transform.position + 2 * BulletAddedPosition,
                                //permet de positionner la balle dans la même orientation que la camera 
                                Camera.transform.rotation * Quaternion.Euler(90f,0f,0f)  //permet de faire apparaître la balle à l'horizontal et non à la vertical
                                );

        //on récupère le rigidbody de la balle et on lui applique une force
        bullet.GetComponent<Rigidbody>().AddForce(1000 * Camera.transform.forward); ;  

    }

    private void Fire() //permet de tirer une balle à chaque fois que l'on appuie sur le bouton Fire1
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnBullet();
        }
    }

    private void MovePlayer() //Permet au joueur de se mouvoir
    {
        SideMovement = Input.GetAxis("Horizontal");
        ForwardMovement = Input.GetAxis("Vertical");

        //Permet au joueur d'avancer dans la bonne direction en projetant le mouvement sur l'axe principal (de la scène)
        //sur l'axe relatif de la caméra grâce à l'angle d'Euler de la caméra autour de l'axe Y (angle entre
        //l'axe x principal et l'axe x relatif de la caméra)
        Movement = new Vector3(
                                ForwardMovement * realMovementSpeed * Mathf.Sin(CameraYAngle) + SideMovement * realMovementSpeed * Mathf.Cos(CameraYAngle),
                                0f,
                                ForwardMovement * realMovementSpeed * Mathf.Cos(CameraYAngle) - SideMovement * realMovementSpeed * Mathf.Sin(CameraYAngle)
                                );

        transform.position += Movement;
    }
    
    private void jump() //permet au joueur de sauter
    {
        Rb.AddForce(new Vector3(0f,5f,0f),ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)//gère les collision avec les ennemis et les balles
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Entity"))
        {
            Rb.AddForce(- 15f * transform.forward,ForceMode.Impulse); //Repousse le joueur en arrière (knockback)
            health--; //diminue les points de vie du joueur de 1

            if (collision.gameObject.tag == "bullet")
            {
                Destroy(collision.gameObject); //quand un joueur rencontre une balle, détruit la balle
            }

            if(health <= 0)
            {
                KillPlayer(); //tue le joueur si sa vie est à 0
            }
        }
    }

    private void check_out_limits() //tue le joueur s'il est tombé du terrain
    {
        if (transform.position.y <= -1)
        {
            KillPlayer();
        }
    }

    private void KillPlayer() //tue le joueur
    {
        Debug.Log("Vous avez perdu !!!");
        //délie la caméra du playerbody pour que l'on puisse détruire le second sans détruire la première
        Camera.transform.parent = null; 
        Destroy(gameObject); //détruit le PlayerBody
    }

    private void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody>(); //récupère le rigidbody du playerbody
    }

    // Update is called once per frame
    void Update()
    {
        //calcule la vitesse réelle du joueur en prenant en compte les fps du joueur et
        //en multipliant par un coefficient adaptant la vitesse du joueur (ce qui permet
        //de mettre 1f dans MovementSpeed, ce qui rend plus facile la modificationde la vitesse
        //directement dans l'interface unity (ex:"je veux augmenter ma vitesse de 50%, je passe  
        //donc de 1 à 1.5" est plus facile que "je passe de 15 à 22.5") )
        realMovementSpeed = MovementSpeed * Time.deltaTime *15f; 

        //Recueille l'angle de rotation autour de y de la caméra en le convertissant en radians
        //car Mathf.Sin et Mathf.cos utilise des radians (voir dans MovePlayer() )
        CameraYAngle = 2 * Mathf.PI * Camera.transform.eulerAngles.y / 360;

        MovePlayer();
        Fire();
        check_out_limits();
        if (Input.GetButtonDown("Jump")){ //Fait sauter le joueur s'il appuie sur la barre espace
            jump();
        }
    }
}
