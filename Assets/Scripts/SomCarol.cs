using UnityEngine;

public class SomCarol : MonoBehaviour
{
    // Referência ao componente AudioSource
    public AudioSource walkingSound;
    public AudioSource Jump;
    public AudioSource Push;

    // Referência ao Animator
    public Animator animator;

    // Nome do parâmetro bool no Animator
    public string isWalkingParameterName = "isWalking";

    // Intervalo de tempo para tocar o som (em segundos)
    public float soundInterval = 0.5f;

    // Temporizador interno para controlar o som
    private float timer = 0f;

    private Rigidbody2D rb; // Referência ao Rigidbody2D
    private bool isColliding = false; // Indica se o objeto está colidindo com a tag "Box"
    private Vector3 lastPosition; // Última posição registrada da caixa
    private GameObject boxObject; // Referência ao GameObject com a tag "Box"

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtém o componente Rigidbody2D do GameObject atual
        if (rb == null)
        {
            Debug.LogError("O GameObject precisa ter um Rigidbody2D para este script funcionar.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            isColliding = true; // O objeto está colidindo com a tag "Box"
            boxObject = collision.gameObject; // Armazena o GameObject com a tag "Box"
            lastPosition = boxObject.transform.position; // Registra a posição inicial da caixa
            Debug.Log("Colidiu com Box");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            isColliding = false; // O objeto parou de colidir com a tag "Box"
            Debug.Log("Saiu da colisão com Box");
            if (Push.isPlaying)
            {
                Push.Stop(); // Para o som quando a colisão com a tag "Box" termina
            }
            // Atualiza a posição final ao sair da colisão
            lastPosition = boxObject.transform.position;
        }
    }

    void Update()
    {
        // Verifica o valor do parâmetro isWalking no Animator
        bool isWalking = animator.GetBool(isWalkingParameterName);

        if (isWalking)
        {
            // Incrementa o temporizador com o tempo desde a última atualização
            timer += Time.deltaTime;

            // Verifica se o tempo passado é maior ou igual ao intervalo definido
            if (timer >= soundInterval)
            {
                // Toca o som
                walkingSound.Play();

                // Reseta o temporizador
                timer = 0f;
            }
        }
        else
        {
            // Reseta o temporizador quando não está andando
            timer = 0f;

            // Para o som se estiver tocando
            if (walkingSound.isPlaying)
            {
                walkingSound.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump.Play();
        }

        if (boxObject != null && isColliding)
        {
            // Verifica se a posição x da caixa mudou em relação à última posição registrada
            if (Mathf.Abs(lastPosition.x - boxObject.transform.position.x) > 0f)
            {
                if (!Push.isPlaying) // Verifica se o som não está tocando
                {
                    Push.Play(); // Reproduz o som
                }
            }
            else
            {
                if (Push.isPlaying) // Verifica se o som está tocando
                {
                    Push.Stop(); // Para o som
                }
            }
        }
    }
}
