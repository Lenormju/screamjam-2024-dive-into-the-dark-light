using TMPro.Examples;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    void Awake() {
        _instance = FindFirstObjectByType<GameManager>();
    }

    public Player Player;
    public bool GotKey1 = false;
    public bool GotKey2 = false;
    public bool GotKey3 = false;
    public string currentGroundWalkingCategory = "none by default";  // the audio clip of the noisy groudn we are currently working on
    public GlobalMusic globalMusic;

    void Start() {
        StartAmbientMusic();
    }


    [SerializeField] private Animator deathAnim;
    [SerializeField] private Animator deathAnimSpider;
    public TextMeshProUGUI keys_nb_display;
    public GameObject key_image;

    public bool final_boss = false;

    public void DisplayEndScreen()
    {
        Debug.Log("fin du  game");
        if (final_boss)
        {
            deathAnimSpider.gameObject.SetActive(true);
            deathAnim.gameObject.SetActive(false);
            deathAnimSpider.SetTrigger("displayGameOver");
        }
        else
        {
            deathAnimSpider.gameObject.SetActive(false);
            deathAnim.gameObject.SetActive(true);
            deathAnim.SetTrigger("displayGameOver");
        }
        SetCursorActive(true);
    }

    public void AddKey(int NbKeysToAdd = 1)
    {
        nb_keys += NbKeysToAdd;
        SetNbKey(nb_keys);
    }

     public void RemoveKey(int NbKeysToRemove = 1)
    {
        nb_keys -= NbKeysToRemove;
        SetNbKey(nb_keys);
    }

    public void SetNbKey(int nbKey)
    {
        nb_keys = nbKey;
        if (nbKey > 0)
        {
            key_image.SetActive(true);
            keys_nb_display.text = nbKey.ToString();
        }
        else
        {
            key_image.SetActive(false);
            keys_nb_display.text = "";
        }
    }
    public int nb_keys = 0;

    public void SetCursorActive(bool active)
    {
        if (!active) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }

    public void StartAmbientMusic() {
       globalMusic.StartAmbientMusic();
    }

    public void StartRunningMusic() {
        globalMusic.StartRunningMusic();
    }

    public void StartBangingDoorMusic() {
        globalMusic.StartBangingDoorMusic();
    }

}
