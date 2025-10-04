using UnityEngine;

public class MenuText : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    public void SetText(string text)
    {
        _text.text = text;
    }
}