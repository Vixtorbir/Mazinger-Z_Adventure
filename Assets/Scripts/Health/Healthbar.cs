using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerhealth;
    [SerializeField] private Image totalhealthbar;
    [SerializeField] private Image currenthealthbar;

    private void Start()
    {
        totalhealthbar.fillAmount = playerhealth.currentHealth / 10;
        
    }

    private void Update()
    {
        currenthealthbar.fillAmount = playerhealth.currentHealth /10;
    }
}
