using UnityEngine;

public class MenuDoubleText : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _text1;
    [SerializeField] private TMPro.TextMeshProUGUI _text2;

    public void SetText1(string text)
    {
        _text1.text = text;
    }

    public void SetText2(string text)
    {
        _text2.text = text;
    }
}