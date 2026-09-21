using Unity.VisualScripting;
using UnityEngine;

public class Guards : MonoBehaviour
{
    private Collider2D collider;
    private Animator animator;
    private Transform transform;
    
    [SerializeField] private GameObject detected_canvas;

    [SerializeField] private string type;

    [SerializeField] private FieldOfView fieldOfView;

    [SerializeField] private GameObject player;

    // Patrol
    [SerializeField] private Transform[] patrol_points;
    private int index = 0;
    [SerializeField] private float patrol_speed = 1;
    private Vector3 target_pos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        collider = GetComponent<Collider2D>();
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        target_pos = transform.position;
        detected_canvas.SetActive(false);
        
        fieldOfView = Instantiate(fieldOfView).GetComponent<FieldOfView>();
    }


    void FixedUpdate()
    {
        // Vector2 current_position = rb.position;
        // Vector2 ray_size = new Vector2(2, 3);
        // Vector2 direction = new Vector2(1, 0);
        // RaycastHit2D[] hit = Physics2D.CapsuleCastAll(current_position, ray_size, CapsuleDirection2D.Horizontal, 0, direction);
        // if (!hit[0].collider.CompareTag("Guards"))
        // {
        //     Debug.Log(hit[0].collider.name);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        fieldOfView.SetOrigin(transform.position);

        FieldPoint();

        if (type != "stationary")
        {
            animator.SetBool("is_moving", true);
            Patrol();
        }
        
        if (fieldOfView.GetPlayer())
        {
            Detected();
        }
    }

    void Patrol()
    {
        Transform point = patrol_points[index];
        transform.position = Vector3.MoveTowards(transform.position, point.position, patrol_speed);
        if (Vector3.Distance(transform.position, point.position) < 0.001f)
        {
            point.position = target_pos;
            target_pos = transform.position;
        }

        Walk();
    }

    // private void FindPlayer()
    // {
    //     
    //     if (Vector3.Distance(transform.position, player.transform.position) < fieldOfView.GetViewDistance())
    //     {
    //         // player in distance
    //         Vector3 player_direction = (player.transform.position - transform.position).normalized;
    //         if (Vector3.Angle(fieldOfView.GetAimDirection(), player_direction) < 40)
    //         {
    //             // player inside view angle
    //             detected_canvas.SetActive(true);
    //         }
    //     }
    // }

    private void FieldPoint()
    {
        switch (type)
        {
            case "horizontal": // x axis
                if (transform.position.x > target_pos.x)
                {
                    fieldOfView.SetAimDirection(90); // left (-1,0)
                    animator.SetFloat("x_val", -1);
                    animator.SetFloat("y_val", 0);
                }
                else if (transform.position.x < target_pos.x)
                {
                    fieldOfView.SetAimDirection(270); // right (1,0)
                    animator.SetFloat("x_val", 1);
                    animator.SetFloat("y_val", 0);
                }

                break;
            case "vertical": // y axis
                if (transform.position.y > target_pos.y)
                {
                    fieldOfView.SetAimDirection(180); // down (0,-1)
                    animator.SetFloat("x_val", 0);
                    animator.SetFloat("y_val", -1);
                }
                else if (transform.position.y < target_pos.y)
                {
                    fieldOfView.SetAimDirection(0); // up (0,1)
                    animator.SetFloat("x_val", 0);
                    animator.SetFloat("y_val", 1);
                }

                break;
            case "stationary":
                fieldOfView.SetAimDirection(0);
                break;
        }
    }

    private void Walk()
    {
        switch (type)
        {
            case "horizontal": // x axis
                if (transform.position.x < target_pos.x)
                {
                    animator.SetFloat("x_val", -1);
                    animator.SetFloat("y_val", 0);
                }
                else if (transform.position.x > target_pos.x)
                {
                    animator.SetFloat("x_val", 1);
                    animator.SetFloat("y_val", 0);
                }

                break;
            case "vertical": // y axis
                if (transform.position.y < target_pos.y)
                {
                    animator.SetFloat("x_val", 0);
                    animator.SetFloat("y_val", -1);
                }
                else if (transform.position.y > target_pos.y)
                {
                    animator.SetFloat("x_val", 0);
                    animator.SetFloat("y_val", 1);
                }

                break;
            case "stationary":
                animator.SetFloat("x_val", 0);
                animator.SetFloat("y_val", 0);
                break;
        }
    }

    public static float GetAngleFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        return n;
    }

    private void Detected()
    {
        detected_canvas.SetActive(true);
        Time.timeScale = 0;
    }
}