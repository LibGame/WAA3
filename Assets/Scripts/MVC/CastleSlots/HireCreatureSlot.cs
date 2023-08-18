using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HireCreatureSlot : MonoBehaviour
{
    [SerializeField] private Image _icon;

    public void SetIcon(Sprite icon)
    {
        if (icon != null)
        {
            _icon.color = new Color(1, 1, 1, 1);
            _icon.sprite = icon;
        }
        
    }
}
