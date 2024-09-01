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
        // Aqui voc� pode implementar l�gica para verificar se o bot�o deve ser interativo
        return !Input.GetKey(KeyCode.Space); // Exemplo: evita intera��o se a tecla Space estiver pressionada
    }
}
