using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFInal : MonoBehaviour
{

    public GameObject Block;

    private void Start()
    {
        Block.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que entrou no Trigger tem a tag "Player"
        if (other.CompareTag("Player"))
        {
           Block.SetActive(true);

        }
    }
}
