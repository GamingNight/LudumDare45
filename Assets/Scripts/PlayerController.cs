using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Vector2 initPosition;
    public float maxSpeed = 7f;
    public float minGroundNormalY = 0.65f;
    public float jumpTakeOffSpeed = 7f;
    //A memory that allows a slight jump instruction delay when player is not grounded.
    public float jumpInputMemory = 0.1f;
    public float gravityMultiplier = 1;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rgbd2D;

    private ContactFilter2D contactFilter;
    private RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

    private Vector2 velocity;
    private Vector2 wallReaction;
    private bool grounded;
    private Vector2 groundNormal;
    private bool jump;
    private bool slowJump;
    private float jumpInputTimer;

    private const float minMoveDistance = 0.001f;
    private const float shellRadius = 0.01f;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rgbd2D = GetComponent<Rigidbody2D>();

        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;

        grounded = true;
        jump = false;
        slowJump = false;
        jumpInputTimer = 0f;
    }

    public void ResetPosition() {

        transform.position = initPosition;
    }

    void Update() {

        //Register jump entries
        bool _jumpInputDown = Input.GetKeyDown(KeyCode.Space);
        if (_jumpInputDown && jumpInputTimer == 0) {
            jumpInputTimer = jumpInputMemory;
        } else {
            jumpInputTimer = Mathf.Max(0, jumpInputTimer - Time.deltaTime);
        }
        bool jumpInputUp = Input.GetKeyUp(KeyCode.Space);

        //Register a jump entry 
        if (jumpInputTimer > 0 && grounded && !jump) {
            jump = true;
            jumpInputTimer = 0;
        }
        //Register a jump release (slow down jump)
        slowJump = jumpInputUp && velocity.y > 0;
    }

    void FixedUpdate() {

        Vector2 inputVelocity = Vector2.zero;
        inputVelocity += ComputeJumpVelocity();
        inputVelocity += ComputeWalkVelocity();

        velocity = inputVelocity;

        //Apply gravity to velocity
        velocity += Physics2D.gravity * Time.fixedDeltaTime * gravityMultiplier;

        //Remove wall and ceil reactions to velocity
        velocity += wallReaction;

        //Convert velocity into a position shift
        Vector2 deltaPosition = velocity * Time.deltaTime;


        //Compte movement along ground
        Vector2 directionAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Vector2 movementAlongGround = directionAlongGround * deltaPosition.x;
        Move(movementAlongGround, false);

        //compute movement along gravity
        Vector2 antiGravity = -Physics2D.gravity;
        Vector2 movementAlongGravity = antiGravity * deltaPosition.y;
        Move(movementAlongGravity, true);

        //Apply ground friction if grounded
        ApplyGroundFriction();
    }

    private Vector2 ComputeWalkVelocity() {

        float h = Input.GetAxis("Horizontal");
        Vector2 walkVelocity = Vector2.zero;
        if (h != 0) {
            walkVelocity.x = h * maxSpeed;
        } else {
            walkVelocity.x = velocity.x;
        }

        //Flip sprite if necessary
        bool flipSprite = (spriteRenderer.flipX ? (h > 0f) : (h < 0f));
        if (flipSprite) {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        return walkVelocity;
    }

    private Vector2 ComputeJumpVelocity() {

        Vector2 jumpVelocity = Vector2.zero;

        //Jump
        if (jump) {
            jumpVelocity.y = jumpTakeOffSpeed - wallReaction.y;
            jump = false;
        } else if (slowJump) {
            jumpVelocity.y = velocity.y * 0.5f;
        } else {
            jumpVelocity.y = velocity.y;
        }


        return jumpVelocity;
    }

    //Compute movement in a 2Dplateformer-friendly way (doesn't use default RigidBody2D behavior)
    private void Move(Vector2 movement, bool yMovement) {
        float distance = movement.magnitude;

        if (distance > minMoveDistance) {
            grounded = false;
            wallReaction = Vector2.zero;
            int count = rgbd2D.Cast(movement, contactFilter, hitBuffer, distance + shellRadius);
            for (int i = 0; i < count; i++) {
                Vector2 currentNormal = hitBuffer[i].normal;
                if (currentNormal.y > minGroundNormalY) {
                    grounded = true;
                    if (yMovement) {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0) {
                    //If object hits a wall or a ceil, remove velocity in its direction.
                    wallReaction -= projection * currentNormal;
                }

                float modifiedDistance = hitBuffer[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rgbd2D.position = rgbd2D.position + movement.normalized * distance;
    }

    private void ApplyGroundFriction() {
        if (grounded) {
            if (Mathf.Abs(velocity.x) > 0.01f) {
                velocity.x *= 0.1f;
            } else {
                velocity.x = 0f;
            }
        }
    }

    public void ResetVelocity() {

        Input.ResetInputAxes();
        velocity.x = 0;
        jumpInputTimer = 0;
    }
}
