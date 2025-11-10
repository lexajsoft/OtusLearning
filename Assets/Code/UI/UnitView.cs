using Code.Infrastructure.Services.Audio;
using Code.Infrastructure.Services.Input;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
  public sealed class UnitView : MonoBehaviour
  {
    [SerializeField] private Image heroImage;
    [SerializeField] private TMP_Text stats;
    [SerializeField] private Button button;
    
    [Header("Active")] 
    [SerializeField, Space] private Image activeImage;
    [SerializeField] private Sprite activeIcon;
    [SerializeField] private Sprite inactiveIcon;
    [SerializeField] private GameObject activeBlur;
    
    [Header("Attack")] 
    [SerializeField] private RectTransform center;
    [SerializeField] private float forwardDuration = 0.2f;
    [SerializeField] private AnimationCurve attackCurve;
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float backDuration = 0.5f;
    [SerializeField] private AudioClip punchSFX;

    private Sequence attackAnimation;
    private AudioPlayer audioPlayer;
    private IInputService _input;
    
    private void Start()
    {
      audioPlayer = AudioPlayer.Instance;
    }

    public void SetIcon(Sprite icon)
    {
      heroImage.sprite = icon;
    }

    public void SetStats(string stats)
    {
      this.stats.text = stats;
    }

    public void SetActive(bool isActive)
    {
      Debug.Log("UnitView Unselect");
      activeImage.sprite = isActive ? activeIcon : inactiveIcon;
      activeBlur.SetActive(isActive);
    }

    public UniTask AnimateAttack(UnitView target)
    {
      if (attackAnimation != null)
      {
        return UniTask.CompletedTask;
      }

      UniTaskCompletionSource tcs = new UniTaskCompletionSource();

      Vector3 sourcePosition = center.position;
      Vector3 targetPosition = target.center.position;

      attackAnimation = DOTween
        .Sequence()
        .Append(center.DOMove(targetPosition, forwardDuration).SetEase(attackCurve))
        .Join(center.DOScale(1.25f, forwardDuration).SetEase(scaleCurve))
        .AppendCallback(() => audioPlayer.PlaySound(punchSFX))
        .Append(center.DOMove(sourcePosition, backDuration))
        .Join(center.DOScale(1, backDuration))
        .OnComplete(() =>
        {
          attackAnimation = null;
          tcs.TrySetResult();
        });

      return tcs.Task;
    }
  }
}