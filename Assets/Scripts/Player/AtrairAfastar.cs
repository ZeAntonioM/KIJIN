using Unity.VisualScripting;
using UnityEngine;

public class AtrairAfastar : MonoBehaviour
{
    // Velocidade de movimenta��o das caixas
    public float moveSpeed = 2f;
    // Tag dos objetos que ser�o afetados
    public string targetTag = "Box";
    // Teclas para ativar as fun��es
    public KeyCode attractKey = KeyCode.E;
    public KeyCode repelKey = KeyCode.F;
    // Objetos que aparecer�o ao pressionar as teclas F e E
    public GameObject objectForF; // Objeto para a tecla F
    public GameObject objectForE; // Objeto para a tecla E
    [SerializeField] private AudioClip PULL;
    [SerializeField] private AudioClip PUSH;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Certifica-se de que os objetos est�o desativados no in�cio
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
        // Verifica se a tecla de atra��o ou repuls�o est� pressionada
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
        
      /*  // Verifica se a tecla F est� pressionada
        if (Input.GetKey(KeyCode.F) && objectForF != null)
        {
            objectForF.SetActive(true);
            if (!audioSource.isPlaying) // Verifica se o som j� est� tocando
            {
                audioSource.PlayOneShot(PUSH); // Toca o som de PUSH
            }
        }
        else if (Input.GetKeyUp(KeyCode.F) && objectForF != null)
        {
            objectForF.SetActive(false);
        }

        // Verifica se a tecla E est� pressionada
        if (Input.GetKey(KeyCode.E) && objectForE != null)
        {
            objectForE.SetActive(true);
            if (!audioSource.isPlaying) // Verifica se o som j� est� tocando
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

    // Fun��o que move os objetos em dire��o ou para longe do player gradualmente
    void MoveBoxesTowardsPlayer(bool attract)
    {
        // Encontra todos os objetos com a tag especificada
        GameObject[] boxes = GameObject.FindGameObjectsWithTag(targetTag);

        // Itera sobre cada objeto encontrado
        foreach (GameObject box in boxes)
        {
            // Calcula a dire��o do player para o objeto
            Vector3 directionToPlayer = (transform.position - box.transform.position).normalized;

            // Se estiver atraindo, move na dire��o do player, caso contr�rio, afasta
            Vector3 movement = attract ?
                directionToPlayer * moveSpeed * Time.deltaTime :
                -directionToPlayer * moveSpeed * Time.deltaTime;

            // Atualiza a posi��o do objeto de forma suave
            box.transform.position += movement;
        }
    }
}
