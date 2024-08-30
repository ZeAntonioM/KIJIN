using Unity.VisualScripting;
using UnityEngine;

enum DoorType{
    Left,
    Right,
    Up
}

public class Door : MonoBehaviour
{

    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private Transform cam;
    [SerializeField] private DoorType doorType;
    [SerializeField] private Transform scallingButtons;
    [SerializeField] private GameObject doorBlock;

    private ScaleInteraction scaleInteraction;
    private Transform nRoom;
    private Transform pRoom;
    private Transform player; 

    private void Awake(){
        nRoom = nextRoom.parent.parent;
        pRoom = previousRoom.parent.parent;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        scaleInteraction = scallingButtons.GetComponent<ScaleInteraction>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Player") {

            CameraController cameraController = cam.GetComponent<CameraController>();

            if (
                ((doorType == DoorType.Right) && (collision.transform.position.x < transform.position.x)) || 
                ((doorType == DoorType.Left) && (collision.transform.position.x > transform.position.x)) || 
                ((doorType == DoorType.Up) && (collision.transform.position.y < (transform.position.y-2)))                
                ) {
                cameraController.MoveToNewRoom(nextRoom);

                Debug.Log("Posição y da colisão: " + collision.transform.position.y);
                Debug.Log("Posição y da porta: " + transform.position.y);

                scaleInteraction.Reset();
                player.GetComponent<PlayerMovement>().Reset();

                if((doorType == DoorType.Up) && (doorBlock != null)){
                    doorBlock.SetActive(true);
                    player.transform.position = nRoom.Find("Checkpoint").transform.position;
                }

                nRoom.GetComponent<Room>().ActivateRoom(true);
                pRoom.GetComponent<Room>().ActivateRoom(false);

                player.GetComponent<PlayerRespawn>().UpdateRespawn(nRoom.Find("Checkpoint").transform);
            } 
            else{

                cameraController.MoveToNewRoom(previousRoom);
                
                scaleInteraction.Reset();
                player.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

                pRoom.GetComponent<Room>().ActivateRoom(true);
                nRoom.GetComponent<Room>().ActivateRoom(false);

                player.GetComponent<PlayerRespawn>().UpdateRespawn(pRoom.Find("Checkpoint").transform);
            }
        }

    }


}
