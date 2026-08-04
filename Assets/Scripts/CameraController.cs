using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerInput playerinput;
    public float movespeed;
    private float camy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerinput = new PlayerInput();
        playerinput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseinput = playerinput.Player.Look.ReadValue<Vector2>();
        camy -= mouseinput.y;
        camy = Mathf.Clamp(camy, -90f, 90f);
        transform.localRotation = Quaternion.Euler(camy, 0, 0);
    }
}
