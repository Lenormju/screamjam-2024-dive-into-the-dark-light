using UnityEngine;
using TMPro;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private int nb_keys = 0;
    public TextMeshProUGUI keys_nb_display;
    public GameObject key_image;

    private void Start()
    {
        key_image.SetActive(false);
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.transform.tag == "Key")
        {
            Debug.Log("found a key");
            nb_keys++;
            if (nb_keys > 0)
            {
                key_image.SetActive(true);
            }
            keys_nb_display.text = nb_keys.ToString();
            Destroy(other.gameObject);
        }
    }
}
