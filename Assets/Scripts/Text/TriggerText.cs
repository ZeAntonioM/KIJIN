using UnityEngine;
using TMPro;

public class TriggerText : MonoBehaviour
{
    public TextMeshPro textMesh;  // Arraste o TextMeshPro (não UI) para esse campo no inspetor.
    public float fadeDuration = 1.0f;  // Tempo para aparecer/desaparecer

    private bool isPlayerInside = false;
    private float fadeAmount = 0f;
    private Color originalColor;

    void Start()
    {
        if (textMesh != null)
        {
            originalColor = textMesh.color;
            textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        }
    }

    void Update()
    {
        if (textMesh != null)
        {
            if (isPlayerInside)
            {
                if (fadeAmount < 1)
                {
                    fadeAmount += Time.deltaTime / fadeDuration;
                    textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, fadeAmount);
                }
            }
            else
            {
                if (fadeAmount > 0)
                {
                    fadeAmount -= Time.deltaTime / fadeDuration;
                    textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, fadeAmount);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}
