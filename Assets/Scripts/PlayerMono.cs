using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerMono : MonoBehaviour
{
    [Inject][SerializeField] private Player _player = null;

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        _player.CreateRandomInventory();
    }
}