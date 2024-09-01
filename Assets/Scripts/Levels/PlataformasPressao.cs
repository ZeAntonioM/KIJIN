using UnityEngine;

public class plataformasPressao : MonoBehaviour
{
    // Refer�ncia ao GameObject que tem a anima��o
    public GameObject animatedObject;

    // Nome das anima��es
    public string subirAnimation = "Subir";
    public string defaultAnimation = "Default";

    // Refer�ncia ao Animator do objeto animado
    private Animator animator;

    // �udio de entrada e sa�da
    public AudioSource enterSound;
    public AudioSource exitSound;

    // Controle para garantir que o som de entrada toque apenas uma vez
    private bool hasEntered = false;

    // Refer�ncia ao SpriteRenderer para troca de sprites
    public SpriteRenderer spriteRenderer;

    // Sprites para ligado e desligado
    public Sprite spriteLigado; // Sprite quando est� dentro do trigger
    public Sprite spriteDesligado; // Sprite quando est� fora do trigger

    // M�todo chamado no in�cio do jogo
    void Start()
    {
        // Obt�m o componente Animator do objeto animado
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

    // M�todo chamado quando outro Collider2D entra no Trigger
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

            // Inicia a anima��o "Subir"
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

    // M�todo chamado quando outro Collider2D sai do Trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        // Verifica se o objeto que saiu do Trigger tem a tag "Player" ou "Box"
        if (other.CompareTag("Player") || other.CompareTag("Box"))
        {
            // Toca o som de sa�da
            if (exitSound != null)
            {
                exitSound.Play();
            }

            // Volta para a anima��o "Default"
            if (animator != null)
            {
                animator.Play(defaultAnimation);
            }

            // Muda o sprite para o estado desligado
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = spriteDesligado;
            }

            hasEntered = false; // Marca que os objetos sa�ram
        }
    }

    // M�todo chamado enquanto um objeto permanece dentro do Trigger
    private void OnTriggerStay2D(Collider2D other)
    {
        // Garante que a anima��o continue a tocar "Subir" enquanto estiver dentro
        if (animator != null && (other.CompareTag("Player") || other.CompareTag("Box")) && hasEntered)
        {
            animator.Play(subirAnimation);
        }
    }
}
