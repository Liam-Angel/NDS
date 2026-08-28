using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] PlayerControls playerinput;
    public GameObject menu;
    private bool showing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        playerinput = new PlayerControls();
    }
    void OnEnable()
    {
        playerinput.Enable();
        playerinput.Menu.Escape.performed += OnEscapePerformed;
    }

    void OnDisable()
    {
        playerinput.Disable();
        playerinput.Menu.Escape.performed -= OnEscapePerformed;
    }

    private void OnEscapePerformed(InputAction.CallbackContext context)
    {
        showing = !showing;
        menu.SetActive(showing);
    }
}
