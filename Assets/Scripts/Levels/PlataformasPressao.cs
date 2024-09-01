using UnityEngine;

public class plataformasPressao : MonoBehaviour
{
    // Referência ao GameObject que tem a animação
    public GameObject animatedObject;

    // Nome das animações
    public string subirAnimation = "Subir";
    public string defaultAnimation = "Default";

    // Referência ao Animator do objeto animado
    private Animator animator;

    // Áudio de entrada e saída
    public AudioSource enterSound;
    public AudioSource exitSound;

    // Controle para garantir que o som de entrada toque apenas uma vez
    private bool hasEntered = false;

    // Referência ao SpriteRenderer para troca de sprites
    public SpriteRenderer spriteRenderer;

    // Sprites para ligado e desligado
    public Sprite spriteLigado; // Sprite quando está dentro do trigger
    public Sprite spriteDesligado; // Sprite quando está fora do trigger

    // Método chamado no início do jogo
    void Start()
    {
        // Obtém o componente Animator do objeto animado
        if (animatedObject != null)
        {
            animator = animatedObject.GetComponent<Animator>();
        }

        // Define o sprite inicial como desligado
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = spriteDesligado;
        }
    }

    // Método chamado quando outro Collider2D entra no Trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que entrou no Trigger tem a tag "Player" ou "Box"
        if ((other.CompareTag("Player") || other.CompareTag("Box")) && !hasEntered)
        {
            // Toca o som de entrada
            if (enterSound != null)
            {
                enterSound.Play();
            }

            // Inicia a animação "Subir"
            if (animator != null)
            {
                animator.Play(subirAnimation);
            }

            // Muda o sprite para o estado ligado
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = spriteLigado;
            }

            hasEntered = true; // Marca que um dos objetos entrou
        }
    }

    // Método chamado quando outro Collider2D sai do Trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        // Verifica se o objeto que saiu do Trigger tem a tag "Player" ou "Box"
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            // Toca o som de saída
            if (exitSound != null)
            {
                exitSound.Play();
            }

            // Volta para a animação "Default"
            if (animator != null)
            {
                animator.Play(defaultAnimation);
            }

            // Muda o sprite para o estado desligado
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = spriteDesligado;
            }

            hasEntered = false; // Marca que os objetos saíram
        }
    }

    // Método chamado enquanto um objeto permanece dentro do Trigger
    private void OnTriggerStay2D(Collider2D other)
    {
        // Garante que a animação continue a tocar "Subir" enquanto estiver dentro
        if (animator != null && (other.CompareTag("Player") || other.CompareTag("Box")) && hasEntered)
        {
            animator.Play(subirAnimation);
        }
    }
}
