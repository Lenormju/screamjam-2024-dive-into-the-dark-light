using UnityEngine;
using TMPro;

public class keyCollection : MonoBehaviour
{
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
            GameManager.Instance.nb_keys++;
            if (GameManager.Instance.nb_keys > 0)
            {
                key_image.SetActive(true);
            }

            if (other.gameObject.name == "Key1"){
                GameManager.Instance.GotKey1 = true;
            } else if (other.gameObject.name == "Key2"){
                GameManager.Instance.GotKey2 = true;
            } else if (other.gameObject.name == "Key3"){
                GameManager.Instance.GotKey3 = true;
            }
            keys_nb_display.text = GameManager.Instance.nb_keys.ToString();
            Destroy(other.gameObject);
        }
    }
}
