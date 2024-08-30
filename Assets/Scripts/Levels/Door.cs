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
    [SerializeField] private GameObject doorBlock;

    private Transform nRoom;
    private Transform pRoom;
    private Transform player; 

    private void Awake(){
        nRoom = nextRoom.parent.parent;
        pRoom = previousRoom.parent.parent;
        player = GameObject.FindGameObjectWithTag("Player").transform;
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

                //scaleInteraction.Reset();
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
                
                //scaleInteraction.Reset();
                player.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

                pRoom.GetComponent<Room>().ActivateRoom(true);
                nRoom.GetComponent<Room>().ActivateRoom(false);

                player.GetComponent<PlayerRespawn>().UpdateRespawn(pRoom.Find("Checkpoint").transform);
            }
        }

    }


}
