using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;  // Referência ao jogador
    public float followSpeed = 2.0f;  // Velocidade que os olhos seguem o jogador
    public float minX = -1.0f;  // Limite esquerdo para os olhos
    public float maxX = 1.0f;   // Limite direito para os olhos

    private Vector3 initialPosition;  // Posição inicial dos olhos

    void Start()
    {
        // Armazenar a posição inicial dos olhos
        initialPosition = transform.position;
    }

    void Update()
    {
        // Verificar se o jogador está abaixo dos olhos
        if (player.position.y < initialPosition.y)
        {
            // Calcular a nova posição x dos olhos com base na posição x do jogador
            float targetX = player.position.x;

            // Limitar a posição x dos olhos aos valores minX e maxX
            targetX = Mathf.Clamp(targetX, initialPosition.x + minX, initialPosition.x + maxX);

            // Mover os olhos suavemente em direção ao targetX
            Vector3 newPosition = new Vector3(targetX, initialPosition.y, initialPosition.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);
        }
    }
}
