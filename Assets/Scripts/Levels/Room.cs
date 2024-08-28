using UnityEngine;

public class Room : MonoBehaviour
{
    
    [SerializeField] private GameObject[] objects;
    private Vector3[] initialPosition;

    private void Awake()
    {

        if (objects == null) return;

        initialPosition = new Vector3[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            //Assim ele vai guardar a posição inicial de cada objeto para o reset
            if (objects[i] != null) initialPosition[i] = objects[i].transform.position;
        }
    }   
    
    public void ActivateRoom(bool _status){

        if (objects == null) return;

        // Ativar ou desativar os objetos da sala, no caso de morte do jogador
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null) {

                objects[i].SetActive(_status);

                if (objects[i].GetComponent<Rigidbody2D>() != null){
                    objects[i].transform.position = initialPosition[i];
                    objects[i].transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    objects[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                }


            }
        }

    }

}
