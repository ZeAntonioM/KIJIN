using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuSounds : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    // Referência ao AudioSource que tocará os sons
    public AudioSource audioSource;

    // Sons para hover e click
    public AudioClip hoverSound;
    public AudioClip clickSound;

    // Método que toca o som quando o mouse entra no botão
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySound(hoverSound);
    }

    // Método que toca o som quando o botão é clicado
    public void OnPointerClick(PointerEventData eventData)
    {
        PlaySound(clickSound);
    }

    // Método para tocar o som
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

