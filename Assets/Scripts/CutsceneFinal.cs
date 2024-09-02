using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class CutsceneFinal : MonoBehaviour
{
    public PlayerMovement playerMovement;    // Referência ao script de movimento do jogador
    public GameObject animatedObject;        // GameObject com a animação
    public GameObject fadingObject;          // GameObject que desaparecerá gradualmente (Sprite)
    public GameObject fadePanel;             // Painel que aparecerá gradualmente
    public GameObject creditsText;           // Texto de créditos
    public GameObject backToMenuButton;      // Botão para voltar ao menu
    public float fadeSpeed = 5f;             // Velocidade de fade
    public float animationDuration = 5f;     // Duração da animação (ajuste conforme necessário)
    public AudioClip endSound;               // Som que será tocado após a animação

    private Animator animator;               // Referência ao Animator do objeto animado
    private AudioSource audioSource;         // Referência ao AudioSource para tocar o som
    public AudioSource camerasom;

    private void Start()
    {
        fadePanel.SetActive(false);
        creditsText.SetActive(false);
        backToMenuButton.SetActive(false);
        animatedObject.SetActive(false); // Deixa o objeto animado desativado inicialmente
        

        // Inicializa o Animator e o AudioSource do objeto animado
        animator = animatedObject.GetComponent<Animator>();
        
        audioSource = animatedObject.AddComponent<AudioSource>();
        audioSource.clip = endSound;
        audioSource.playOnAwake = false; // Evita que o som toque automaticamente
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Animator playeranimator = other.GetComponent<Animator>();

            if (playerMovement != null)
            {
                playerMovement.enabled = false; // Desativa o movimento do jogador
                playeranimator.SetBool("isWalking", false); // Altera o parâmetro isWalking para false
            }

            StartCoroutine(CutsceneSequence()); // Inicia a sequência da cutscene
        }
    }

    private IEnumerator CutsceneSequence()
    {
        animatedObject.SetActive(true);      // Ativa o objeto animado
        animator.Play("Gaiola");           // Toca a animação chamada "BOX_IDLE"

        yield return new WaitForSeconds(animationDuration); // Espera o tempo da animação (ajuste conforme necessário)

        audioSource.Play(); // Toca o som após o término da animação

        yield return StartCoroutine(FadeOutObject(fadingObject)); // Desaparece gradualmente o sprite

        fadePanel.SetActive(true);
        yield return StartCoroutine(FadeInObject(fadePanel)); // Aparecer gradualmente o painel de fade

        creditsText.SetActive(true);
        yield return new WaitForSeconds(3f); // Tempo para exibir os créditos (ajuste conforme necessário)

        backToMenuButton.SetActive(true); // Exibe o botão para voltar ao menu
    }

    private IEnumerator FadeOutObject(GameObject obj)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;

        while (color.a > 0)
        {
            color.a -= Time.deltaTime * fadeSpeed;
            spriteRenderer.color = color;
            yield return null;
        }

        obj.SetActive(false);
    }

    private IEnumerator FadeInObject(GameObject obj)
    {
        Image panelImage = obj.GetComponent<Image>();
        Color color = panelImage.color;
        color.a = 0;
        panelImage.color = color;
           camerasom.volume = 0;

        while (color.a < 1)
        {
            color.a += Time.deltaTime * fadeSpeed;
            panelImage.color = color;
            yield return null;
        }
    }

 
}
