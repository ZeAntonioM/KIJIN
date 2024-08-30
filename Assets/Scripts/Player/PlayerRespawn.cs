using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    
    private Transform RespawnPoint;
    private Health Health;

    private void Awake() {
        
        Health = GetComponent<Health>();

    }

    public void Respawn(){
        transform.position = RespawnPoint.position;
        Health.Respawn();
    }

    public void UpdateRespawn (Transform _checkpoint){
        RespawnPoint = _checkpoint;
    }  

}
