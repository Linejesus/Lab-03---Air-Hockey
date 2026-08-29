using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 10f;

    public float minX = -4f;
    public float maxX = 4f;
    public float minY = -7f;
    public float maxY = -1f;

    private Rigidbody2D rb2d;
    private Camera cam;

    private Vector2 targetPosition;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    private void Update()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        targetPosition = new Vector2(
            Mathf.Clamp(mousePosition.x, minX, maxX),
            Mathf.Clamp(mousePosition.y, minY, maxY)
        );
    }

    private void FixedUpdate()
    {
        Vector2 currentPosition = rb2d.position;

        Vector2 newPosition = Vector2.MoveTowards(
            currentPosition,
            targetPosition,
            speed * Time.fixedDeltaTime
        );

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        rb2d.MovePosition(newPosition);
    }
}
