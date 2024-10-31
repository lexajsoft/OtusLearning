using UnityEngine;
using UnityEngine.UI;

namespace PresentationModel.Scripts
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _valueImage;
    
        [SerializeField] private bool _isShowText;
        [SerializeField] private TMPro.TextMeshProUGUI _progressText;
        [SerializeField] private string _beforeText;
    
        [SerializeField] private int _valueMax;
        [SerializeField] private int _value;

        public void SetValueMax(int value)
        {
            _valueMax = value;
            if (_valueMax < 0)
            {
                _valueMax = 0;
            }
            UpdateVisual();
        }
        public void SetValue(int value)
        {
            _value = value;
            if (_value < 0)
            {
                _value = 0;
            }

            UpdateVisual();
        }


        private void OnValidate()
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            _valueImage.fillAmount = (float) _value / _valueMax;
            if (_isShowText)
            {
                _progressText.text = _beforeText + $"{_value.ToString()} / {_valueMax.ToString()}";
            }
        }
    }
}
