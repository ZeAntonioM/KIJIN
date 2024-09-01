using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] dialogueLines;
    public float typingSpeed = 0.05f;
    public AudioSource typingSound;
    public float fadeOutDuration = 1.5f;
    public Image continueIcon;  // Referência ao ícone de continuar

    private int index;
    private bool isTyping;
    private bool isSkipping;
    private PlayerMovement playerMovement;
    private CanvasGroup canvasGroup;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>(); // Encontra o script de movimento do jogador
        playerMovement.enabled = false; // Desativa o movimento do jogador

        canvasGroup = GetComponent<CanvasGroup>(); // Obtém o CanvasGroup para o fade out

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 1; // Garante que o painel comece totalmente visível

        if (continueIcon != null)
        {
            continueIcon.color = new Color(continueIcon.color.r, continueIcon.color.g, continueIcon.color.b, 0); // Deixa o ícone invisível no início
        }

        StartCoroutine(TypeLine());
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !isTyping)
        {
            NextLine();
        }
        else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isTyping)
        {
            isSkipping = true;
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in dialogueLines[index].ToCharArray())
        {
            if (isSkipping)
            {
                dialogueText.text = dialogueLines[index];
                break;
            }

            dialogueText.text += letter;

            if (typingSound != null)
            {
                typingSound.Play(); // Toca o som durante a digitação
            }

            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        isSkipping = false;

        if (continueIcon != null)
        {
            StartCoroutine(BlinkContinueIcon()); // Inicia a animação do ícone
        }
    }

    IEnumerator BlinkContinueIcon()
    {
        while (!Input.GetKeyDown(KeyCode.Space) && !Input.GetMouseButtonDown(0))
        {
            // Faz o alpha do ícone oscilar
            continueIcon.color = new Color(continueIcon.color.r, continueIcon.color.g, continueIcon.color.b, Mathf.PingPong(Time.time, 1f));
            yield return null;
        }

        // Quando o jogador aperta para continuar, reseta o alpha do ícone
        continueIcon.color = new Color(continueIcon.color.r, continueIcon.color.g, continueIcon.color.b, 0);
    }

    void NextLine()
    {
        if (index < dialogueLines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueText.text = "";
            playerMovement.enabled = true; // Reativa o movimento do jogador
            StartCoroutine(FadeOutAndDeactivate()); // Inicia o fade out do painel
        }
    }

    IEnumerator FadeOutAndDeactivate()
    {
        float startAlpha = canvasGroup.alpha;
        float rate = 0.7f / fadeOutDuration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        gameObject.SetActive(false); // Desativa o painel de diálogo após o fade out
    }
}
