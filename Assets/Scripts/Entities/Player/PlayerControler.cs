using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
	public static PlayerControler instance;

	public float moveSpeed = 10f;
	public float jumpForce = 8f;
	public float wallSlidingSpeed = 5f;
	public float moveForceInAir = 1000;
	public Vector2 maxAirSpeed = new Vector2(6, 8);
	public float inAirTurnAroundMultiplier = 1.5f;
	public float airDragMultiplicator = 0.1f;
	public float jumpHeightCutOffMultiplier = 0.5f;
	public float wallJumpForce = 500f;

	public float controlBlockDelayWallJump = 0.2f;
	public float groundCheckRadius = 0.5f;
	public float wallCheckDistance = 0.5f;
	public int amountOfJumps = 1;
	public LayerMask whatIsGround;

	public Transform directionFlipTransform;

	public Vector2 wallJumpDir;

	private int amountOfJumpsLeft;

	private bool facingRight = true;
	private bool jumpPressed = false, jumpNowReleased = false;
	private bool isWalking = false;
	private bool isLookingUp = false;
	private bool isDucking = false;
	private bool isGrounded = false, canJump = false;
	private bool isTouchingWall = false;
	private bool isWallSliding = false;

	private float horizontalInput = 0;
	private float verticalInput = 0;
	private float moveBlockDelay = 0;

	public Transform groundCheck;
	public Transform wallCheck;

	private Rigidbody2D body;
	private Animator animator;

	[SerializeField] private ParticleSystem partikelSystem;

	private string jumpButton = "Jump";
	private string horizontalMovementAxis = "Horizontal";
	private string verticalMovementAxis = "Vertical";

	private void Awake() {
		instance = this;
	}

	private void Start()
    {
		body = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		amountOfJumpsLeft = amountOfJumps;
		wallJumpDir.Normalize();
		if (wallJumpDir.x > 0) wallJumpDir.x = -wallJumpDir.x;


    }

	public void FixedUpdate() {
		Move();
		UpdateJumping();
		CheckSurroundings();
		ResetButtons();
	}

	private void ResetButtons() {
		jumpPressed = false;
		jumpNowReleased = false;
	}

	private void GetInputs() {
		horizontalInput = Input.GetAxis(horizontalMovementAxis);
		verticalInput = Input.GetAxis(verticalMovementAxis);
		if (!jumpPressed)
			jumpPressed = Input.GetButtonDown(jumpButton);

		if (!jumpNowReleased)
			jumpNowReleased = Input.GetButtonUp(jumpButton);
	}

	private void Move() {


		if (moveBlockDelay > 0) moveBlockDelay -= Time.deltaTime;

		if (isGrounded) {
			body.velocity = new Vector2(moveSpeed * horizontalInput * Time.deltaTime, body.velocity.y);
			if (Mathf.Abs(horizontalInput) > 0.3) SoundManager.PlaySound(Sound.PLAYER_FOOTSTEPS, true);
		} else {
			if (body.velocity.x > 0 == facingRight) { //Input in LookingDir
				if (moveBlockDelay <= 0 && horizontalInput != 0 && Mathf.Abs(body.velocity.x) <= maxAirSpeed.x) {
					body.AddForce(new Vector2(moveForceInAir * horizontalInput * Time.deltaTime, 0), ForceMode2D.Force);
					CapAirSpeedX();
				}
			} else { //Input against LookingDir
				body.AddForce(new Vector2(moveForceInAir * horizontalInput * inAirTurnAroundMultiplier * Time.deltaTime, 0), ForceMode2D.Force);
			}

			if (horizontalInput == 0) {
				body.velocity = new Vector2(body.velocity.x * Mathf.Max(1 - airDragMultiplicator * Time.deltaTime, 0), body.velocity.y);
			}
			CapAirSpeedY();
		}

		if (isWallSliding) {
			if (body.velocity.y < -wallSlidingSpeed) body.velocity = new Vector2(body.velocity.x, -wallSlidingSpeed);
		}
	}

	private void CapAirSpeedX() {
		if (Mathf.Abs(body.velocity.x) > maxAirSpeed.x) {
			body.velocity = new Vector2((facingRight ? 1 : -1) * maxAirSpeed.x, body.velocity.y);
		}
	}

	private void CapAirSpeedY() {
		if (Mathf.Abs(body.velocity.y) > maxAirSpeed.y)
			body.velocity = new Vector2(body.velocity.x, (body.velocity.y > 0 ? 1 : -1) * maxAirSpeed.y);
	}

	private void CheckSurroundings() {
		bool wasLastFrameGrounded = isGrounded;
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
		if (!wasLastFrameGrounded && isGrounded) SoundManager.PlaySound(Sound.PLAYER_LAND, true);

		isTouchingWall = Physics2D.Raycast(wallCheck.position, new Vector2(facingRight ? 1 : -1, 0), wallCheckDistance, whatIsGround);
	}

	private void CheckMovementDir() {
		if (facingRight && horizontalInput < 0) {
			Flip();
		} else if (!facingRight && horizontalInput > 0) {
			Flip();
		}

		if (Mathf.Abs(horizontalInput) > 0.3f) {
			isWalking = true;
			isDucking = false;
			isLookingUp = false;
		} else {
			isWalking = false;
			isLookingUp = false;
			isDucking = false;

			if (verticalInput > 0) {
				isLookingUp = true;
			}else if(verticalInput < 0) {
				isDucking = true;
			}
		}
	}

	private void UpdateJumping() {
		if (jumpPressed && isWallSliding) {
			int dir = facingRight ? 1 : -1;
			body.velocity = new Vector2(wallJumpDir.x * wallJumpForce * dir, wallJumpDir.y * wallJumpForce);
			moveBlockDelay = controlBlockDelayWallJump;
			jumpPressed = false;

			SoundManager.PlaySound(Sound.PLAYER_JUMP, true);
		}

		if (jumpPressed && canJump) {
			body.velocity = new Vector2(body.velocity.x, jumpForce);
			jumpPressed = false;
			amountOfJumpsLeft--;

			SoundManager.PlaySound(Sound.PLAYER_JUMP, true);
		}


		if (jumpNowReleased) {
			jumpNowReleased = false;
			if (body.velocity.y > 0) body.velocity = new Vector2(body.velocity.x, body.velocity.y * jumpHeightCutOffMultiplier);
		}


	}

	private void UpdateAnimations() {
		animator.SetBool("isWalking", isWalking);
		animator.SetBool("isGrounded", isGrounded);
		animator.SetBool("isWallSliding", isWallSliding);
		animator.SetBool("isLookingUp", isLookingUp);
		animator.SetBool("isDucking", isDucking);
	}

	private void Flip() {
		facingRight = !facingRight;
		directionFlipTransform.rotation = Quaternion.Euler(0, (directionFlipTransform.eulerAngles.y + 180) % 360, 0);
	}

	// Update is called once per frame
	void Update()
    {
		GetInputs();
		CheckMovementDir();
		UpdateAnimations();
		CheckCanJump();
		CheckIfWallSliding();
		UpdatePartikleSystem();
    }

	private void UpdatePartikleSystem() {
		ParticleSystem.EmissionModule emission = partikelSystem.emission;

		if (isWallSliding || isGrounded) {
			if (!emission.enabled) emission.enabled = true;
		} else {
			if (emission.enabled) emission.enabled = false;
		}
	}


	private void CheckIfWallSliding() {
		isWallSliding = !isGrounded && isTouchingWall && body.velocity.y <= 0 && (horizontalInput > 0 == facingRight);
	}

	private void CheckCanJump() {
		if (isGrounded && body.velocity.y <= 0) {
			amountOfJumpsLeft = amountOfJumps;
		}

		canJump = amountOfJumpsLeft > 0;
	}

	private void OnDrawGizmos() {
		Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

		Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, 0));
	}
	public static void Shutdown() {
		_Shutdown();
	}

	private static void _Shutdown() {
		instance.enabled = false;
		instance.body.velocity = new Vector2(0, instance.body.velocity.y);

	}

	public static void WakeUp() {
		instance.enabled = true;
	}

	public static void Shutdown(float time) {
		instance.StartCoroutine(EnablePlayerControllerAfterTime(time));
		_Shutdown();
	}


	private static IEnumerator EnablePlayerControllerAfterTime(float time) {
		yield return new WaitForSeconds(time);
		instance.enabled = true;
	}


	public bool IsGrounded() {
		return isGrounded;
	}
}


