using UnityEngine;

public class ActivateAttractRepel : MonoBehaviour
{
    // Referência ao script "AtrairAfastar" que será ativado/desativado
    private AtrairAfastar attractRepelScript;

  

    // Collider que ativa o script quando colidido
    public Collider2D activationCollider; // Collider público para definir no Inspector

    void Start()
    {
        // Obtém o script "AtrairAfastar" que está anexado ao player
        attractRepelScript = GetComponent<AtrairAfastar>();

        // Desativa o script inicialmente
        if (attractRepelScript != null)
        {
            attractRepelScript.enabled = false;
        }

      
    }

    void Update()
    {
       
    }

    // Ativa o script quando o player entra em um trigger específico
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o collider que colidiu é o collider de ativação especificado
        if (other == activationCollider && attractRepelScript != null)
        {
            attractRepelScript.enabled = true;
        }
    }


   
 }

