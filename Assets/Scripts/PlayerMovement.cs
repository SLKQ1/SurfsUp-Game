using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField]
    private float movementSpeed = 100f;

    private Rigidbody rbody;
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(isLocalPlayer)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            //TODO modify movement of player object to reflect the surfing game mechanic.
            Vector3 movement = new Vector3(horizontal, 0f, vertical);
            rbody.velocity = movement * Time.deltaTime * movementSpeed;
            //END TODO
        }
    }
}
