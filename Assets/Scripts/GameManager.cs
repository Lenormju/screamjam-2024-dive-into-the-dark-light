using System;
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

    public GameObject key1;
    public GameObject key2;
    public GameObject key3;

    public void InvokeKey2(){
        EventKey2?.Invoke(this, EventArgs.Empty);
    }

    CheckpointSaving cpSaving;
    void Awake() {
         Time.timeScale = 1;
         isGamePaused = false;
        _instance = FindFirstObjectByType<GameManager>();

        cpSaving = FindFirstObjectByType<CheckpointSaving>();
    }

    public Player Player;
    public bool GotKey1 = false;
    public bool GotKey2 = false;
    public bool GotKey3 = false;
    public event EventHandler EventKey2;
    public string currentGroundWalkingCategory = "none by default";  // the audio clip of the noisy groudn we are currently working on
    public GlobalMusic globalMusic;

    void Start() {
        StartAmbientMusic();
        cpSaving.ConfigureGameManagerWhenRespawning();
    }


    [SerializeField] private Animator deathAnim;
    [SerializeField] private Animator winAnim;
    [SerializeField] private Animator deathAnimSpider;
    public TextMeshProUGUI keys_nb_display;
    public GameObject key_image;

    public bool final_boss = false;

    public bool isGamePaused = false;

    public AudioSource gameoverAudioSource;
    public AudioClip gameoverSpider;
    public AudioClip gameoverGrunt;
    public void DisplayEndScreen()
    {
        if (final_boss)
        {
            deathAnimSpider.gameObject.SetActive(true);
            deathAnim.gameObject.SetActive(false);
            deathAnimSpider.SetTrigger("displayGameOver");

            gameoverAudioSource.resource = gameoverSpider;
            gameoverAudioSource.Play();
            gameoverAudioSource.volume = 1.0f;
        }
        else
        {
            deathAnimSpider.gameObject.SetActive(false);
            deathAnim.gameObject.SetActive(true);
            deathAnim.SetTrigger("displayGameOver");

            gameoverAudioSource.resource = gameoverGrunt;
            gameoverAudioSource.Play();
            gameoverAudioSource.volume = 1.0f;
        }
        SetCursorActive(true);
    }

    [SerializeField] private GameObject winGameCanvas;
    public void DisplayWinScreen()
    {
        winGameCanvas.gameObject.SetActive(true);
        SetCursorActive(true);
        winAnim.SetTrigger("displayGameOver");
        Destroy(cpSaving);
    }

    public void StopTime()
    {
        isGamePaused = true;
        Time.timeScale = 0;
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
        UpdateKeyNb();
    }
    public void UpdateKeyNb()
    {
        if (nb_keys > 0)
        {
            key_image.SetActive(true);
            keys_nb_display.text = nb_keys.ToString();
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
