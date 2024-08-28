using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    
    [SerializeField] private Transform initialCheckpoint;
    [SerializeField] private Transform initialRoom;
    [SerializeField] private Transform Buttons;

    private Transform RespawnRoom;
    private Transform RespawnPoint;
    private ScaleInteraction scaleInteraction;

    public void Awake(){
        RespawnPoint = initialCheckpoint;
        RespawnRoom = initialRoom;
        scaleInteraction = Buttons.GetComponent<ScaleInteraction>();
    }

    public void Respawn(){
        transform.position = RespawnPoint.position;
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        RespawnRoom.GetComponent<Room>().ActivateRoom(false);
        RespawnRoom.GetComponent<Room>().ActivateRoom(true);
        scaleInteraction.Reset();
        
    }

    public void UpdateRespawn (Transform _checkpoint){
        RespawnPoint = _checkpoint;
        RespawnRoom = _checkpoint.parent;
    }  

}
