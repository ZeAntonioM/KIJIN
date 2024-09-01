using System.Collections;
using UnityEngine;
using TMPro;  // Certifique-se de adicionar esta linha para usar TextMeshPro

public class SubtitleTrigger : MonoBehaviour
{
    public Canvas subtitleCanvas;  // Referência ao Canvas com as legendas
    public TextMeshProUGUI subtitleText;  // Referência ao TextMeshProUGUI no Canvas
    public GameObject player;  // Referência ao jogador
    public GameObject otherCharacter;  // Referência ao outro personagem
    public float fadeDuration = 1.0f;  // Duração do fade out do sprite
    public string[] subtitles = { "Who are you?", "Come with me...sister.", "Wait!" };  // Legendas a serem exibidas


    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer otherCharacterSpriteRenderer;

    private void Start()
    {
        // Inicializar referências
 
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        otherCharacterSpriteRenderer = otherCharacter.GetComponent<SpriteRenderer>();

        // Desativar o Canvas de legendas no início
        subtitleCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            // Ativar o Canvas de legendas
            subtitleCanvas.gameObject.SetActive(true);

            Debug.Log("ENTREIII");

            // Iniciar a sequência de legendas
            StartCoroutine(PlaySubtitles());
        }
    }

    private IEnumerator PlaySubtitles()
    {
        for (int i = 0; i < subtitles.Length; i++)
        {
            subtitleText.text = subtitles[i];
            yield return new WaitForSeconds(4.0f);  // Esperar 4 segundos antes de mudar para a próxima legenda
        }

        // Após a última legenda, iniciar o fade out dos sprites
        StartCoroutine(FadeOutSprites());
    }

    private IEnumerator FadeOutSprites()
    {
        float elapsedTime = 0f;

        // Gradualmente diminuir o alpha do sprite do jogador e do outro personagem
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            playerSpriteRenderer.color = new Color(1f, 1f, 1f, alpha);
            otherCharacterSpriteRenderer.color = new Color(1f, 1f, 1f, alpha);

            yield return null;
        }

        // Desativar os personagens após o fade out
        player.SetActive(false);
        otherCharacter.SetActive(false);
    }
}
