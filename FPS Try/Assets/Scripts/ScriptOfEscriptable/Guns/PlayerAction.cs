using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    [Header("Inputs Actions")]
    [SerializeField] private InputActionAsset PlayerControls;

    [Header("Gun Selector")]
    [SerializeField] private PlayerGunSelector GunSelector;

    private InputAction fireAction;

    private void Awake()
    {
        fireAction = PlayerControls.FindActionMap("Combat").FindAction("Fire");
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && GunSelector.ActiveGun != null)
        {
            GunSelector.ActiveGun.Shoot();
        }
    }
}
