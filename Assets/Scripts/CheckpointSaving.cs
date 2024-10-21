using TMPro.Examples;
using UnityEngine;

public class CheckpointSaving : MonoBehaviour
{
    private bool hasKey1 = false;
    private bool hasKey2 = false;
    private bool hasKey3 = false;

    private bool gotAllKeys = false;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ConfigureGameManagerWhenRespawning()
    {

        int keysNb = 0;
        if (hasKey1 || gotAllKeys) { Destroy(GameManager.Instance.key1); keysNb++; }
        if (hasKey2 || gotAllKeys) { Destroy(GameManager.Instance.key2); keysNb++; }
        if (hasKey3 || gotAllKeys) { Destroy(GameManager.Instance.key3); keysNb++; }

        GameManager.Instance.GotKey1 = hasKey1 || gotAllKeys;
        GameManager.Instance.GotKey2 = hasKey1 || gotAllKeys;
        GameManager.Instance.GotKey3 = hasKey1 || gotAllKeys;

        GameManager.Instance.nb_keys = keysNb;
        GameManager.Instance.SetNbKey(keysNb);
        Debug.Log("respawned with keynb =" + keysNb);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GameManager.Instance.GotKey1) hasKey1 = true;
            if (GameManager.Instance.GotKey2) hasKey2 = true;
            if (GameManager.Instance.GotKey3) hasKey3 = true;
            GameManager.Instance.UpdateKeyNb();

            if (GameManager.Instance.nb_keys == 3) gotAllKeys = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GameManager.Instance.GotKey1) hasKey1 = true;
            if (GameManager.Instance.GotKey2) hasKey2 = true;
            if (GameManager.Instance.GotKey3) hasKey3 = true;
            GameManager.Instance.UpdateKeyNb();

            if (GameManager.Instance.nb_keys == 3) gotAllKeys = true;

        }
    }
}
