using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement: MonoBehaviour
{   
    #region Properties
    public Vector3 Movement { get; set; }
    public float Speed { get; set; } = 5f;
    #endregion

    #region UnityEngine
    private void Start()
    {

    }

    private void Update()
    {
        Move();
    }
    #endregion

    private void Move()
    {
        bool isLeftPressed = Keyboard.current != null && Keyboard.current.aKey.isPressed || Gamepad.current != null && Gamepad.current.leftStick.x.ReadValue() < 0;
        bool isRightPressed = Keyboard.current != null && Keyboard.current.dKey.isPressed || Gamepad.current != null && Gamepad.current.leftStick.x.ReadValue() > 0;
        bool isUpPressed = Keyboard.current != null && Keyboard.current.wKey.isPressed || Gamepad.current != null && Gamepad.current.leftStick.y.ReadValue() > 0;
        bool isDownPressed = Keyboard.current != null && Keyboard.current.sKey.isPressed || Gamepad.current != null && Gamepad.current.leftStick.y.ReadValue() < 0;

        Movement = new Vector3(isLeftPressed ? -1 : isRightPressed ? 1 : 0, isDownPressed ? -1 : isUpPressed ? 1 : 0, 0);
        transform.position += Speed * Movement * Time.deltaTime;
    }
}