using UnityEngine;
using UnityEngine.InputSystem;

public class ShootController : MonoBehaviour
{
    [SerializeField] Transform tr;
    [SerializeField] GameObject bullet;
    [SerializeField] PlayerControls playercontrols;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playercontrols = new PlayerControls();
        playercontrols.Enable();
        playercontrols.Player.Shoot.performed += OnShootPerformed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnShootPerformed(InputAction.CallbackContext context)
    {
        Instantiate(bullet, (tr.position + 1 * transform.forward), tr.rotation);
        Debug.Log("bang");
    }
}
