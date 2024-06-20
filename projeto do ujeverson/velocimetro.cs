using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    public TMP_Text speedText; // Referência ao componente de texto TMP para exibir a velocidade
    public RectTransform pointer; // Referência ao ponteiro do velocímetro
    public float maxSpeed = 200f; // Velocidade máxima do velocímetro
    public float maxPointerAngle = -90f; // Ângulo máximo do ponteiro
    public float minPointerAngle = 90f; // Ângulo mínimo do ponteiro
    public int smoothingFrames = 100; // Número de frames para suavização

    private Vector3 lastPosition; // Última posição do carro
    private float[] speedBuffer; // Buffer para armazenar velocidades
    private int speedBufferIndex = 0; // Índice do buffer de velocidades
    private float lastTime; // Último tempo registrado

    void Start()
    {
        lastPosition = transform.position;
        lastTime = Time.time;
        speedBuffer = new float[smoothingFrames];
        // Definir a cor da fonte para branco
        speedText.color = Color.white;
    }

    void Update()
    {
        // Calcular a velocidade usando a derivada da posição
        float currentTime = Time.time;
        float deltaTime = currentTime - lastTime;

        if (deltaTime > 0)
        {
            Vector3 currentPosition = transform.position;
            float distance = Vector3.Distance(currentPosition, lastPosition);
            float speed = (distance / deltaTime) * 3.6f; // Convertendo de m/s para km/h

            // Atualizar a última posição e tempo
            lastPosition = currentPosition;
            lastTime = currentTime;

            // Adicionar a velocidade ao buffer
            speedBuffer[speedBufferIndex] = speed;
            speedBufferIndex = (speedBufferIndex + 1) % smoothingFrames;

            // Calcular a velocidade média
            float averageSpeed = 0f;
            foreach (float s in speedBuffer)
            {
                averageSpeed += s;
            }
            averageSpeed /= smoothingFrames;

            // Exibir a velocidade média na UI
            speedText.text = averageSpeed.ToString("F1");

            // Atualizar a rotação do ponteiro do velocímetro
            float pointerAngle = Mathf.Lerp(minPointerAngle, maxPointerAngle, averageSpeed / maxSpeed);
            pointer.localRotation = Quaternion.Euler(0, 0, pointerAngle);
        }
    }
}
