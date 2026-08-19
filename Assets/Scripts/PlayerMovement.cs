using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{

    [Header("References")]
    [SerializeField] PlayerControls playerinput;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform cam;
    [SerializeField] Transform ground;
    public LayerMask groundmask;

    [Header("Variables")]
    public float groundmovespeed;
    public float airmovespeed;
    public float groundacceleration;
    public float airacceleration;
    public float jumpforce;
    public float drag;
    
    private float camy;
    private bool groundcheck;

    private bool dragapplied = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerinput = new PlayerControls();
        playerinput.Enable();
        playerinput.Player.Jump.performed += OnJumpPerformed;
    }

    void OnDisable()
    {
        playerinput.Player.Jump.performed -= OnJumpPerformed;
        playerinput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        float basemovespeed;
        float baseacceleration;
        groundcheck = Physics.CheckSphere(ground.position, 0.46f, groundmask);
        Vector2 playermove = playerinput.Player.Movement.ReadValue<Vector2>();
        Vector2 mouseinput = playerinput.Player.Look.ReadValue<Vector2>();
        float forwardspeed = Vector3.Dot(rb.linearVelocity, transform.forward);
        float sidespeed = Vector3.Dot(rb.linearVelocity, transform.right);

        playermove.Normalize();
      

        if (groundcheck == true)
        {
            baseacceleration = groundacceleration;
            basemovespeed = groundmovespeed;
            if (dragapplied == false)
            {
                rb.linearDamping += drag;
                dragapplied = true;
            }
            
        }
        else
        {
            baseacceleration = airacceleration;
            basemovespeed = airmovespeed;
            if (dragapplied == true)
            {
                rb.linearDamping -= drag;
                dragapplied = false;
            }
            
        }

        float acceleration = baseacceleration * Time.deltaTime;
        float movespeed = basemovespeed;
        float forwardaddspeed = movespeed - (forwardspeed * playermove.y);

        if (playermove.x != 0 && playermove.y != 0)
        {
            movespeed *= 0.5f;
        }

        if (playermove.y * forwardspeed < movespeed && forwardaddspeed > 0f)
        {
            if (forwardaddspeed < acceleration)
            {
                acceleration = forwardaddspeed;
            }
            rb.linearVelocity += (playermove.y * acceleration * transform.forward);
            acceleration = baseacceleration * Time.deltaTime;
        }

        float sideaddspeed = movespeed - (sidespeed * playermove.x);
        if (playermove.x * sidespeed < movespeed && sideaddspeed > 0f)
        {
            if (sideaddspeed < acceleration)
            {
                acceleration = sideaddspeed;
            }
            rb.linearVelocity += (playermove.x * acceleration * transform.right);
            acceleration = baseacceleration * Time.deltaTime;
        }

        transform.Rotate(0f, mouseinput.x, 0);
        camy -= mouseinput.y;
        camy = Mathf.Clamp(camy, -90f, 90f);
        cam.localRotation = Quaternion.Euler(camy, 0, 0);
    }

    public void OnJumpPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("hop");
        if (groundcheck == true)
        {
            if (rb.linearVelocity.y > 0)
            {
                rb.linearVelocity += new Vector3(0f, jumpforce - rb.linearVelocity.y, 0f);
            }
            else
            {
                rb.linearVelocity += new Vector3(0f, jumpforce, 0f);
            }
                
        }
    }
}