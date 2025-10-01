using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootEmUp
{
    public sealed class GameManager
    {
        public GameObject Character { get; private set; }

        public Action<GameObject> OnCharacterChanged; 
        public void SetCharacterGameObject(GameObject character)
        {
            Character = character;
            OnCharacterChanged?.Invoke(Character);
        }

        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
            SceneManager.LoadScene(0);
        }
    }
}