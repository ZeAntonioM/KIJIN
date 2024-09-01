using Unity.VisualScripting;
using UnityEngine;

public class AtrairAfastar : MonoBehaviour
{
    // Velocidade de movimentação das caixas
    public float moveSpeed = 2f;
    // Tag dos objetos que serão afetados
    public string targetTag = "Box";
    // Teclas para ativar as funções
    public KeyCode attractKey = KeyCode.E;
    public KeyCode repelKey = KeyCode.F;
    // Objetos que aparecerão ao pressionar as teclas F e E
    public GameObject objectForF; // Objeto para a tecla F
    public GameObject objectForE; // Objeto para a tecla E
    [SerializeField] private AudioClip PULL;
    [SerializeField] private AudioClip PUSH;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Certifica-se de que os objetos estão desativados no início
        if (objectForF != null)
        {
            objectForF.SetActive(false);
        }

        if (objectForE != null)
        {
            objectForE.SetActive(false);
        }
    }

    void Update()
    {
        // Verifica se a tecla de atração ou repulsão está pressionada
        if (Input.GetKey(attractKey))
        {
            
            MoveBoxesTowardsPlayer(true); // Atrai gradualmente
          //  audioSource.PlayOneShot(PULL); // Toca o som de PULL
            objectForE.SetActive(true);
        }
        else if (Input.GetKey(repelKey))
        {
           
            MoveBoxesTowardsPlayer(false); // Afasta gradualmente
            objectForF.SetActive(true);
           // audioSource.PlayOneShot(PUSH); // Toca o som de PULL
        }
        
      /*  // Verifica se a tecla F está pressionada
        if (Input.GetKey(KeyCode.F) && objectForF != null)
        {
            objectForF.SetActive(true);
            if (!audioSource.isPlaying) // Verifica se o som já está tocando
            {
                audioSource.PlayOneShot(PUSH); // Toca o som de PUSH
            }
        }
        else if (Input.GetKeyUp(KeyCode.F) && objectForF != null)
        {
            objectForF.SetActive(false);
        }

        // Verifica se a tecla E está pressionada
        if (Input.GetKey(KeyCode.E) && objectForE != null)
        {
            objectForE.SetActive(true);
            if (!audioSource.isPlaying) // Verifica se o som já está tocando
            {
                audioSource.PlayOneShot(PULL); // Toca o som de PULL
            }
        }
        else if (Input.GetKeyUp(KeyCode.E) && objectForE != null)
        {
            objectForE.SetActive(false);
        } */
        else { 

       // audioSource.Stop();
          objectForE.SetActive(false);
            objectForF.SetActive(false);
        }
    }

    // Função que move os objetos em direção ou para longe do player gradualmente
    void MoveBoxesTowardsPlayer(bool attract)
    {
        // Encontra todos os objetos com a tag especificada
        GameObject[] boxes = GameObject.FindGameObjectsWithTag(targetTag);

        // Itera sobre cada objeto encontrado
        foreach (GameObject box in boxes)
        {
            // Calcula a direção do player para o objeto
            Vector3 directionToPlayer = (transform.position - box.transform.position).normalized;

            // Se estiver atraindo, move na direção do player, caso contrário, afasta
            Vector3 movement = attract ?
                directionToPlayer * moveSpeed * Time.deltaTime :
                -directionToPlayer * moveSpeed * Time.deltaTime;

            // Atualiza a posição do objeto de forma suave
            box.transform.position += movement;
        }
    }
}
