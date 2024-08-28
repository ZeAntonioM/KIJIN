using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.UIElements;

struct ScaleMode {
    public const string AUMENTAR = "AUMENTAR";
    public const string DIMINUIR = "DIMINUIR";
    public const string NONE = "NONE";
}

public class ScaleInteraction : MonoBehaviour
{

    [Header("Scalling Box")]
    [SerializeField] private Vector3 minBoxScale = new Vector3(0.2f, 0.2f, 0.2f); // Escala m�nima da caixa
    [SerializeField] private Vector3 maxBoxScale = new Vector3(0.5f, 0.5f, 0.5f); // Escala m�xima da caixa
    
    [Header("Scalling Player")]
    [SerializeField] private Vector3 minPlayerScale = new Vector3(0.2f, 0.2f, 0.2f); // Escala m�nima do jogador
    [SerializeField] private Vector3 maxPlayerScale = new Vector3(0.5f, 0.5f, 0.5f); // Escala m�xima do jogador
    
    [Header("Scalling Options")]
    [SerializeField] private float scalingSpeed = 0.001f; // Velocidade de escalamento gradual
    [SerializeField] private Color outlineColor = Color.yellow;
    private bool isScaling;
    private Transform selectedBox;
    private string scaleMode;
    private Transform playerTransform;
    private GameObject outlineAnim;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        selectedBox = null;
        outlineAnim = null;
        scaleMode = ScaleMode.NONE;
    }

    private void Update()
    {
        // Selecionar a Box e iniciar o escalamento ao pressionar o bot�o esquerdo do mouse
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null) {
                if (hit.collider.CompareTag("Box")) {

                    if (selectedBox != null) {
                        Reset();
                    }

                    selectedBox = hit.collider.transform;
                    outlineAnim = selectedBox.transform.GetChild(0).gameObject;

                    outlineAnim.SetActive(true);
                    selectedBox = hit.collider.transform;
                }
            } 
        }

        // Continuar o escalamento enquanto o bot�o do mouse estiver pressionado
        if (isScaling)
        {
            if (scaleMode.Equals(ScaleMode.AUMENTAR)) {
                IncreasePlayerDecreaseBox();
            }
            else if (scaleMode.Equals(ScaleMode.DIMINUIR)) {
                DecreasePlayerIncreaseBox();
            }
            
        }
    }

    private void IncreasePlayerDecreaseBox()
    {
        if (selectedBox != null)
        {

            Vector3 newBoxScale = selectedBox.transform.localScale - new Vector3(scalingSpeed, scalingSpeed, scalingSpeed); // Diminui a Box gradualmente
            Vector3 newPlayerScale = playerTransform.localScale + new Vector3(scalingSpeed, scalingSpeed, scalingSpeed); // Aumenta o Player gradualmente

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
        }
    }

    private void DecreasePlayerIncreaseBox()
    {
        if (selectedBox != null)
        {

            Vector3 newBoxScale = selectedBox.transform.localScale + new Vector3(scalingSpeed, scalingSpeed, scalingSpeed); // Aumenta a Box gradualmente
            Vector3 newPlayerScale = playerTransform.localScale - new Vector3(scalingSpeed, scalingSpeed, scalingSpeed); // Diminui o Player gradualmente

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
        }
    }

    
    public void Aumentar() {
        if (selectedBox != null) {
            if (scaleMode.Equals(ScaleMode.AUMENTAR)) {
                Reset();
            }
            else {
                isScaling = true;
                scaleMode = ScaleMode.AUMENTAR;
            }
        }
    }


    public void Diminuir() {
    
        if (selectedBox != null) {
            if (scaleMode.Equals(ScaleMode.DIMINUIR)) {
                Reset();
            }
            else {
                isScaling = true;
                scaleMode = ScaleMode.DIMINUIR;
            }
        }
    }

    public void Reset() {

            selectedBox = null;
            isScaling = false;
            if (outlineAnim != null) {
                outlineAnim.SetActive(false);
                outlineAnim = null;
            }
            scaleMode = ScaleMode.NONE;
    }



}
