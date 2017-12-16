using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class movement : MonoBehaviour {
	
	[SerializeField]
	[Tooltip("Input de Movimiento Horizontal")]
	/// <summary>
	/// Input de Movimiento Horizontal
	/// </summary>
	private string horizontalAxis = "Horizontal";

	[SerializeField]
	[Tooltip("Velocidad Horizontal")]
	/// <summary>
	/// Velocidad Horizontal
	/// </summary>
	private float horizontalVelocity = 5f;

	/// <summary>
	/// Velocidad Actual del body
	/// </summary>
	private Vector2 currentVel;

	[SerializeField]
	[Tooltip("Input de Saltar")]
	/// <summary>
	/// Input de Saltar
	/// </summary>
	private string jumpButton = "Jump";

	[SerializeField]
	[Tooltip("Si se puede Mover")]
	/// <summary>
	/// Si se puede Mover
	/// </summary>
	public bool canMove = true;

	[SerializeField]
	[Tooltip("Si puede Saltar")]
	/// <summary>
	/// Si puede Saltar
	/// </summary>
	public bool canJump = true;

	[SerializeField]
	[Tooltip("Maxima Altura de Salto")]
	/// <summary>
	/// Maxima Altura de Salto
	/// </summary>
	private float maxJumpHeight = 5f;

	/// <summary>
	/// Velocidad Inicial para Lograr Salto.
	/// </summary>
	private float initialJumpVel;

	#if UNITY_EDITOR
	[SerializeField]
	[Tooltip("Si se quiere debuguear la velocidad inicial.")]
	/// <summary>
	/// Si se quiere debuguear la velocidad inicial.
	/// </summary>
	public bool debugInitialJumpVel = false;
	#endif

	/// <summary>
	/// Si se presiono boton de saltar
	/// </summary>
	private bool jumpButtonPressed = false;

	/// <summary>
	/// Mitad de la Altura
	/// </summary>
	private float height;

	[SerializeField]
	[Tooltip("Offset de Altura para Calculos sobre pisar suelo")]
	/// <summary>
	/// Offset de Altura para Calculos sobre pisar suelo
	/// </summary>
	private float heightOffset = 0f;

	[SerializeField]
	[Tooltip("Radio para el calculo de pisar el suelo")]
	/// <summary>
	/// Radio para el calculo de pisar el suelo
	/// </summary>
	private float isGroundedRadius = 1f;

	[SerializeField]
	[Tooltip("Que se considera piso")]
	/// <summary>
	/// Que se considera piso
	/// </summary>
	private LayerMask whatIsGround;

	/// <summary>
	/// Si estamos tocando el suelo
	/// </summary>
	private bool isGroundedPlayer = false;

	/// <summary>
	/// Vector para guardar la posicion donde se mide si
	/// se esta tocando el suelo
	/// </summary>
	private Vector2 _groundHelp = Vector2.zero;

	#if UNITY_EDITOR
	[SerializeField]
	[Tooltip("Si se quiere ver la esfera de sensar el piso")]
	/// <summary>
	/// Si se quiere ver la esfera de sensar el piso
	/// </summary>
	public bool debugIsGrounded = false;
	#endif

	/// <summary>
	/// El rigidbody2d que se movera.
	/// </summary>
	private Rigidbody2D body;

	/// <summary>
	/// Cuando inicia la caida
	/// </summary>
	private float fallStartTime = -1f;

	// Use this for initialization
	void Start () {
		height = GetComponent<CapsuleCollider2D>().size.y / 2f;
		initialJumpVel = JumpInitialVelocity(maxJumpHeight);
		body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update() {

		if (canMove && canJump && Input.GetButtonDown (jumpButton)) {
			jumpButtonPressed = true;
		}
	}


	void FixedUpdate () {
		currentVel = body.velocity;
		IsGrounded();

		if (isGroundedPlayer) {
			currentVel.y = 0f;
			fallStartTime = -1f;
			if (jumpButtonPressed) {
				currentVel.y += initialJumpVel;
            }
		}
		else {
			if (fallStartTime == -1f) {
				fallStartTime = Time.time;
			}
			// Gravedad ya es negativa
			currentVel.y += Physics.gravity.y * Time.deltaTime;
		}

		if (canMove) {
			
			switch ((int)Input.GetAxisRaw(horizontalAxis)) {
			case 1:
				currentVel.x = horizontalVelocity;
				break;
			case 0:
				currentVel.x = 0;
				break;
			case -1:
				currentVel.x = -horizontalVelocity;
				break;
			default:
				Debug.LogError("No debimos salir por este case");
				break;
			}
		}
		body.velocity = currentVel;
		jumpButtonPressed = false;
	}

	/// <summary>
	/// Revisa y guarda si se esta tocando el suelo
	/// </summary>
	private void IsGrounded(){
		_groundHelp.x = transform.position.x;
		_groundHelp.y = transform.position.y - heightOffset - height;

		isGroundedPlayer = Physics2D.OverlapCircle (
			_groundHelp, 
			isGroundedRadius, 
			whatIsGround
		);

		#if UNITY_EDITOR
		if (debugIsGrounded) {
			Debug.LogFormat("Soy {0}. Estoy en el piso: {1}", this, isGroundedPlayer);
		}
		#endif
	}

	void OnDrawGizmosSelected() {
		#if UNITY_EDITOR
		if (debugIsGrounded) {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - heightOffset - height, transform.position.z), isGroundedRadius);
		}
		#endif
	}

	void OnValidate() {
		height = GetComponent<CapsuleCollider2D>().size.y / 2f;
		if (body == null) {
			body = GetComponent<Rigidbody2D>();
		}
		initialJumpVel = JumpInitialVelocity(maxJumpHeight);
		#if UNITY_EDITOR
		if (debugInitialJumpVel) {
			Debug.LogFormat("Soy {0}. Mi nueva Velocidad de Salto es: {1}", this, initialJumpVel);
		}
		#endif
	}

	/// <summary>
	/// Calcular la Velocidad Inicial para lograr Altura de Salto
	/// </summary>
	/// <returns>la Velocidad Inicial para lograr Altura de Salto</returns>
	/// <param name="height">Altura Maxima de Salto</param>
	private float JumpInitialVelocity(float height) {
		return Mathf.Sqrt(2f * height * -Physics.gravity.y);
	}
}
