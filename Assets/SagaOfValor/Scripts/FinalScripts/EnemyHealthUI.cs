using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    private float healthBarVal = 1;
    public float HealthBar
    {
        get { return healthBarVal; }
        set
        {
            healthBarVal = value;
            transform.Find("health").GetComponent<Image>().fillAmount = healthBarVal;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<Canvas>().worldCamera = Camera.main;
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
