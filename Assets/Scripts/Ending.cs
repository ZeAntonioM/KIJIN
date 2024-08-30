using UnityEngine;

public class Ending : MonoBehaviour
{
    // Referência ao painel de UI que queremos ativar
    public GameObject uiPanel; // Arraste o painel de UI aqui no Inspector.

    private void Start()
    {
        uiPanel.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que entrou no Trigger tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            
            // Tenta pegar o componente PlayerMovement do Player
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            // Se o PlayerMovement for encontrado, desativa o script
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }

            // Ativa o painel de UI
            if (uiPanel != null)
            {
                uiPanel.SetActive(true);
            }
        }
    }
}

