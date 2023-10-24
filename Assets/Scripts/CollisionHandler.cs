using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Hit!!");
            gameObject.SetActive(false);
            
            GetComponentInParent<TouchDetection>().StopMovement();
            GetComponentInParent<TouchDetection>().enabled = false;

            DOVirtual.DelayedCall(1.5f, () =>
            {
                SceneManager.LoadScene(0);
            });
        }
    }
}
