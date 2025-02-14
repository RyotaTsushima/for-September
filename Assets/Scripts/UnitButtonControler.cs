using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButtonControler : MonoBehaviour
{
    [SerializeField] Button[] _childrenButton;

    public void ButtonActivate(bool _activate)
    {
        if( _activate)
        {
            foreach (var button in _childrenButton)
            {
                button.enabled = true;
            }
        }
        else
        {
            foreach (var button in _childrenButton)
            {
                button.enabled = false;
            }
        }
    }
}
