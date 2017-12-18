using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Input de Movimiento Horizontal")]
    private string horizontalMove = "Horizontal";

    [SerializeField]
    [Tooltip("Velocidad Horizontal")]
    private float speed = 5f;

    [SerializeField]
    [Tooltip("Input de Saltar")]
    private string jumpButton = "Jump";

    [Tooltip("Si se puede Mover")]
    public bool canMove = true;

    [Tooltip("Define si el jugador esta vivo")]
    private bool alive = true;

    private bool paused = false;

    [Tooltip("Si puede Saltar")]
    public bool canJump = true;

    [SerializeField]
    [Tooltip("Maxima Altura de Salto")]

    /// Maxima Altura de Salto
    private float maxJumpHeight = 5f;

    /// Velocidad Inicial para Lograr Salto.
    private float initialJumpVel;

#if UNITY_EDITOR
    [SerializeField]
    [Tooltip("Si se quiere debuguear la velocidad inicial.")]
    public bool debugInitialJumpVel = false;
#endif

    /// Mitad de la Altura
    private float height;

    [SerializeField]
    [Tooltip("Offset de Altura para Calculos sobre pisar suelo")]
    private float heightOffset = 0f;

    [SerializeField]
    [Tooltip("Radio para el calculo de pisar el suelo")]
    private float isGroundedRadius = 1f;

    [SerializeField]
    [Tooltip("Selecciona las capas donde se puede mover el jugador")]
    private LayerMask whatIsGround;


    private bool isGroundedPlayer = false;

    /// Si jugador esta tocando el piso
    public bool IsGroundedPlayer { get { return isGroundedPlayer; } }

    /// Vector para guardar la posicion donde se mide si
    /// se esta tocando el suelo
    private Vector2 _groundHelp = Vector2.zero;

#if UNITY_EDITOR
    [SerializeField]
    [Tooltip("Si se quiere ver la esfera de sensar el piso")]
    public bool debugIsGrounded = false;
#endif

    private Rigidbody2D body;

    private Vector2 velocity = Vector2.zero;

	[SerializeField]
	[Tooltip("Activa modo runner")]
	private bool runner = false;

    private AudioSource audioSource;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        height = GetComponent<CapsuleCollider2D>().size.y / 2f;
        initialJumpVel = JumpInitialVelocity(maxJumpHeight);
        GetComponent<Rigidbody2D> ().freezeRotation = true;
    }

    public void SetLife() {
        alive = false;
    }

    public void SetPause() {
        paused = !paused;
    }

    private void Update()
    {
        if (!paused && alive && canMove && canJump && isGroundedPlayer
            && Input.GetButtonDown(jumpButton)) Jump();

        velocity.y = body.velocity.y;

		if (!paused && alive && canMove && (Input.GetButton(horizontalMove) || runner)) Run();
		else velocity.x = 0f;
    }

    private void FixedUpdate()
    {
        IsGrounded();
    }

    private void Run()
    {
		Vector3 direction = runner? transform.right : transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        velocity.x = direction.x;
    }

    private void Jump()
    {
        // Evitar saltar cuando esta pausado
        if (Time.timeScale == 0)
        {
            return;
        }
        Vector2 vel = body.velocity;
        vel.y += initialJumpVel;
        body.velocity = vel;

        audioSource.Play();
    }

    // Revisa y guarda si se esta tocando el suelo
    private void IsGrounded()
    {
        _groundHelp.x = transform.position.x;
        _groundHelp.y = transform.position.y - heightOffset - height;

        isGroundedPlayer = Physics2D.OverlapCircle(
            _groundHelp,
            isGroundedRadius,
            whatIsGround
        );

#if UNITY_EDITOR
        if (debugIsGrounded)
        {
            Debug.LogFormat("Soy {0}. Estoy en el piso: {1}", this, isGroundedPlayer);
        }
#endif
    }

    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        if (debugIsGrounded)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - heightOffset - height, transform.position.z), isGroundedRadius);
        }
#endif
    }

	/// Calcular la Velocidad Inicial para lograr Altura de Salto
	/// Salida: la Velocidad Inicial para lograr Altura de Salto
	/// Param: Altura Maxima de Salto
	private float JumpInitialVelocity(float height)
    {
        return Mathf.Sqrt(2f * height * -Physics.gravity.y * body.gravityScale);
    }
}
