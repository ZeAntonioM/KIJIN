using UnityEngine;
using System.Collections;

struct ScaleMode
{
    public const string AUMENTAR = "AUMENTAR";
    public const string DIMINUIR = "DIMINUIR";
    public const string NONE = "NONE";
}

public class ScaleInteraction : MonoBehaviour
{
    [Header("Scalling Box")]
    [SerializeField] private Vector3 minBoxScale = new Vector3(0.2f, 0.2f, 0.2f); // Escala mínima da caixa
    [SerializeField] private Vector3 maxBoxScale = new Vector3(0.5f, 0.5f, 0.5f); // Escala máxima da caixa

    [Header("Scalling Player")]
    [SerializeField] private Vector3 minPlayerScale = new Vector3(0.2f, 0.2f, 0.2f); // Escala mínima do jogador
    [SerializeField] private Vector3 maxPlayerScale = new Vector3(0.5f, 0.5f, 0.5f); // Escala máxima do jogador

    [Header("Scalling Options")]
    [SerializeField] private float scalingSpeed = 0.001f; // Velocidade de escalamento gradual
    [SerializeField] private AudioClip scaleUpSound; // Som ao aumentar
    [SerializeField] private AudioClip scaleDownSound; // Som ao diminuir
    [SerializeField] private AudioClip scaleLimitReachedClip; // Som quando o limite de escala é atingido
    [SerializeField] private AudioClip boxMoveSound; // Som quando a caixa se move no eixo X
    private AudioSource audioSource;

    private Transform selectedBox;
    private string scaleMode;
    private Transform playerTransform;
    private GameObject outlineAnim;
    private Vector3 lastBoxPosition;
    private bool hasPlayedScaleUpSound;
    private bool hasPlayedScaleDownSound;
    private bool isBoxMoving;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        selectedBox = null;
        outlineAnim = null;
        scaleMode = ScaleMode.NONE;
        audioSource = GetComponent<AudioSource>();
        lastBoxPosition = Vector3.zero;
        isBoxMoving = false;
    }

    private void Update()
    {
        // Verificar se o mouse está sobre uma caixa
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Box"))
        {
            // Se o mouse está sobre uma caixa, ativar o outline
            if (selectedBox == null || selectedBox != hit.collider.transform)
            {
                selectedBox = hit.collider.transform;
                outlineAnim = selectedBox.transform.GetChild(0).gameObject;
                outlineAnim.SetActive(true);
                lastBoxPosition = selectedBox.position; // Inicializar a última posição da caixa
            }

            // Detectar se o botão esquerdo ou direito está pressionado para aumentar ou diminuir a caixa
            if (Input.GetMouseButtonDown(0))
            {
                if (CanScale(ScaleMode.AUMENTAR))
                {
                    scaleMode = ScaleMode.AUMENTAR;
                    PlaySound(scaleUpSound);
                    hasPlayedScaleUpSound = true;
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (CanScale(ScaleMode.DIMINUIR))
                {
                    scaleMode = ScaleMode.DIMINUIR;
                    PlaySound(scaleDownSound);
                    hasPlayedScaleDownSound = true;
                }
            }
            else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) // Parar o escalamento quando o botão é solto
            {
                scaleMode = ScaleMode.NONE;
                hasPlayedScaleUpSound = false;
                hasPlayedScaleDownSound = false;
            }

            // Continuar o escalamento enquanto o botão do mouse estiver pressionado
            if (scaleMode.Equals(ScaleMode.AUMENTAR))
            {
                IncreasePlayerDecreaseBox();
            }
            else if (scaleMode.Equals(ScaleMode.DIMINUIR))
            {
                DecreasePlayerIncreaseBox();
            }

            // Verificar movimento da caixa no eixo X e tocar som
            if (selectedBox.position.x != lastBoxPosition.x)
            {
                // Verificar colisão com o player
                Collider2D playerCollider = playerTransform.GetComponent<Collider2D>();
                Collider2D boxCollider = selectedBox.GetComponent<Collider2D>();
                if (boxCollider.IsTouching(playerCollider))
                {
                    if (!isBoxMoving)
                    {
                        PlayMovementSound(boxMoveSound);
                        isBoxMoving = true;
                    }
                }
            }
            else
            {
                // Se a caixa não está se movendo no eixo X, parar o som
                if (isBoxMoving)
                {
                    StopMovementSound();
                    isBoxMoving = false;
                }
            }

            lastBoxPosition = selectedBox.position; // Atualizar a última posição da caixa
        }
        else
        {
            // Se o mouse não está sobre uma caixa, desativar o outline
            Reset();
        }
    }

    private bool CanScale(string mode)
    {
        if (mode == ScaleMode.AUMENTAR)
        {
            // Verificar se a escala da personagem está no máximo ou se a caixa já está no mínimo
            if (playerTransform.localScale.x >= maxPlayerScale.x || selectedBox.localScale.x <= minBoxScale.x)
            {
                PlayScaleLimitSound();
                return false;
            }
        }
        else if (mode == ScaleMode.DIMINUIR)
        {
            // Verificar se a escala da personagem está no mínimo ou se a caixa já está no máximo
            if (playerTransform.localScale.x <= minPlayerScale.x || selectedBox.localScale.x >= maxBoxScale.x)
            {
                PlayScaleLimitSound();
                return false;
            }
        }

        return true;
    }

    private void IncreasePlayerDecreaseBox()
    {
        if (selectedBox != null)
        {
            Vector3 newBoxScale = selectedBox.transform.localScale - new Vector3(scalingSpeed, scalingSpeed, scalingSpeed); // Diminui a Box gradualmente
            Vector3 newPlayerScale = playerTransform.localScale + new Vector3(scalingSpeed, scalingSpeed, scalingSpeed); // Aumenta o Player gradualmente

            bool limitReached = false;

            if (newBoxScale.x <= minBoxScale.x || newPlayerScale.x >= maxPlayerScale.x)
            {
                limitReached = true;
            }

            newBoxScale = new Vector3(
                Mathf.Clamp(newBoxScale.x, minBoxScale.x, maxBoxScale.x),
                Mathf.Clamp(newBoxScale.y, minBoxScale.y, maxBoxScale.y),
                Mathf.Clamp(newBoxScale.z, minBoxScale.z, maxBoxScale.z)
            );

            newPlayerScale = new Vector3(
                Mathf.Clamp(newPlayerScale.x, minPlayerScale.x, maxPlayerScale.x),
                Mathf.Clamp(newPlayerScale.y, minPlayerScale.y, maxPlayerScale.y),
                Mathf.Clamp(newPlayerScale.z, minPlayerScale.z, maxPlayerScale.z)
            );

            selectedBox.transform.localScale = newBoxScale;
            playerTransform.localScale = newPlayerScale;

            if (limitReached)
            {
                PlayScaleLimitSound();
                scaleMode = ScaleMode.NONE;
            }
        }
    }

    private void DecreasePlayerIncreaseBox()
    {
        if (selectedBox != null)
        {
            Vector3 newBoxScale = selectedBox.transform.localScale + new Vector3(scalingSpeed, scalingSpeed, scalingSpeed); // Aumenta a Box gradualmente
            Vector3 newPlayerScale = playerTransform.localScale - new Vector3(scalingSpeed, scalingSpeed, scalingSpeed); // Diminui o Player gradualmente

            bool limitReached = false;

            if (newBoxScale.x >= maxBoxScale.x || newPlayerScale.x <= minPlayerScale.x)
            {
                limitReached = true;
            }

            newBoxScale = new Vector3(
                Mathf.Clamp(newBoxScale.x, minBoxScale.x, maxBoxScale.x),
                Mathf.Clamp(newBoxScale.y, minBoxScale.y, maxBoxScale.y),
                Mathf.Clamp(newBoxScale.z, minBoxScale.z, maxBoxScale.z)
            );

            newPlayerScale = new Vector3(
                Mathf.Clamp(newPlayerScale.x, minPlayerScale.x, maxPlayerScale.x),
                Mathf.Clamp(newPlayerScale.y, minPlayerScale.y, maxPlayerScale.y),
                Mathf.Clamp(newPlayerScale.z, minPlayerScale.z, maxPlayerScale.z)
            );

            selectedBox.transform.localScale = newBoxScale;
            playerTransform.localScale = newPlayerScale;

            if (limitReached)
            {
                PlayScaleLimitSound();
                scaleMode = ScaleMode.NONE;
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            // Parar qualquer som que esteja tocando para evitar sobreposição
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private void PlayMovementSound(AudioClip clip)
    {
        if (audioSource != null && clip != null && !audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.loop = false;
            audioSource.Play();
        }
    }

    private void StopMovementSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void PlayScaleLimitSound()
    {
        if (audioSource != null && scaleLimitReachedClip != null)
        {
            // Parar qualquer som que esteja tocando para evitar sobreposição
            audioSource.Stop();
            audioSource.clip = scaleLimitReachedClip;
            audioSource.Play();
        }
    }

    public void Reset()
    {
        if (selectedBox != null)
        {
            if (outlineAnim != null)
            {
                outlineAnim.SetActive(false);
                outlineAnim = null;
            }
            selectedBox = null;
        }
        scaleMode = ScaleMode.NONE;
        StopMovementSound(); // Parar som de movimento quando resetar
    }
}