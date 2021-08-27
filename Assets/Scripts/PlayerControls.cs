using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class PlayerControls : MonoBehaviour
{
    // Param
    private Animator _playerAnimations;
    private Rigidbody _rigidbody;
    private Vector3 _playerDirection;
    private bool _isMoving;

    private int _inputCounter;
    private float _startingMovingSpeed;

    [Header("Player Movement")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _sprintMultiplier;
    [Header("Rig")]
    [SerializeField] private Transform _rightArmIK;
    [SerializeField] private ChainIKConstraint _rigIK;

    RaycastHit _ray;

    //Interaction
    private bool _isReachingButton;
    private float _reachTarget;    
    [Header("Button Interactions")]
    [SerializeField] private float _interactionRange;
    [SerializeField] private float _reachSpeed;

    void Start()
    {
        
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _playerAnimations = gameObject.GetComponent<Animator>();
        _startingMovingSpeed = _movementSpeed;
        _inputCounter = 0;
    }

    private void Update()
    {
        if (_isReachingButton)
        {
            _rigIK.weight = SmoothTransition(_rigIK.weight, _reachTarget);
        }
    }

    void FixedUpdate()
    {
        if (_isMoving)
        {
            PlayerMovement();
            if (_inputCounter <= 0)
            {
                _isMoving = false;
                CancelSprint();
            }
        }
    }

    //Movement ------------------------------------------------------

    public void OnMovement(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            _playerAnimations.SetBool("IsMoving", true);
            _isMoving = true;
            _inputCounter++;
        }       
        
        Vector2 inputDirection = value.ReadValue<Vector2>();
        _playerDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
                
        if (value.canceled)
        {
            Debug.Log(_inputCounter);
            _inputCounter--;
            if (_inputCounter <= 0)
            {
                _inputCounter = 0;
                _playerAnimations.SetBool("IsMoving", false);
            }

        }
    }

    public void OnSprint(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            _playerAnimations.SetBool("IsSprinting", true);
            _movementSpeed *= _sprintMultiplier;
        }        

        else if (value.canceled)
        {
            CancelSprint();         
        }
    }

    private void CancelSprint()
    {
        _playerAnimations.SetBool("IsSprinting", false);
        _movementSpeed = _startingMovingSpeed;
    }    

    private void PlayerMovement()
    {
        //Rotate Player
        if (_playerDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                  Quaternion.LookRotation(_playerDirection),
                                                  _rotationSpeed);
        }
        
        //Move Player
        _rigidbody.velocity = _playerDirection * _movementSpeed;
    }


    //Button ---------------------------------------------------
    public void OnInteraction(InputAction.CallbackContext value)
    {
        if (value.started)
        {//Shoots a ray in front of the player, 
        //if it hits a button start button animation and functions
            RaycastHit hit;
            Ray checkForInteractions = new Ray(new Vector3(transform.position.x,
                                                           transform.position.y + 1,
                                                           transform.position.z),
                                                           transform.forward);           
            if (Physics.Raycast(checkForInteractions, out hit, _interactionRange))
            {
                if (hit.collider.gameObject.layer == 6 && !_isReachingButton)
                {//if ray hit the Button (by layer)
                    hit.collider.gameObject.GetComponent<ButtonScript>().buttonPressed = true;
                    //Activate button functions

                    _rightArmIK.transform.position = hit.collider.transform.position;
                    // target position for the hand

                    StartCoroutine("PressButton");
                }
            }            
        }
        
    }   

    private IEnumerator PressButton()
    {
        _isReachingButton = true;
        _reachTarget = 1f;
        yield return new WaitForSeconds(.25f);
        _reachTarget = 0f;
        yield return new WaitForSeconds(.25f);
        _rigIK.weight = 0;
        _isReachingButton = false;
    }

    private float SmoothTransition(float a, float target)
    {
        return Mathf.Lerp(a, target, Time.deltaTime * _reachSpeed);
    }
}
