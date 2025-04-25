using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpdate : MonoBehaviour
{
    public Image FrontHealth;
    public Image BackHealth;
    public float maxHealth = 500f;
    private float health;
    private float lerpTimer;
    public float delaySpeed = 2f;
    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthBar();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(UnityEngine.Random.Range(0, 10));
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            Regenerate(UnityEngine.Random.Range(0, 10));
        }
    }
    public void UpdateHealthBar()
    {
        Debug.Log(health);
       float fillFront = FrontHealth.fillAmount;
       float fillBack = BackHealth.fillAmount;
       float healthFraction = health / maxHealth;

       if (fillBack > healthFraction /*condition on taking damage (to be changed)*/)
       {
        fillFront = healthFraction;
        BackHealth.color = Color.red;
        lerpTimer += Time.deltaTime;
        float percentComplete = Mathf.Pow(lerpTimer * delaySpeed, 2);
        fillBack = Mathf.Lerp(fillBack, healthFraction, percentComplete);
       }

       if (fillFront < healthFraction /*condition on shooting (to be changed)*/)
       {
        fillBack = healthFraction;
        BackHealth.color = Color.cyan;
        lerpTimer += Time.deltaTime;
        float percentComplete = Mathf.Pow(lerpTimer * delaySpeed, 2);
        fillFront = Mathf.Lerp(fillFront, fillBack, percentComplete);
       }

       FrontHealth.fillAmount = fillFront;
       BackHealth.fillAmount  = fillBack;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }
    public void Regenerate(float recover)
    {
        health += recover;
        lerpTimer = 0f;
    }
}
