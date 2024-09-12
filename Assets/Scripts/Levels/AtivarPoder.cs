using UnityEngine;

public class ActivateAttractRepel : MonoBehaviour
{
    // Refer�ncia ao script "AtrairAfastar" que ser� ativado/desativado
    private AtrairAfastar attractRepelScript;

  

    // Collider que ativa o script quando colidido
    public Collider2D activationCollider; // Collider p�blico para definir no Inspector

    void Start()
    {
        // Obt�m o script "AtrairAfastar" que est� anexado ao player
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

    // Ativa o script quando o player entra em um trigger espec�fico
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o collider que colidiu � o collider de ativa��o especificado
        if (other == activationCollider && attractRepelScript != null)
        {
            attractRepelScript.enabled = true;
        }
    }


   
 }

