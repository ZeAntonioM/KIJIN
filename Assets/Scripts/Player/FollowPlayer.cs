using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;  // Refer�ncia ao jogador
    public float followSpeed = 2.0f;  // Velocidade que os olhos seguem o jogador
    public float minX = -1.0f;  // Limite esquerdo para os olhos
    public float maxX = 1.0f;   // Limite direito para os olhos

    private Vector3 initialPosition;  // Posi��o inicial dos olhos

    void Start()
    {
        // Armazenar a posi��o inicial dos olhos
        initialPosition = transform.position;
    }

    void Update()
    {
        // Verificar se o jogador est� abaixo dos olhos
        if (player.position.y < initialPosition.y)
        {
            // Calcular a nova posi��o x dos olhos com base na posi��o x do jogador
            float targetX = player.position.x;

            // Limitar a posi��o x dos olhos aos valores minX e maxX
            targetX = Mathf.Clamp(targetX, initialPosition.x + minX, initialPosition.x + maxX);

            // Mover os olhos suavemente em dire��o ao targetX
            Vector3 newPosition = new Vector3(targetX, initialPosition.y, initialPosition.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);
        }
    }
}
