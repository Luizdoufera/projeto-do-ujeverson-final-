using UnityEngine;
using TMPro;

public class LapCounter : MonoBehaviour
{
    public TMP_Text lapText; // Referência ao componente de texto TMP para exibir o número de voltas
    public TMP_Text resultsText; // Referência ao componente de texto TMP para exibir os resultados finais
    public int totalLaps = 3; // Número total de voltas
    public Rigidbody carRigidbody; // Referência ao Rigidbody do carro

    private int currentLap = 0; // Contador de voltas
    private float[] lapTimes; // Array para armazenar os tempos das voltas
    private float startTime; // Tempo de início da volta atual
    private float totalTime; // Tempo total do percurso
    private float totalDistance = 0f; // Distância total percorrida

    private Vector3 lastPosition; // Última posição do carro

    void Start()
    {
        // Inicializa o array de tempos de volta
        lapTimes = new float[totalLaps];
        // Registra o tempo de início da corrida
        startTime = Time.time;
        // Armazena a posição inicial do carro
        lastPosition = transform.position;
        // Atualiza o texto da UI com o estado inicial das voltas
        lapText.text = "Lap: 0 / " + totalLaps;
        // Inicializa o texto de resultados como vazio
        resultsText.text = "";
    }

    void Update()
    {
        // Atualiza o texto da UI com o número de voltas atual
        lapText.text = "Lap: " + currentLap + " / " + totalLaps;

        // Calcula a distância percorrida desde o último frame
        float distanceThisFrame = Vector3.Distance(transform.position, lastPosition);
        // Adiciona a distância percorrida ao total
        totalDistance += distanceThisFrame;
        // Atualiza a última posição do carro
        lastPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica se o carro passou pelo checkpoint
        if (other.CompareTag("Checkpoint"))
        {
            if (currentLap < totalLaps)
            {
                // Calcula o tempo da volta atual
                float lapTime = Time.time - startTime;
                // Armazena o tempo da volta no array
                lapTimes[currentLap] = lapTime;
                // Adiciona o tempo da volta ao tempo total
                totalTime += lapTime;

                // Incrementa o contador de voltas
                currentLap++;
                // Reinicia o tempo de início para a próxima volta
                startTime = Time.time;

                // Verifica se todas as voltas foram completadas
                if (currentLap == totalLaps)
                {
                    EndRace();
                }
            }
        }
    }

    void EndRace()
    {
        // Para o carro ao definir sua velocidade e rotação angular para zero
        carRigidbody.velocity = Vector3.zero;
        carRigidbody.angularVelocity = Vector3.zero;

        // Calcula a velocidade média total em km/h
        float averageSpeed = (totalDistance / totalTime) * 3.6f;

        // Exibe os resultados finais no texto de resultados
        resultsText.text = "voltas finalizadas\n";
        resultsText.text += "Tempo total: " + totalTime.ToString("F1") + " s\n";
        resultsText.text += "Velocidade média: " + averageSpeed.ToString("F1") + " km/h\n";

        for (int i = 0; i < totalLaps; i++)
        {
            resultsText.text += "volta " + (i + 1) + ": " + lapTimes[i].ToString("F1") + " s\n";
        }

        // Parar o tempo do jogo (opcional)
        // Time.timeScale = 0f;
    }
}
