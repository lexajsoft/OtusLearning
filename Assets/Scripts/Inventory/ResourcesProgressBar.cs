using UnityEngine;
using UnityEngine.UI.ProceduralImage;

namespace Inventory
{
    public class ResourcesProgressBar : MonoBehaviour
    {
        [SerializeField] private ProceduralImage _proceduralImage;
        [SerializeField] private TMPro.TextMeshProUGUI _progressText;

        public void SetData(int current, int max)
        {
            if (current < 0)
                current = 0;
            if (current > max)
                current = max;

            if (current == 0 && max == 0)
            {
                _proceduralImage.fillAmount = 0f;
            }
            else
            {
                _proceduralImage.fillAmount = (float) current / max;    
            }
            
            _progressText.text = $"{current}/{max}";
        }
        
        public void SetData(DynamicResource dynamicResource)
        {
            int current = dynamicResource.CurrentValue; 
            int max = dynamicResource.MaxValue;
            SetData(current, max);
        }
    }
}