using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float CameraSensitivity = 1f; //permet de r�gler la sensibilit� de la cam�ra
    
    private float CameraXPosition; //recueille la rotation autour de X de la cam�ra
    private float CameraYPosition; //recueille la rotation autour de Y de la cam�ra

    //pareil que la MouvementSpeed du PlayerController, ajuste la sensibilit� avec certains param�tres
    private float realCameraSensitivity; 


    private void MoveCamera() //permet la rotation de la cam�ra
    {
        //Je suis pass� par ces 2 variables interm�diaires pour faire bouger la cam�ra car cela permet 
        //1) de ne pas r�cup�rer les angles d'euler de la cam�ra � chaque frame
        //2) de bien utiliser Mathf.clamp car quand je l'utilisais directement sur le x d'un angle d'euler
        //�a marchait un peu mais quand on s'approchait des valeurs maximales, �a faisait parfois des choses
        //bizarres comme des changements abruptes dans l'orientation de la cam�ra sans raison
        CameraXPosition += -Input.GetAxis("Mouse Y")* realCameraSensitivity;
        CameraYPosition += Input.GetAxis("Mouse X") * realCameraSensitivity;

        //fixe la position du regard vers le haut et vers le bas � un mmaximum et un minimum pour am�liorer
        //le confort du joueur (regarder totalement vers le bas ou vers le haut peut rendre les contr�les
        //"perturbants")
        CameraXPosition = Mathf.Clamp(CameraXPosition, -45f, 45f); 

        //Met � jour l'orientation de la cam�ra
        transform.eulerAngles = new Vector3(CameraXPosition, CameraYPosition, 0f);

    }

    private void Awake()
    {
        //permet d'�viter d'avoir le curseur qui se balade sur l'�cran pendant le jeu.
        //il faut d'abord cliquer sur la fen�tre de jeu pour qu'il se mette en place
        Cursor.lockState = CursorLockMode.Locked;

        //initialise l'orientation de la cam�ra
        CameraXPosition = 0f;
        CameraYPosition = 0f;
        transform.eulerAngles = new Vector3(CameraXPosition, CameraYPosition, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //calcule la sensibilit� r�elle de la cam�ra en prenant en compte les fps du joueurs
        //et en appliquant un facteur (comme pour la MovementSpeed dans le script PlayerController)
        realCameraSensitivity = CameraSensitivity * Time.deltaTime * 1000f;
        MoveCamera();
    }
}
