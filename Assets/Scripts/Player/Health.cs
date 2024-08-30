using UnityEngine;

public class Health : MonoBehaviour
{
    
    [SerializeField] private float startingHealth;
    public float health { get; private set; }
    private bool isDead; // Adicionar uma variável para verificar se o jogador está morto
    private Animator animator;

    private void Awake()
    {
        health = startingHealth;
        //animator = GetComponent<Animator>();	
    }

    public void Heal(float _heal)
    {
        health = Mathf.Clamp(health + _heal, 0, startingHealth);
    }

    public void Respawn()
    {
        isDead = false;
        Heal(startingHealth);
        animator.ResetTrigger("Die");
        animator.Play("Idle");
    }

    public void TakeDamage(int _damage)
    {

        health = Mathf.Clamp(health - _damage, 0, startingHealth);

        if (health == 0)
        {
            if (!isDead) // Verificar se o jogador já está morto
            {   
                //animator.Play("Die");
                isDead = true;
                GetComponent<PlayerMovement>().enabled = false;
            }
            
        }
    }

}
