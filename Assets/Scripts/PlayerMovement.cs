using UnityEngine;
using Unity.Mathematics;
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
    public float grounddist;
    public float hoverstrength;
    public float stepspeed;
    public float fallcounter;
    float speeddistscale;
    
    private float camy;
    private bool groundcheck;

    private bool dragapplied = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        playerinput = new PlayerControls();
    }
    void OnEnable()
    {
        playerinput.Enable();
        playerinput.Player.Jump.performed += OnJumpPerformed;
    }

    void OnDisable()
    {
        playerinput.Player.Disable();
        playerinput.Disable();
        playerinput.Player.Jump.performed -= OnJumpPerformed;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float basemovespeed;
        float baseacceleration;
        groundcheck = Physics.CheckSphere(ground.position, grounddist, groundmask);
        Vector2 playermove = playerinput.Player.Movement.ReadValue<Vector2>();
        Vector2 mouseinput = playerinput.Player.Look.ReadValue<Vector2>();
        float forwardspeed = Vector3.Dot(rb.linearVelocity, transform.forward);
        float sidespeed = Vector3.Dot(rb.linearVelocity, transform.right);

        playermove.Normalize();

        RaycastHit hit;
        if (groundcheck == true)
        {
            baseacceleration = groundacceleration;
            basemovespeed = groundmovespeed;
            if (dragapplied == false)
            {
                rb.linearDamping += drag;
                dragapplied = true;
            }

            if (Physics.Raycast(ground.position, new Vector3(0, -1, 0), out hit, grounddist, groundmask) == true)
            {

                float mapdist = math.remap(0f, grounddist * grounddist, grounddist * grounddist, 0f, hit.distance * hit.distance);
                float upforce = mapdist * hoverstrength * Time.deltaTime;
                upforce *= Mathf.Clamp(-rb.linearVelocity.y * fallcounter, 1, 100);

                //print(upforce + " " + -rb.linearVelocity.y);

                if (upforce + rb.linearVelocity.y > (stepspeed * mapdist)) //if the force will make it go over the max speed
                {
                    upforce = stepspeed * mapdist - rb.linearVelocity.y; //makes the force just enough to hit max speed
                }
                if(upforce > 0) //prevents a negative value from dragging the player down
                {
                    rb.linearVelocity += new Vector3(0, upforce, 0);
                }
                
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

        float acceleration = baseacceleration;
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
            acceleration = baseacceleration;
        }

        float sideaddspeed = movespeed - (sidespeed * playermove.x);
        if (playermove.x * sidespeed < movespeed && sideaddspeed > 0f)
        {
            if (sideaddspeed < acceleration)
            {
                acceleration = sideaddspeed;
            }
            rb.linearVelocity += (playermove.x * acceleration * transform.right);
            acceleration = baseacceleration;
        }

        transform.Rotate(0f, mouseinput.x, 0);
        camy -= mouseinput.y;
        camy = Mathf.Clamp(camy, -90f, 90f);
        cam.localRotation = Quaternion.Euler(camy, 0, 0);
    }

    public void OnJumpPerformed(InputAction.CallbackContext context)
    {
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