using UnityEngine;

public class SomCarol : MonoBehaviour
{
    // Refer�ncia ao componente AudioSource
    public AudioSource walkingSound;
    public AudioSource Jump;
    public AudioSource Push;

    // Refer�ncia ao Animator
    public Animator animator;

    // Nome do par�metro bool no Animator
    public string isWalkingParameterName = "isWalking";

    // Intervalo de tempo para tocar o som (em segundos)
    public float soundInterval = 0.5f;

    // Temporizador interno para controlar o som
    private float timer = 0f;

    private Rigidbody2D rb; // Refer�ncia ao Rigidbody2D
    private bool isColliding = false; // Indica se o objeto est� colidindo com a tag "Box"
    private Vector3 lastPosition; // �ltima posi��o registrada da caixa
    private GameObject boxObject; // Refer�ncia ao GameObject com a tag "Box"

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obt�m o componente Rigidbody2D do GameObject atual
        if (rb == null)
        {
            Debug.LogError("O GameObject precisa ter um Rigidbody2D para este script funcionar.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            isColliding = true; // O objeto est� colidindo com a tag "Box"
            boxObject = collision.gameObject; // Armazena o GameObject com a tag "Box"
            lastPosition = boxObject.transform.position; // Registra a posi��o inicial da caixa
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            isColliding = false; // O objeto parou de colidir com a tag "Box"
            if (Push.isPlaying)
            {
                Push.Stop(); // Para o som quando a colis�o com a tag "Box" termina
            }
            // Atualiza a posi��o final ao sair da colis�o
            lastPosition = boxObject.transform.position;
        }
    }

    void Update()
    {
        // Verifica o valor do par�metro isWalking no Animator
        bool isWalking = animator.GetBool(isWalkingParameterName);

        if (isWalking)
        {
            // Incrementa o temporizador com o tempo desde a �ltima atualiza��o
            timer += Time.deltaTime;

            // Verifica se o tempo passado � maior ou igual ao intervalo definido
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
            // Reseta o temporizador quando n�o est� andando
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
            // Verifica se a posi��o x da caixa mudou em rela��o � �ltima posi��o registrada
            if (Mathf.Abs(lastPosition.x - boxObject.transform.position.x) > 0f)
            {
                if (!Push.isPlaying) // Verifica se o som n�o est� tocando
                {
                    Push.Play(); // Reproduz o som
                }
            }
            else
            {
                if (Push.isPlaying) // Verifica se o som est� tocando
                {
                    Push.Stop(); // Para o som
                }
            }
        }
    }
}
