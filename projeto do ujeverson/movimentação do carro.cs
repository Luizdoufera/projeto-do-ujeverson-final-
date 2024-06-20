using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Referências aos WheelColliders das quatro rodas
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    // Torque máximo aplicado ao motor das rodas traseiras
    public float maxMotorTorque = 1500f;
    // Ângulo máximo de direção para as rodas dianteiras
    public float maxSteeringAngle = 30f;
    // Força de frenagem regulável
    public float brakeTorque = 3000f;

    // Referência ao Rigidbody do carro
    private Rigidbody rb;

    void Start()
    {
        // Obtém a referência ao Rigidbody do carro
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Obtém o valor de entrada vertical (aceleração/frenagem)
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        // Obtém o valor de entrada horizontal (direção)
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        // Aplica o ângulo de direção às rodas dianteiras
        frontLeftWheel.steerAngle = steering;
        frontRightWheel.steerAngle = steering;

        // Verifica se a tecla de espaço (freio) está pressionada
        if (Input.GetKey(KeyCode.Space))
        {
            // Aplica força de frenagem às quatro rodas
            ApplyBrakes();
        }
        else
        {
            // Libera os freios
            ReleaseBrakes();

            // Aplica o torque do motor às rodas traseiras
            rearLeftWheel.motorTorque = motor;
            rearRightWheel.motorTorque = motor;
        }

        // Atualiza as posições visuais das rodas
        ApplyWheelPositions();
    }

    // Função para aplicar força de frenagem às quatro rodas
    void ApplyBrakes()
    {
        frontLeftWheel.brakeTorque = brakeTorque;
        frontRightWheel.brakeTorque = brakeTorque;
        rearLeftWheel.brakeTorque = brakeTorque;
        rearRightWheel.brakeTorque = brakeTorque;

        // Define o torque do motor das rodas traseiras como 0 para simular a frenagem
        rearLeftWheel.motorTorque = 0;
        rearRightWheel.motorTorque = 0;
    }

    // Função para liberar os freios das quatro rodas
    void ReleaseBrakes()
    {
        frontLeftWheel.brakeTorque = 0;
        frontRightWheel.brakeTorque = 0;
        rearLeftWheel.brakeTorque = 0;
        rearRightWheel.brakeTorque = 0;
    }

    // Função para atualizar as posições visuais das quatro rodas
    void ApplyWheelPositions()
    {
        ApplyWheelPosition(frontLeftWheel);
        ApplyWheelPosition(frontRightWheel);
        ApplyWheelPosition(rearLeftWheel);
        ApplyWheelPosition(rearRightWheel);
    }

    // Função para atualizar a posição visual de uma roda específica
    void ApplyWheelPosition(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        // Obtém a referência ao transform do objeto visual da roda
        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        // Obtém a posição e rotação da roda de acordo com o WheelCollider
        collider.GetWorldPose(out position, out rotation);

        // Define a posição e rotação do objeto visual da roda
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
}
