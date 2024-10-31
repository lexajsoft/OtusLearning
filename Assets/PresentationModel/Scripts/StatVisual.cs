using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatVisual : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _text;

    public void SetData(string name, string value)
    {
        _text.text = $"{name}: {value}";
    }
}
