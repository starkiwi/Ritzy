using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private Sprite close_sprite;
    [SerializeField] private Sprite open_sprite;
    [SerializeField] private GameObject open_overlay;
    
    private Transform transform;
    private SpriteRenderer sprite_renderer;
    private Collider2D coll;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform = GetComponent<Transform>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.sprite = close_sprite;
        coll = GetComponent<Collider2D>();
        coll.enabled = true;
        open_overlay.SetActive(false);
    }

    public void OpenDoor()
    {
        coll.enabled = false;
        sprite_renderer.sprite = open_sprite;
        float new_y = transform.position.y + 0.423f;
        transform.position = new Vector3(transform.position.x, new_y, transform.position.z);
        open_overlay.SetActive(true);
    }
}
