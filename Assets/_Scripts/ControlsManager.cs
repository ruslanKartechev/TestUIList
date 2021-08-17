using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(IControl)) ]
public class ControlsManager : MonoBehaviour
{
    private PlayerInput input;
    private IControl controlHandle;
    void Awake()
    {

        controlHandle = GetComponent<IControl>();
        input = GetComponent<PlayerInput>();
        SetKeyboardMouseScheme();
        SetInGameActionMap();
        InitActions();
        input.enabled = true;

    }
    private void InitActions()
    {
        input.actions["Fire"].started += ctx => OnMouseClick(ctx);
        input.actions["Fire"].canceled += ctx => OnMouseRelease(ctx);
    }

    public void OnMousePosition(Vector2 rawPos)
    {
        controlHandle.SetPosition(rawPos);
    }
    public void OnMouseClick(InputAction.CallbackContext ctx)
    {
        controlHandle.Click();
    }
    public void OnMouseRelease(InputAction.CallbackContext ctx)
    {
        controlHandle.Release();
    }
    private void FixedUpdate()
    {
       OnMousePosition(input.actions["Look"].ReadValue<Vector2>());
    }

    public void SetInGameActionMap()
    {
        input.SwitchCurrentActionMap("Player");
    }
    public void SetUIActionMap()
    {
        input.SwitchCurrentActionMap("UI");
    }
    public void SetKeyboardMouseScheme()
    {
        input.SwitchCurrentControlScheme("Keyboard&Mouse");
    }


}
