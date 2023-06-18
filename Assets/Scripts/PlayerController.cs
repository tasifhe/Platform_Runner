using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    [Range(0, 500)]
    [SerializeField] private float forwardForce = 500f;
    [Range(0, 500)]
    [SerializeField] private float sidewaysForce = 500f;
    [Range(0, 10)]
    [SerializeField] private float maxVelocity = 5f;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovePlayerForward();

        float moveInput = Input.GetAxisRaw("Horizontal");
        MovePlayerSideways(moveInput);
    }

    private void MovePlayerForward()
    {
        playerRb.AddForce(0, 0, forwardForce * Time.deltaTime);
    }

    private void MovePlayerSideways(float moveInput)
    {
        float force = moveInput * sidewaysForce * Time.deltaTime;
        playerRb.AddForce(force, 0, 0);

        // Limit the maximum velocity
        Vector3 velocity = playerRb.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity);
        playerRb.velocity = velocity;
    }
}
