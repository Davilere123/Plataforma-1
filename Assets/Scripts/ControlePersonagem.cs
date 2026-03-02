using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlePersonagem : MonoBehaviour
{
    
    private float velocidade = 4.0f; // Velocidade de movimento do personagem
    private Rigidbody2D rb;

    private bool querPular = false;
    public Transform playerfeet;
    public float raioDoSensor = 0.2f;
    public LayerMask camadaChao;

    private bool estaNoChao;

    private AudioSource JBL;
    public AudioClip somGanhou;
    public AudioClip somPerdeu;
    public AudioClip somPulou;

    private bool venceuJogo = false;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        JBL = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(playerfeet.position, raioDoSensor, camadaChao) == true)
        {
            estaNoChao = true;
        }
        else
        {
            estaNoChao = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao == true)
        {
            querPular = true;
        }

        if (transform.position.y <= -6 && venceuJogo == false)
        {
            venceuJogo = true;
            JBL.PlayOneShot(somPerdeu);
            Invoke("ReiniciarJogo", 1f);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.linearVelocity = new Vector2(-velocidade, rb.linearVelocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.linearVelocity = new Vector2(velocidade, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0,rb.linearVelocity.y);
        }

        if (querPular == true)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 7);
            JBL.PlayOneShot(somPulou);
            querPular = false;
        }        
    }

    void ReiniciarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter2D(Collider2D outroObjeto)
    {
        if (outroObjeto.CompareTag("Vitoria"))
        {
            JBL.PlayOneShot(somGanhou);
            Invoke("ReiniciarJogo", 0.35f);
        }
    }
}
