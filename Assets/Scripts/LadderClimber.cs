using UnityEngine;
using UnityEngine.UI;

public class LadderClimber : MonoBehaviour
{
    public float climbSpeed = 5f;  // Velocidade de subida
    public float climbHeight = 12f;  // Altura que o jogador vai subir
    public SpriteRenderer climbIcon;  // �cone que aparecer� quando o jogador estiver perto da escada

    private bool isNearLadder = false;
    private bool isClimbing = false;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float climbProgress = 0f;

    void Update()
    {
        // Verifica se o jogador est� perto da escada e se pressiona "E"
        if (isNearLadder && Input.GetKeyDown(KeyCode.E) && !isClimbing)
        {
            StartClimbing();
        }

        // Move o jogador enquanto est� subindo
        if (isClimbing)
        {
            Climb();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isNearLadder = true;
            climbIcon.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isNearLadder = false;
            climbIcon.enabled = false;  // Esconde o �cone
        }
    }

    private void StartClimbing()
    {
        isClimbing = true;
        startPosition = transform.position;
        targetPosition = new Vector3(transform.position.x, transform.position.y + climbHeight, transform.position.z);
    }

    private void Climb()
    {
        climbProgress += Time.deltaTime * climbSpeed / climbHeight;

        transform.position = Vector3.Lerp(startPosition, targetPosition, climbProgress);

        if (climbProgress >= 1f)
        {
            isClimbing = false;
            climbProgress = 0f;
        }
    }
}
