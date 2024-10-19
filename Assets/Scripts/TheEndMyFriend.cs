using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEndMyFriend : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.transform.tag == "Player")
        {
            SceneManager.LoadScene("FinalScene", LoadSceneMode.Single);
        }
    }
}
