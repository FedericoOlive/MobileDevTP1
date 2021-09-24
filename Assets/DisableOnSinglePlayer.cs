using UnityEngine;
public class DisableOnSinglePlayer : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsDisableSinglePlayer;
    [SerializeField] private Camera[] camsP1ToExpand;
    [SerializeField] private GameObject[] camsP2ToDisable;

    private Rect rectCameraExpand = new Rect(0, 0, 1, 1);
    private void Start()
    {
        if (GameMaster.Get().IsSinglePlayer())
        {
            foreach (GameObject go in objectsDisableSinglePlayer)
            {
                go.SetActive(false);
            }
            foreach (GameObject camsP2 in camsP2ToDisable)
            {
                camsP2.SetActive(false);
            }
            foreach (Camera camsP1 in camsP1ToExpand)
            {
                camsP1.rect = rectCameraExpand;
            }
        }
    }
}