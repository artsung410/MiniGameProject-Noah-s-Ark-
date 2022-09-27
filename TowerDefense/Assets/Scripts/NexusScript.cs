using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NexusScript : LivingEntity
{
    public Slider HealthSlider;



    private void OnEnable()
    {
        HealthSlider.maxValue = Health;
        HealthSlider.value = Health;
    }

    private void Update()
    {
        if (Health <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }


    public override void TakeDamage(int damage)
    {
        HealthSlider.value = Health;
        Health -= damage;
    }

    
}
