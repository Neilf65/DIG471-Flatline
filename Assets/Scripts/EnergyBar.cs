using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider energyBar;
    public PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        energyBar = GetComponent<Slider>();
        energyBar.maxValue = player.maxEnergy;
        energyBar.value = player.maxEnergy;

    }

    public void SetEnergy(int eg)
    {
        energyBar.value = eg;
    }
}
