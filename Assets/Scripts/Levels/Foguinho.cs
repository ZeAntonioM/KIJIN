using UnityEngine;

public class Foguinho : MonoBehaviour
{
    // Refer�ncia ao som que ser� reproduzido
    private AudioSource audioSource;

    // Refer�ncia ao SpriteRenderer para controlar a transpar�ncia
    private SpriteRenderer spriteRenderer;

    // Tempo para o fade-out (em segundos)
    public float fadeDuration = 1f;

    // Indica se o fade-out est� ativo
    private bool isFading = false;

    // M�todo chamado no in�cio do jogo
    void Start()
    {
        // Obt�m o componente AudioSource e SpriteRenderer anexados ao objeto
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // M�todo chamado quando outro Collider2D entra no Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que entrou no Trigger tem a tag "Player"
        if (other.CompareTag("Player") && !isFading)
        {
            // Reproduz o som se o AudioSource estiver configurado
            if (audioSource != null)
            {
                audioSource.Play();
            }

            // Inicia o processo de fade-out
            StartCoroutine(FadeOut());
        }
    }

    // Coroutine para controlar o fade-out do objeto
    private System.Collections.IEnumerator FadeOut()
    {
        isFading = true;
        float elapsedTime = 0f;
        Color initialColor = spriteRenderer.color;

        // Executa o fade-out ao longo do tempo especificado
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

        // Garante que o objeto desapare�a completamente no final
        spriteRenderer.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        // Opcionalmente, desativa o objeto ap�s o fade-out completo
        gameObject.SetActive(false);
    }
}
