using UnityEngine;

public class KillTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.DisplayEndScreen();
        }
    }
}
