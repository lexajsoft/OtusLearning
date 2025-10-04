using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private Button _button;

    public Action OnClicked;
    private void OnEnable()
    {
        _button.onClick.AddListener(Click);
    }
    private void OnDisable()
    {
        _button.onClick.RemoveListener(Click);
    }

    private void Click()
    {
        OnClicked?.Invoke();
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}