using UnityEngine;
using UnityEngine.UI;

public class ControlarSom : MonoBehaviour
{
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Image buttonImage;
    public Button toggleButton;

    private AudioSource[] audioSources;
    private bool isMuted = false;

    void Start()
    {
        audioSources = FindObjectsOfType<AudioSource>();
        UpdateSprite();
    }

    public void ToggleMute()
    {
        if (!IsButtonInteractable()) return;

        isMuted = !isMuted;

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = isMuted;
        }

        UpdateSprite();
    }

    private void UpdateSprite()
    {
        buttonImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
    }

    private bool IsButtonInteractable()
    {
        // Aqui você pode implementar lógica para verificar se o botão deve ser interativo
        return !Input.GetKey(KeyCode.Space); // Exemplo: evita interação se a tecla Space estiver pressionada
    }
}
