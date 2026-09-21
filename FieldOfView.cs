using UnityEditor;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    //how far field of view goes
    [SerializeField] private float view_distance;

    //angle for field of view
    [SerializeField] private float fov;

    [SerializeField] private LayerMask layerMask;
    
    private Mesh mesh;
    private Vector3 origin;
    private float start_angle = 0;

    public bool player_found = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        origin = Vector3.zero;
        player_found = false;
    }

    // Update is called once every frame
    void Update()
    {
        player_found = false;
        int rayCount = 40; //more = smooth curve
        float angle = start_angle;
        float angle_inc = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertex_index = 1;
        int triangle_index = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycast_hit = Physics2D.Raycast(origin, GetVectorFromAngle(angle), view_distance, layerMask);

            // did we hit anything?
            if (raycast_hit.collider == null)
            {
                // nope all good
                vertex = origin + GetVectorFromAngle(angle) * view_distance;
            }
            else
            {
                if (raycast_hit.collider.CompareTag("Player"))
                {
                    player_found = true;
                }
                // yes go around
                Vector3 temp = new Vector3();

                // define x
                if (raycast_hit.point.x > origin.x)
                {
                    temp.x = raycast_hit.point.x + 0.4f;
                }
                else if (raycast_hit.point.x < origin.x)
                {
                    temp.x = raycast_hit.point.x - 0.4f;
                }
                else
                {
                    temp.x = raycast_hit.point.x;
                }

                // define y
                if (raycast_hit.point.y > origin.y)
                {
                    temp.y = raycast_hit.point.y + 0.4f;
                }
                else if (raycast_hit.point.y < origin.y)
                {
                    temp.y = raycast_hit.point.y - 0.4f;
                }
                else
                {
                    temp.y = raycast_hit.point.y;
                }

                vertex = temp;
            }

            vertices[vertex_index] = vertex;

            if (i > 0)
            {
                triangles[triangle_index] = 0;
                triangles[triangle_index + 1] = vertex_index - 1;
                triangles[triangle_index + 2] = vertex_index;

                triangle_index += 3;
            }

            vertex_index++;
            angle -= angle_inc;
        }

        mesh.Clear();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }


    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(float aim_direction)
    {
        start_angle = aim_direction - fov / 2; // GetAngleFromVector(aim_direction) - fov / 2;
    }

    public Vector3 GetAimDirection()
    {
        Vector3 ang_cordinates = new Vector3();
        switch (start_angle)
        {
            case -40:
                ang_cordinates = new Vector3(0, 1, 0);
                break;
            case 50:
                ang_cordinates = new Vector3(1, 0, 0);
                break;
            case 140:
                ang_cordinates = new Vector3(0, -1, 0);
                break;
            case 230:
                ang_cordinates = new Vector3(-1, 0, 0);
                break;
        }
        return ang_cordinates;
    }

    public bool GetPlayer()
    {
        return player_found;
    }

    public float GetViewDistance()
    {
        return view_distance;
    }
}