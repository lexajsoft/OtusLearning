using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public enum DirectType
{
    Next,
    Prev
}

public class BackgroundLoader : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private GameObject _loading;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _prevButton;

    [SerializeField] private List<string> _images;
    private Sprite _loadedFromAddressables = null;
    private int _index = 0;
    private bool _isLoading = false;
    private CancellationToken _cancellationToken;
    
    private async void Start()
    {
        _nextButton.onClick.AddListener(LoadNextPicture);
        _prevButton.onClick.AddListener(LoadPrevPicture);

        

        // получение мета данных из адресаблов по лейблу и указанному типу
        _images = await AddressablesHelper.GetAssetNamesByLabel<Sprite>("Background");
        
        // загружает текущее изображение
        LoadBackgroundByCurrentIndex().Forget();
    }

    private void LoadPrevPicture()
    {
        SwitchImage(DirectType.Prev);
    }

    private void LoadNextPicture()
    {
        SwitchImage(DirectType.Next);
    }

    private async void SwitchImage(DirectType directType)
    {
        if(_isLoading)
            return;
        
        switch (directType)
        {
            case DirectType.Next:
                NextIndex();
                break;
            case DirectType.Prev:
                PrevIndex();
                break;
        }
        await LoadBackgroundByCurrentIndex();
    }

    private async UniTask LoadBackgroundByCurrentIndex()
    {
        _cancellationToken = new CancellationToken();
        _isLoading = true;
        _loading.gameObject.SetActive(true);
        
        var nameSprite = GetCurrentImageName();
        Debug.Log("Начинается загрузка изображения из адресаблов:" + nameSprite);
        
        var result = await AddressablesHelper.LoadAsync<Sprite>(nameSprite, _cancellationToken).SuppressCancellationThrow();
        if (result.IsCanceled == false)
        {
            Debug.Log("Загрузка завершена");
            SetNewBackground(result.Result);
        }
        else
        {
            Debug.Log("Загрузка отменена");
        }
        
        _loading.gameObject.SetActive(false);
        _isLoading = false;
    }

    private void SetNewBackground(Sprite sprite)
    {
        ReleaseOldAsset();

        _loadedFromAddressables = sprite;
        _background.sprite = _loadedFromAddressables;
    }

    private void ReleaseOldAsset()
    {
        if (_loadedFromAddressables != null)
            Addressables.Release(_loadedFromAddressables);
    }

    private string GetCurrentImageName()
    {
        return _images[_index];
    }

    private void NextIndex()
    {
        _index += 1;
        _index %= _images.Count;
    }
    
    private void PrevIndex()
    {
        _index -= 1;
        if(_index< 0)
            _index = _images.Count-1;
    }
}
