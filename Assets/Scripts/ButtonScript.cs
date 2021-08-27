using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private UnityEvent OnButton;
    public bool buttonPressed = false;

    void Update()
    {
        if (buttonPressed)
        {
            OnButton.Invoke();
            buttonPressed = false;
        }
    }
}
