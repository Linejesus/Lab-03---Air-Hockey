using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int PlayerScore1 = 0;
    public static int PlayerScore2 = 0;

    [Header("Disco")]
    public GameObject disco;
    public Transform posicaoInicialDisco;

    [Header("Placar")]
    public TMP_Text textoPlayer1;
    public TMP_Text textoPlayer2;

    void Start()
    {
        AtualizarPlacar();
    }

    // Player 1 marcou
    public void GolPlayer1()
    {
        PlayerScore1++;

        Debug.Log("PLAYER 1 MARCOU! Placar: " +
                  PlayerScore1 + " x " + PlayerScore2);

        AtualizarPlacar();
        ReiniciarDisco();
    }

    // Player 2 marcou
    public void GolPlayer2()
    {
        PlayerScore2++;

        Debug.Log("PLAYER 2 MARCOU! Placar: " +
                  PlayerScore1 + " x " + PlayerScore2);

        AtualizarPlacar();
        ReiniciarDisco();
    }

    void AtualizarPlacar()
    {
        if (textoPlayer1 != null)
            textoPlayer1.text = PlayerScore1.ToString();

        if (textoPlayer2 != null)
            textoPlayer2.text = PlayerScore2.ToString();
    }

    void ReiniciarDisco()
    {
        if (disco == null)
            return;

        Rigidbody2D rb = disco.GetComponent<Rigidbody2D>();

        // Para o disco
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Volta o disco para o centro
        if (posicaoInicialDisco != null)
        {
            disco.transform.position = posicaoInicialDisco.position;
        }
        else
        {
            disco.transform.position = Vector3.zero;
        }

        // Pequeno atraso antes de o disco voltar a se mover
        Invoke(nameof(ReiniciarMovimentoDisco), 0.5f);
    }

    void ReiniciarMovimentoDisco()
    {
        if (disco == null)
            return;

        Rigidbody2D rb = disco.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Lança o disco para uma direção aleatória
            Vector2 direcao = Random.insideUnitCircle.normalized;

            // Evita que ele comece praticamente na horizontal
            if (Mathf.Abs(direcao.y) < 0.3f)
            {
                direcao.y = direcao.y >= 0 ? 0.5f : -0.5f;
                direcao.Normalize();
            }

            rb.linearVelocity = direcao * 5f;
        }
    }

    public void ReiniciarPartida()
    {
        PlayerScore1 = 0;
        PlayerScore2 = 0;

        AtualizarPlacar();
        ReiniciarDisco();
    }
}
