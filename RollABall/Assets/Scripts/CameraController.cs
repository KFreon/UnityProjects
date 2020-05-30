using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    private Vector3 offset;

    private float spinMultiplier = 90;

    private Vector2 orbitAngles = new Vector2(25, 0);  // 25deg up from flat, 0deg rotation

    private float distance = 0;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - Player.transform.position;
        distance = 5;
    }

    Vector2 GetInput()
    {
        var spinRight = Input.GetKey(KeyCode.Keypad6); // right
        var spinLeft = Input.GetKey(KeyCode.Keypad4); // left
        var spin = spinRight ? -1 : spinLeft ? 1 : 0;

        orbitAngles += new Vector2(0, Time.deltaTime * spinMultiplier * spin);  // Accumulate the orbiting in a stored value
        return orbitAngles;
    }

    // After all update and other codes
    void LateUpdate()
    {
        var orbitAngles = GetInput();

        var lookRotation = Quaternion.Euler(orbitAngles);
        var lookDirection = lookRotation * Vector3.forward;
        var lookPosition = Player.transform.position - lookDirection * distance; // From the player position, the camera position should be distance units back along the look direction
        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }
}
