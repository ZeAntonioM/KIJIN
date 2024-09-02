using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class CutsceneFinal : MonoBehaviour
{
    public PlayerMovement playerMovement;    // Refer�ncia ao script de movimento do jogador
    public GameObject animatedObject;        // GameObject com a anima��o
    public GameObject fadingObject;          // GameObject que desaparecer� gradualmente (Sprite)
    public GameObject fadePanel;             // Painel que aparecer� gradualmente
    public GameObject creditsText;           // Texto de cr�ditos
    public GameObject backToMenuButton;      // Bot�o para voltar ao menu
    public float fadeSpeed = 5f;             // Velocidade de fade
    public float animationDuration = 5f;     // Dura��o da anima��o (ajuste conforme necess�rio)
    public AudioClip endSound;               // Som que ser� tocado ap�s a anima��o

    private Animator animator;               // Refer�ncia ao Animator do objeto animado
    private AudioSource audioSource;         // Refer�ncia ao AudioSource para tocar o som
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
                playeranimator.SetBool("isWalking", false); // Altera o par�metro isWalking para false
            }

            StartCoroutine(CutsceneSequence()); // Inicia a sequ�ncia da cutscene
        }
    }

    private IEnumerator CutsceneSequence()
    {
        animatedObject.SetActive(true);      // Ativa o objeto animado
        animator.Play("Gaiola");           // Toca a anima��o chamada "BOX_IDLE"

        yield return new WaitForSeconds(animationDuration); // Espera o tempo da anima��o (ajuste conforme necess�rio)

        audioSource.Play(); // Toca o som ap�s o t�rmino da anima��o

        yield return StartCoroutine(FadeOutObject(fadingObject)); // Desaparece gradualmente o sprite

        fadePanel.SetActive(true);
        yield return StartCoroutine(FadeInObject(fadePanel)); // Aparecer gradualmente o painel de fade

        creditsText.SetActive(true);
        yield return new WaitForSeconds(3f); // Tempo para exibir os cr�ditos (ajuste conforme necess�rio)

        backToMenuButton.SetActive(true); // Exibe o bot�o para voltar ao menu
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
