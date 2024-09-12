using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneFinal : MonoBehaviour
{
    public PlayerMovement playerMovement;    // Refer�ncia ao script de movimento do jogador
    public PauseMenu pauseMenu;
    public GameObject animatedObject;        // GameObject com a anima��o
    public GameObject fadingObject;          // GameObject que desaparecer� gradualmente (Sprite)
    public GameObject fadePanel;             // Painel que aparecer� gradualmente
    public GameObject creditsText;           // Texto de cr�ditos
    public GameObject backToMenuButton;      // Bot�o para voltar ao menu
    public float fadeSpeed = 5f;             // Velocidade de fade
    public float animationDuration = 5f;     // Dura��o da anima��o (ajuste conforme necess�rio)
    public AudioClip endSound;               // Som que ser� tocado ap�s a anima��o
    public AudioClip ghost;                  // Som que ser� tocado durante o fade-out

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

            if (pauseMenu != null)
            {
                pauseMenu.enabled = false; // Desativa o menu de pausa
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

        // Inicia a reprodu��o do som "ghost" e faz o fade-out do objeto
        AudioSource ghostAudioSource = gameObject.AddComponent<AudioSource>();
        ghostAudioSource.clip = ghost;
        ghostAudioSource.loop = true; // Faz o som repetir, se necess�rio
        ghostAudioSource.Play();

        yield return StartCoroutine(FadeOutObject(fadingObject, ghostAudioSource)); // Desaparece gradualmente o sprite e controla o som

        ghostAudioSource.Stop(); // Para o som "ghost" ap�s o fade-out

        fadePanel.SetActive(true);
        yield return StartCoroutine(FadeInObject(fadePanel)); // Aparecer gradualmente o painel de fade

        creditsText.SetActive(true);
        yield return new WaitForSeconds(3f); // Tempo para exibir os cr�ditos (ajuste conforme necess�rio)

        backToMenuButton.SetActive(true); // Exibe o bot�o para voltar ao menu
    }

    private IEnumerator FadeOutObject(GameObject obj, AudioSource ghostAudioSource)
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
