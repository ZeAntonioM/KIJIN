using UnityEngine;
using UnityEngine.UI;

public class UI_LOGO : MonoBehaviour
{
    public Image image;            // Referência ao componente Image da UI
    public Sprite[] sprites;       // Array de sprites que serão usados na animação
    public float frameRate = 0.1f; // Tempo entre cada frame (em segundos)

    private int currentFrame;
    private float timer;

    void Start()
    {
        if (sprites.Length > 0)
        {
            image.sprite = sprites[0];
        }
    }

    void Update()
    {
        if (sprites.Length == 0) return;

        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer -= frameRate;

            // Avançar para o próximo sprite
            currentFrame = (currentFrame + 1) % sprites.Length;

            // Atualizar o sprite do componente Image
            image.sprite = sprites[currentFrame];
        }
    }
}
