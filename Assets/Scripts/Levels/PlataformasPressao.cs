using UnityEngine;

public class plataformasPressao : MonoBehaviour
{
    public GameObject animatedObject;

    public string subirAnimation = "Subir";
    public string defaultAnimation = "Default";

    private Animator animator;

    public AudioSource enterSound;
    public AudioSource exitSound;

    private bool active = false;
    private GameObject[] objectsOnTop;

    public SpriteRenderer spriteRenderer;

    public Sprite spriteLigado; // Sprite quando est� dentro do trigger
    public Sprite spriteDesligado; // Sprite quando est� fora do trigger

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

        objectsOnTop = new GameObject[0];
    }

    private void CheckActive() {
        
        if (objectsOnTop.Length > 0) {

            active = true;

        }
        else {

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

            active = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que entrou no Trigger tem a tag "Player" ou "Box"
        if (other.CompareTag("Player") || other.CompareTag("Box"))
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

            // Adiciona o objeto que entrou no Trigger ao array de objetos
            GameObject[] newObjectsOnTop = new GameObject[objectsOnTop.Length + 1];
            for (int i = 0; i < objectsOnTop.Length; i++)
            {
                newObjectsOnTop[i] = objectsOnTop[i];
            }
            newObjectsOnTop[objectsOnTop.Length] = other.gameObject;
            objectsOnTop = newObjectsOnTop;        

            CheckActive();
        }
    }

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

            // Remove o objeto que saiu do Trigger do array de objetos
            GameObject[] newObjectsOnTop = new GameObject[objectsOnTop.Length - 1];
            int j = 0;
            for (int i = 0; i < objectsOnTop.Length; i++)
            {
                if (objectsOnTop[i] != other.gameObject)
                {
                    newObjectsOnTop[j] = objectsOnTop[i];
                    j++;
                }
            }
            objectsOnTop = newObjectsOnTop;

            CheckActive();

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Garante que a anima��o continue a tocar "Subir" enquanto estiver dentro
        if (animator != null && (other.CompareTag("Player") || other.CompareTag("Box")))
        {
            animator.Play(subirAnimation);
        }

    }
}
