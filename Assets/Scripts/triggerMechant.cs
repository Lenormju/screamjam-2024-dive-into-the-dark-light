using UnityEngine;

public class triggerMechant : MonoBehaviour
{
     public GameObject mechant;

    private void Start()
    {
        mechant.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            mechant.SetActive(true);
        }
        GameManager.Instance.final_boss = true;
    }
}
