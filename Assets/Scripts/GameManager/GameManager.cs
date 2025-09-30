using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
            SceneManager.LoadScene(0);
        }
    }
}