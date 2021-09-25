using UnityEngine;
using UnityEngine.UI;
public class Ui_Dificult : MonoBehaviour
{
    private int dificult;
    public Slider sliderDificult;
    
    public void UpdateDificult()
    {
        GameMaster.Get().dificult = (int)sliderDificult.value;
    }
}