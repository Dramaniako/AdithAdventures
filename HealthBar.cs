using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public Color healthyColor = Color.green;
    public Color mediumColor = Color.yellow;
    public Color lowColor = Color.red;
    public float transitionDuration = 1f;
    public float mediumHealthThreshold = 50f;
    public float lowHealthThreshold = 20f;
    public float maxHealth, currentHealth = 100f;
    private Color targetColor;
    private float transitionTimer;


    void Start()
    {
        targetColor = healthyColor;
        healthBarImage.color = healthyColor;    
        RectTransform startSize = healthBarImage.GetComponent<RectTransform>();
        startSize.sizeDelta = new Vector2(maxHealth, startSize.sizeDelta.y);
    }

    void Update()
    {
        if (healthBarImage != null && transitionTimer < transitionDuration)
        {
            healthBarImage.color = Color.Lerp(healthBarImage.color, targetColor, transitionTimer / transitionDuration);
            transitionTimer += Time.deltaTime;
        }        
    }

    public void SetHealth(float health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
        healthBarImage.fillAmount = currentHealth / maxHealth;
        UpdateColor();
        transitionTimer = 0f;
        // Image image = GetComponent<Image>();
        RectTransform rectTransform = healthBarImage.GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2(1f, 0f);
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x - 50f, rectTransform.sizeDelta.y);
    }

    private void UpdateColor()
    {
        if (currentHealth <= maxHealth * lowHealthThreshold)
        {
            targetColor = lowColor;
        }
        else if (currentHealth <= maxHealth * mediumHealthThreshold)
        {
            targetColor = mediumColor;
        }
        else
        {
            targetColor = healthyColor;
        }
    }

    
}
