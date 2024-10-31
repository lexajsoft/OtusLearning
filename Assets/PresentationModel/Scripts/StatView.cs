using UnityEngine;

namespace PresentationModel.Scripts
{
    public class StatView : ViewBase
    {
        [SerializeField] private TMPro.TextMeshProUGUI _text;

        public void SetData(string name, string value)
        {
            _text.text = $"{name}: {value}";
        }
    }
}
