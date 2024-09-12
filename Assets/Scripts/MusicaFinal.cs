using UnityEngine;

public class MusicaFinal : MonoBehaviour
{
    public AudioSource audioSource1; // Primeiro AudioSource que vai diminuir o volume
    public AudioSource audioSource2; // Segundo AudioSource que vai aumentar o volume
    public float transitionSpeed = 1f; // Velocidade da transição do volume

    private bool isTransitioning = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que entrou no Trigger tem a tag "Player"
        if (collision.CompareTag("Player"))
        {
            if (!isTransitioning) {

                audioSource2.Play();
            }
            isTransitioning = true;
            

        }
    }

    private void Update()
    {
        if (isTransitioning)
        {
            // Reduz o volume do primeiro AudioSource
            if (audioSource1.volume > 0)
            {
                audioSource1.volume -= transitionSpeed * Time.deltaTime;
                audioSource1.volume = Mathf.Clamp(audioSource1.volume, 0, 1); // Garante que o volume não fique abaixo de 0
            }

            // Aumenta o volume do segundo AudioSource
            if (audioSource2.volume < 1)
            {
                audioSource2.volume += transitionSpeed * Time.deltaTime;
                audioSource2.volume = Mathf.Clamp(audioSource2.volume, 0, 1); // Garante que o volume não fique acima de 1
            }

            // Verifica se a transição foi concluída
            if (audioSource1.volume == 0 && audioSource2.volume == 1)
            {
                isTransitioning = false;
            }
        }
    }
}