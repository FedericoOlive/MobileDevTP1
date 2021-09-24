using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviourSingleton<GameMaster>
{
    [SerializeField] private bool singlePlayer;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void SetSinglePlayer(bool value) => singlePlayer = value;
    public bool IsSinglePlayer() => singlePlayer;
    
    public void GameLoaded()
    {

       // objectsOff = FindObjectsOfType<ObjectOff>();
       // if (singlePlayer)
       // {
       //
       //     Rect rect = cam.rect;
       //     rect.x = 0;
       //     rect.y = 0;
       //     rect.width = 1;
       //     rect.height = 1;
       //     cam.rect = rect;
       // }

    }
    
}