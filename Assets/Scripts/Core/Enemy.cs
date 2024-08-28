using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            player.GetComponent<PlayerRespawn>().Respawn();
        }
    }


}
