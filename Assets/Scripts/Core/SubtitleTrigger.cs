using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.UI;  // Certifique-se de adicionar esta linha para usar TextMeshPro

public class SubtitleTrigger : MonoBehaviour
{
    public Canvas subtitleCanvas;  // Refer�ncia ao Canvas com as legendas
    public TextMeshProUGUI subtitleText;  // Refer�ncia ao TextMeshProUGUI no Canvas
    public GameObject player;  // Refer�ncia ao jogador
    public GameObject otherCharacter;  // Refer�ncia ao outro personagem
    public float fadeDuration = 1.0f;  // Dura��o do fade out do sprite
    public string[] subtitles = { "Carol: Who are you?", "Stranger: ...", "Carol: Wait!" };  // Legendas a serem exibidas
    [SerializeField] private GameObject planeToFade;
    [SerializeField] private GameObject Botoes;
    [SerializeField] private GameObject PauseMenu;
    
    private bool activated;
    private SpriteRenderer playerSpriteRenderer;
    private SpriteRenderer otherCharacterSpriteRenderer;

    private void Start()
    {
        activated = false;

        // Inicializar refer�ncias
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        otherCharacterSpriteRenderer = otherCharacter.GetComponent<SpriteRenderer>();

        // Desativar o Canvas de legendas no in�cio
        subtitleCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!activated) {
            if (collision.CompareTag("Player"))
            {

                activated = true;


                // Ativar o Canvas de legendas
                subtitleCanvas.gameObject.SetActive(true);

                // Iniciar a sequ�ncia de legendas
                StartCoroutine(PlaySubtitles());
            }
        }

        
    }

    private IEnumerator PlaySubtitles()
    {

        Botoes.SetActive(false);

        for (int i = 0; i < subtitles.Length; i++)
        {
            subtitleText.text = subtitles[i];
            yield return new WaitForSeconds(4.0f);  // Esperar 4 segundos antes de mudar para a pr�xima legenda
        }

        // Ap�s a �ltima legenda, iniciar o fade out dos sprites
        StartCoroutine(FadeOutSprites());

        // FadeOut para mudar para o Menu
        StartCoroutine(FadeOutToMenus());
    }

    private IEnumerator FadeOutSprites()
    {
        float elapsedTime = 0f;

        // Gradualmente diminuir o alpha do sprite do jogador e do outro personagem
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            //playerSpriteRenderer.color = new Color(1f, 1f, 1f, alpha);
            otherCharacterSpriteRenderer.color = new Color(1f, 1f, 1f, alpha);

            yield return null;
        }

        // Desativar os personagens ap�s o fade out
        //player.SetActive(false);
        otherCharacter.SetActive(false);

    }

    private IEnumerator FadeOutToMenus()
    {
        float elapsedTime = 0f;

        PauseMenu.SetActive(false);
        subtitleCanvas.gameObject.SetActive(false);

        // Gradualmente aumentar o alpha do Canvas de legendas
        while (elapsedTime < 5)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / 5);

            planeToFade.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, alpha);

            yield return null;
        }

        // Voltar para o Main Menu
        SceneManager.LoadScene(0);
    }
}
