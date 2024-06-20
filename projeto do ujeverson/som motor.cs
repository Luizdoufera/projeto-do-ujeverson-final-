using UnityEngine;

public class EngineSound : MonoBehaviour
{
    public AudioSource engineAudio; // Referência ao componente AudioSource para o som do motor
    public AudioSource ignitionAudio; // Referência ao componente AudioSource para o som de inicialização
    public AudioClip ignitionClip; // Clip de áudio para o som de inicialização
    public float minPitch = 0.5f; // Pitch mínimo do som do motor
    public float maxPitch = 2.0f; // Pitch máximo do som do motor
    public float maxRPM = 7000.0f; // RPM máximo do motor

    private Rigidbody rb; // Referência ao Rigidbody do carro

    void Start()
    {
        // Obter a referência ao Rigidbody
        rb = GetComponent<Rigidbody>();

        // Configurar o AudioSource para o som de inicialização
        ignitionAudio.clip = ignitionClip;
        ignitionAudio.loop = false; // Som de inicialização não deve ser loop
        ignitionAudio.Play(); // Tocar o som de inicialização

        // Iniciar a reprodução do som do motor após o som de inicialização terminar
        Invoke("StartEngineSound", ignitionAudio.clip.length);
    }

    void Update()
    {
        // Calcular as RPM do motor
        float rpm = CalculateRPM();

        // Mapear as RPM para o pitch do som do motor
        float pitch = Mathf.Lerp(minPitch, maxPitch, rpm / maxRPM);

        // Definir o pitch do AudioSource
        engineAudio.pitch = pitch;
    }

    // Função para iniciar o som do motor
    void StartEngineSound()
    {
        // Certificar-se de que o AudioSource do motor está configurado corretamente
        engineAudio.loop = true; // Som do motor deve ser loop
        engineAudio.Play(); // Tocar o som do motor
    }

    // Função para calcular as RPM do motor
    float CalculateRPM()
    {
        // Aqui você precisa implementar a lógica para calcular as RPM do motor
        // Exemplo simples: velocidade das rodas multiplicada por uma constante de relação de transmissão
        // Esta é uma fórmula simplificada e deve ser ajustada conforme a lógica do seu jogo

        // Calcular a velocidade da roda em RPM
        float wheelRPM = (rb.velocity.magnitude / (2 * Mathf.PI * 0.34f)) * 60; // Supondo que o raio da roda é 0.34 metros
        float gearRatio = 4.0f; // Exemplo de relação de transmissão

        return wheelRPM * gearRatio; // Retornar as RPM do motor
    }
}
