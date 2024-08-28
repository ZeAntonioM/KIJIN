using UnityEngine;

public class TimerEnd : MonoBehaviour
{

    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            timer.GetComponent<Timer>().StopTimer();
        }
    }




}
