using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationController: MonoBehaviour
{
    
    private static bool _teleportIsActive = false;

    
    public enum ControllerType
    {
        RightHand,
        LeftHand
    }

    public ControllerType targetController;

   
    public InputActionAsset inputAction;

    
    public XRRayInteractor rayInteractor;

    
    public TeleportationProvider teleportationProvider;


    
    private InputAction _thumbstickInputAction;

   
    private InputAction _teleportActivate;

  
    private InputAction _teleportCancel;

    void Start()
    {
        
        rayInteractor.enabled = false;

       
        Debug.Log("XRI " + targetController.ToString());
        _teleportActivate = inputAction.FindActionMap("XRI " + targetController.ToString() + " Locomotion" ).FindAction("Teleport Mode Activate");
        _teleportActivate.Enable();
        _teleportActivate.performed += OnTeleportActivate;

      
        _teleportCancel = inputAction.FindActionMap("XRI " + targetController.ToString() + " Locomotion").FindAction("Teleport Mode Cancel");
        _teleportCancel.Enable();
        _teleportCancel.performed += OnTeleportCancel;


       
        _thumbstickInputAction = inputAction.FindActionMap("XRI " + targetController.ToString() + " Locomotion").FindAction("Move");
        _thumbstickInputAction.Enable();
    }

    private void OnDestroy()
    {
        _teleportActivate.performed -= OnTeleportActivate;
        _teleportCancel.performed -= OnTeleportCancel;
    }
    
    void Update()
    {
        if (!_teleportIsActive)
        {
            return;
        }
        if (!rayInteractor.enabled)
        {
            return;
        }
        if (_thumbstickInputAction.triggered)
        {
            return;
        }
        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit raycastHit))
        {
            rayInteractor.enabled = false;
            _teleportIsActive = false;
            return;
        }

        TeleportRequest teleportRequest = new TeleportRequest()
        {
            destinationPosition = raycastHit.point,
        };

        teleportationProvider.QueueTeleportRequest(teleportRequest);

        rayInteractor.enabled = false;
        _teleportIsActive = false;
    }

   
    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        if (!_teleportIsActive)
        {
            rayInteractor.enabled = true;
            _teleportIsActive = true;
        }

    }

  
    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        if (_teleportIsActive && rayInteractor.enabled == true)
        {
            rayInteractor.enabled = false;
            _teleportIsActive = false;
        }

    }
}