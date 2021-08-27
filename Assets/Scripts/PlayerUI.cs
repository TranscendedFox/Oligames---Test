using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private float _startingHp;
    [SerializeField] private Image _hpBar;
    [SerializeField] private float _maxHP;
    private bool _takingDamage;

    private bool _isControlsUp;
    [SerializeField] private Transform _ControlsUI;
    
    
    void Start()
    {
        _takingDamage = false;
        _hpBar.rectTransform.localScale = new Vector3(_startingHp, 1, 1);
        _hp = _startingHp;
        _isControlsUp = false;
        _ControlsUI.gameObject.SetActive(false);
    }
    
    void Update()
    {
        if (_takingDamage && _hp > 0)
        {
            _hp -= Time.deltaTime;
            _hpBar.rectTransform.localScale = new Vector3(_hp, 1, 1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            _takingDamage = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            _takingDamage = false;
        }
    }

    public void addHP(float hpAdded)
    {
        if ((_hp + hpAdded) > _maxHP)
        {
            _hp = _maxHP;
            _hpBar.rectTransform.localScale = new Vector3(_hp, 1, 1);
        }
        else
        {
            _hp += hpAdded;
            _hpBar.rectTransform.localScale = new Vector3(_hp, 1, 1);
        }
    }

    public void ToggleControls(InputAction.CallbackContext value)
    {
        if (!_isControlsUp)
        {
            _ControlsUI.gameObject.SetActive(true);
            _isControlsUp = true;
        }
        else
        {
            _ControlsUI.gameObject.SetActive(false);
            _isControlsUp = false;
        }
    }
}
