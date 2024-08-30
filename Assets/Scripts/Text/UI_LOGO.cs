using UnityEngine;
using UnityEngine.UI;

public class UI_LOGO : MonoBehaviour
{
    public Image image;            // Refer�ncia ao componente Image da UI
    public Sprite[] sprites;       // Array de sprites que ser�o usados na anima��o
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

            // Avan�ar para o pr�ximo sprite
            currentFrame = (currentFrame + 1) % sprites.Length;

            // Atualizar o sprite do componente Image
            image.sprite = sprites[currentFrame];
        }
    }
}
