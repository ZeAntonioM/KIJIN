using UnityEngine;

struct ScaleMode
{
    public const string AUMENTAR = "AUMENTAR";
    public const string DIMINUIR = "DIMINUIR";
    public const string NONE = "NONE";
}

public class ScaleInteraction : MonoBehaviour
{
    [Header("Scalling Box")]
    [SerializeField] private Vector3 minBoxScale = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private Vector3 maxBoxScale = new Vector3(0.5f, 0.5f, 0.5f);

    [Header("Scalling Player")]
    [SerializeField] private Vector3 minPlayerScale = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private Vector3 maxPlayerScale = new Vector3(0.5f, 0.5f, 0.5f);

    [Header("Scalling Options")]
    [SerializeField] private float scalingSpeed = 0.001f;
    [SerializeField] private AudioClip scaleUpSound;
    [SerializeField] private AudioClip scaleDownSound;
    [SerializeField] private AudioClip scaleLimitReachedClip;
    [SerializeField] private AudioClip selectedBoxSound;
   // public GameObject empurrarsom;

    private AudioSource audioSource;
    private Transform playerTransform;
    private Transform selectedBox;
    private GameObject outlineAnim;
    private Vector3 lastBoxPosition;
    private bool isBoxMoving;
    private bool hasPlayedSlideSound;
    private string scaleMode;

    private Collider2D boxCollider;
    private Collider2D playerCollider;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<Collider2D>();

        if (playerTransform != null)
            playerCollider = playerTransform.GetComponent<Collider2D>();

        ResetState();
    }

    private void Update()
    {
        DetectBoxUnderMouse(); // Detecta a caixa sob o cursor e gerencia a seleção
        HandleScalingInput();  // Lida com a lógica de escalonamento quando a caixa está selecionada
       // HandleBoxMovement();   // Lida com a movimentação e reprodução do som de empurrar
    }

    private void DetectBoxUnderMouse()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Box"))
        {
            if (selectedBox == null || selectedBox != hit.collider.transform)
            {
                SelectBox(hit.collider.transform);
            }
        }
        else
        {
            DeselectBox();
        }
    }

    private void SelectBox(Transform box)
    {
        selectedBox = box;
        outlineAnim = selectedBox.GetChild(0).gameObject;
        outlineAnim.SetActive(true);
        PlaySound(selectedBoxSound);
    }

    private void DeselectBox()
    {
        if (selectedBox != null)
        {
            outlineAnim.SetActive(false);
            selectedBox = null;
        }
    }

    private void HandleScalingInput()
    {
        if (selectedBox == null) return;

        if (Input.GetMouseButtonDown(0) && CanScale(ScaleMode.AUMENTAR))
        {
            StartScaling(ScaleMode.AUMENTAR, scaleUpSound);
        }
        else if (Input.GetMouseButtonDown(1) && CanScale(ScaleMode.DIMINUIR))
        {
            StartScaling(ScaleMode.DIMINUIR, scaleDownSound);
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            StopScaling();
        }

        if (scaleMode.Equals(ScaleMode.AUMENTAR))
        {
            IncreasePlayerDecreaseBox();
        }
        else if (scaleMode.Equals(ScaleMode.DIMINUIR))
        {
            DecreasePlayerIncreaseBox();
        }
    }

    private void StartScaling(string mode, AudioClip sound)
    {
        scaleMode = mode;
        PlaySound(sound);
    }

    private void StopScaling()
    {
        scaleMode = ScaleMode.NONE;
    }

   /* private void HandleBoxMovement()
    {
        bool isMovingX = Mathf.Abs(transform.position.x - lastBoxPosition.x) > 0.01f;

        if (isMovingX && IsPlayerTouchingBox())
        {
            
                empurrarsom.SetActive(true);
           
        }
        else
        {
          
                empurrarsom.SetActive(false);
            }

        lastBoxPosition = transform.position;
    }

    private bool IsPlayerTouchingBox()
    {
        if (boxCollider != null && playerCollider != null)
        {
            return boxCollider.IsTouching(playerCollider);

        }
        return false;
    } */

    private bool CanScale(string mode)
    {
        Vector3 targetScale = mode == ScaleMode.AUMENTAR
            ? selectedBox.localScale + Vector3.one * scalingSpeed
            : selectedBox.localScale - Vector3.one * scalingSpeed;

        bool withinMinLimit = targetScale.x >= minBoxScale.x - 0.01f;
        bool withinMaxLimit = targetScale.x <= maxBoxScale.x + 0.01f;

        return withinMinLimit && withinMaxLimit;
    }

    private void IncreasePlayerDecreaseBox()
    {
        Vector3 newBoxScale = Vector3.Max(minBoxScale, selectedBox.localScale - Vector3.one * scalingSpeed);
        if (newBoxScale == selectedBox.localScale)
        {
            PlaySound(scaleLimitReachedClip);
        }
        selectedBox.localScale = newBoxScale;
        playerTransform.localScale = Vector3.Min(maxPlayerScale, playerTransform.localScale + Vector3.one * scalingSpeed);
    }

    private void DecreasePlayerIncreaseBox()
    {
        Vector3 newBoxScale = Vector3.Min(maxBoxScale, selectedBox.localScale + Vector3.one * scalingSpeed);
        if (newBoxScale == selectedBox.localScale)
        {
            PlaySound(scaleLimitReachedClip);
        }
        selectedBox.localScale = newBoxScale;
        playerTransform.localScale = Vector3.Max(minPlayerScale, playerTransform.localScale - Vector3.one * scalingSpeed);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void ResetState()
    {
        outlineAnim = null;
        lastBoxPosition = transform.position;
        isBoxMoving = false;
        hasPlayedSlideSound = false;
        scaleMode = ScaleMode.NONE;
    }
}
