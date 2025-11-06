using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HomeWorkTask2
{
    public class ChestMono : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMPro.TextMeshProUGUI _remainTimeText;
        [SerializeField] private TMPro.TextMeshProUGUI _nameChestText;
        [SerializeField] private GameObject _openChestButtonGameObject;
        [SerializeField] private GameObject _timerTextAreaGameObject;
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _canOpenChestCanvasgroupValue = 1;
        [SerializeField] private float _cannotOpenChestCanvasgroupValue = 0.4f;

        private Icons _icons;
        
        public Chest _chest;

        public Action<Chest> OnTryOpen;
        private void Start()
        {
            _button.onClick.AddListener(Open);
        }

        public void SetIcons(Icons icons)
        {
            _icons = icons;
        }

        public void SetChest(Chest chest)
        {
            _chest = chest;
            _nameChestText.text = _chest.NameChest;
            _image.sprite = _icons.GetSprite(chest.ChestType.ToString());
            _chest.OnTimerStarted += ChestOnOnTimerStarted;

            if (_chest.IsCanOpen())
            {
                ShowButton();
            }
            else
            {
                ChestOnOnTimerStarted();
            }
        }

        private void ChestOnOnTimerStarted()
        {
            StartCoroutine(WaitingForOpenChest());
        }

        private IEnumerator WaitingForOpenChest()
        {
            ShowTimer();

            while (_chest.IsCanOpen() == false)
            {
                var remainSeconds = _chest.Timer.GetRemainTimeSeconds(ServerTime.GetCurrentTime());
                //Debug.Log($"{_chest.NameChest} Remain:[{remainSeconds}]");
                _remainTimeText.text = SecondsToFormat(remainSeconds);
                
                yield return new WaitForSeconds(1);
            }
            
            ShowButton();
        }

        private void ShowTimer()
        {
            _openChestButtonGameObject.SetActive(false);
            _timerTextAreaGameObject.SetActive(true);
            _canvasGroup.alpha = _cannotOpenChestCanvasgroupValue;
        }

        private void ShowButton()
        {
            _canvasGroup.alpha = _canOpenChestCanvasgroupValue;
            _openChestButtonGameObject.SetActive(true);
            _timerTextAreaGameObject.SetActive(false);
        }


        private string SecondsToFormat(double seconds)
        {
            // variant #1
            var timeSpan = TimeSpan.FromSeconds(seconds);
            return $"{timeSpan.TotalHours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
        
            // variant #2
            return $"{(seconds / 3600):00}:{(seconds / 60 % 60):00}:{(seconds % 60):00}";
        }


        private void Open()
        {
            OnTryOpen?.Invoke(_chest);
        }
    }
}