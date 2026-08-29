using UnityEngine;

public class IAPlayerControl : MonoBehaviour
{
    public Transform puck;

    public float speed = 8f;

    public float minX = -4f;
    public float maxX = 4f;
    public float minY = 1f;
    public float maxY = 7f;

    public float reactionDistance = 2.5f;
    public float reactionDelay = 0.15f;

    [Header("Erro da IA")]
    public float aimError = 0.3f;

    private Rigidbody2D rb2d;

    private Vector2 targetPosition;
    private float nextReactionTime;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (puck == null)
            return;

        Vector2 currentPosition = rb2d.position;

        // A IA só reage quando o puck está na metade dela.
        if (puck.position.y > reactionDistance)
        {
            if (Time.time >= nextReactionTime)
            {
                CalculateTarget();

                nextReactionTime = Time.time + reactionDelay;
            }
        }
        else
        {
            // Quando o puck está longe, a IA volta
            // lentamente para uma posição defensiva.
            targetPosition = new Vector2(
                puck.position.x,
                maxY
            );
        }

        // Garante que a IA nunca saia da área permitida.
        targetPosition.x = Mathf.Clamp(
            targetPosition.x,
            minX,
            maxX
        );

        targetPosition.y = Mathf.Clamp(
            targetPosition.y,
            minY,
            maxY
        );

        // Move a IA de forma suave.
        Vector2 newPosition = Vector2.MoveTowards(
            currentPosition,
            targetPosition,
            speed * Time.fixedDeltaTime
        );

        rb2d.MovePosition(newPosition);
    }

    private void CalculateTarget()
    {
        // Pequeno erro proposital para a IA não ser perfeita.
        float errorX = Random.Range(-aimError, aimError);
        float errorY = Random.Range(-aimError, aimError);

        targetPosition = new Vector2(
            puck.position.x + errorX,
            puck.position.y + errorY
        );
    }
}
