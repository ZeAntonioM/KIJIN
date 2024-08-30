using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX, currentPosY;
    private Vector3 velocity = Vector3.zero;

    private void Awake() {

        currentPosX = transform.position.x;
        currentPosY = transform.position.y;

    }

    private void Update() {

        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, currentPosY, transform.position.z), ref velocity, speed);        
    
    }

    public void MoveToNewRoom(Transform _newRoom) {
        currentPosX = _newRoom.position.x;
        currentPosY = _newRoom.position.y;
    }



}
