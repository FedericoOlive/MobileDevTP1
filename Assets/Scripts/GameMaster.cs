using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviourSingleton<GameMaster>
{
    [SerializeField] private bool singlePlayer;
    public int dificult;

    public void SetSinglePlayer(bool value) => singlePlayer = value;
    public bool IsSinglePlayer() => singlePlayer;
    public void GameLoaded()
    {

    }
}