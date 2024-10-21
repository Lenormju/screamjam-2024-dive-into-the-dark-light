using UnityEngine;

public class AudioSourceRaycasting : MonoBehaviour
{
    private Transform _player_transform;
    private AudioSource _audio_source;

    void Start() {
        _player_transform = GameManager.Instance.Player.transform;
        _audio_source = GetComponent<AudioSource>();
    }
    void Update() {
        Vector3 origin = this.transform.position;
        Vector3 direction = _player_transform.position - origin;

        RaycastHit hit;
        bool has_hit_something = Physics.Raycast(origin, direction, out hit);  // from AudioSource to Player

        bool is_player_visible = false;  // by default
        if (has_hit_something) {
            //Debug.Log("hit something: ", hit.transform);
            if (hit.transform == _player_transform) {
                //Debug.Log("hit player");
                is_player_visible = true;
            } else {
                //Debug.Log("hit something else than player");
            }
        } else {
            //Debug.Log("pas hit");
        }

        if (is_player_visible) {
            this._audio_source.volume = 0.8F;
        } else {
            this._audio_source.volume = 0.5F;
        }
    }
}
