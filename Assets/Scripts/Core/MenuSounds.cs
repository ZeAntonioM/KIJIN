using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuSounds : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    // Refer�ncia ao AudioSource que tocar� os sons
    public AudioSource audioSource;

    // Sons para hover e click
    public AudioClip hoverSound;
    public AudioClip clickSound;

    // M�todo que toca o som quando o mouse entra no bot�o
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySound(hoverSound);
    }

    // M�todo que toca o som quando o bot�o � clicado
    public void OnPointerClick(PointerEventData eventData)
    {
        PlaySound(clickSound);
    }

    // M�todo para tocar o som
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

