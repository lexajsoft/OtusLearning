using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootEmUp
{
    public interface IGameManager
    {
        Player Player { get; }
        event Action<Player> OnPlayerChanged;
        void SetCharacterGameObject(Player player);
        void FinishGame();
    }

    public sealed class GameManager : IGameManager
    {
        public Player Player { get; private set; }

        public event Action<Player> OnPlayerChanged; 
        public void SetCharacterGameObject(Player player)
        {
            Player = player;
            OnPlayerChanged?.Invoke(Player);
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            SceneManager.LoadScene(0);
        }
    }
}