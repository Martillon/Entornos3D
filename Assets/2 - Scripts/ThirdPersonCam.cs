using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")] 
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Calcula la dirección de vista del jugador hacia la cámara
        Vector3 viewDir = player.position - transform.position;
        orientation.forward = viewDir.normalized;

        // Obtiene las entradas de los ejes horizontal y vertical
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcula la dirección de entrada combinando la dirección hacia adelante y hacia la derecha de la orientación
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Si hay una entrada no nula, realiza la rotación del jugador
        if (inputDir.magnitude > 0.1f)
        {
            // Calcula la rotación objetivo basada en la dirección de entrada
            Quaternion targetRotation = Quaternion.LookRotation(inputDir, Vector3.up);

            // Aplica una rotación suavizada al jugador
            playerObj.rotation = Quaternion.Slerp(playerObj.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
