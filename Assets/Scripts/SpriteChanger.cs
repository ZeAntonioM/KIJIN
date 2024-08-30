using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Arraste o SpriteRenderer para esse campo no Inspetor
    public Sprite[] sprites; // Arraste seus Sprites aqui no Inspetor
    public float changeInterval = 0.5f; // Intervalo de tempo entre as mudanças

    private int currentSpriteIndex = 0;
    private float timer;

    void Start()
    {
        if (sprites.Length > 0 && spriteRenderer != null)
        {
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }
    }

    void Update()
    {
        if (sprites.Length > 0 && spriteRenderer != null)
        {
            timer += Time.deltaTime;

            if (timer >= changeInterval)
            {
                currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Length;
                spriteRenderer.sprite = sprites[currentSpriteIndex];
                timer = 0f;
            }
        }
    }
}
