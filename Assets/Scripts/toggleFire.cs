using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleFire : MonoBehaviour
{
    [SerializeField] private GameObject _fire;
    private bool _isOn = true;
   


    public void ToggleFire()
    {
        if (_isOn)
        {
            _isOn = false;
            _fire.SetActive(false);
        }
        else
        {
            _isOn = true;
            _fire.SetActive(true);
        } 
    }
}
