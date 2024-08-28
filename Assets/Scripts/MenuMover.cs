using UnityEngine;
using UnityEngine.UI;


public class MenuMover : MonoBehaviour
{
    public GameObject botao01;
    public GameObject botao02;
    public GameObject botao03;

    public Button toggleButton; // Botão que alterna a posição dos outros botões
    public Sprite image1; // Primeira imagem para o botão
    public Sprite image2; // Segunda imagem para o botão

    private int currentPosition = 0;
    private bool useFirstImage = true;

    // Definindo as posições para os botões
    private Vector3[] posicoesEsquerda = new Vector3[]
    {
        new Vector3(-350, 30, 0),
        new Vector3(-350, -30, 0),
        new Vector3(-315, 0, 0)
    };

    private Vector3[] posicoesTopo = new Vector3[]
    {
        new Vector3(-40, 170, 0),
        new Vector3(40, 170, 0),
        new Vector3(0, 140, 0)
    };

    void Start()
    {
        // Adiciona o listener ao botão
        toggleButton.onClick.AddListener(OnToggleButtonClick);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePosition();
            ToggleButtonImage();
        }
    }
    void OnToggleButtonClick()
    {
        TogglePosition();
        ToggleButtonImage();
    }

    void TogglePosition()
    {
        currentPosition++;
        if (currentPosition > 1)
            currentPosition = 0;

        MoverBotoes();
    }

    void MoverBotoes()
    {
        switch (currentPosition)
        {
            case 0: // Esquerda
                botao01.transform.localPosition = posicoesEsquerda[0];
                botao02.transform.localPosition = posicoesEsquerda[1];
                botao03.transform.localPosition = posicoesEsquerda[2];
                break;

            case 1: // Topo
                botao01.transform.localPosition = posicoesTopo[0];
                botao02.transform.localPosition = posicoesTopo[1];
                botao03.transform.localPosition = posicoesTopo[2];
                break;
            
        }
    }

    void ToggleButtonImage()
    {
        if (useFirstImage)
        {
            toggleButton.image.sprite = image2;
        }
        else
        {
            toggleButton.image.sprite = image1;
        }
        useFirstImage = !useFirstImage;
    }
}
