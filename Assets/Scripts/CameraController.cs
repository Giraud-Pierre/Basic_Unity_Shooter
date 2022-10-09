using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float CameraSensitivity = 1f; //permet de régler la sensibilité de la caméra
    
    private float CameraXPosition; //recueille la rotation autour de X de la caméra
    private float CameraYPosition; //recueille la rotation autour de Y de la caméra

    //pareil que la MouvementSpeed du PlayerController, ajuste la sensibilité avec certains paramètres
    private float realCameraSensitivity; 


    private void MoveCamera() //permet la rotation de la caméra
    {
        //Je suis passé par ces 2 variables intermédiaires pour faire bouger la caméra car cela permet 
        //1) de ne pas récupérer les angles d'euler de la caméra à chaque frame
        //2) de bien utiliser Mathf.clamp car quand je l'utilisais directement sur le x d'un angle d'euler
        //ça marchait un peu mais quand on s'approchait des valeurs maximales, ça faisait parfois des choses
        //bizarres comme des changements abruptes dans l'orientation de la caméra sans raison
        CameraXPosition += -Input.GetAxis("Mouse Y")* realCameraSensitivity;
        CameraYPosition += Input.GetAxis("Mouse X") * realCameraSensitivity;

        //fixe la position du regard vers le haut et vers le bas à un mmaximum et un minimum pour améliorer
        //le confort du joueur (regarder totalement vers le bas ou vers le haut peut rendre les contrôles
        //"perturbants")
        CameraXPosition = Mathf.Clamp(CameraXPosition, -45f, 45f); 

        //Met à jour l'orientation de la caméra
        transform.eulerAngles = new Vector3(CameraXPosition, CameraYPosition, 0f);

    }

    private void Awake()
    {
        //permet d'éviter d'avoir le curseur qui se balade sur l'écran pendant le jeu.
        //il faut d'abord cliquer sur la fenêtre de jeu pour qu'il se mette en place
        Cursor.lockState = CursorLockMode.Locked;

        //initialise l'orientation de la caméra
        CameraXPosition = 0f;
        CameraYPosition = 0f;
        transform.eulerAngles = new Vector3(CameraXPosition, CameraYPosition, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //calcule la sensibilité réelle de la caméra en prenant en compte les fps du joueurs
        //et en appliquant un facteur (comme pour la MovementSpeed dans le script PlayerController)
        realCameraSensitivity = CameraSensitivity * Time.deltaTime * 1000f;
        MoveCamera();
    }
}
