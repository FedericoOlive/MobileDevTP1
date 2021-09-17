using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Player : MonoBehaviour
{
    public GameObject player;
    public List<Sprite> truckSprites = new List<Sprite>();
    public Image truckImage;
    public TextMeshProUGUI textMoney;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void AddScore(int amount)
    {
        textMoney.text = "$" + amount;
    }
}