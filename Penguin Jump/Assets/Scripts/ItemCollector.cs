using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int fishes = 0;
    private float duration = 5f;
    public static bool isShieldActive = false;
    [SerializeField] private Text pointsText; 
    [SerializeField] private AudioSource collectingItemEffect;
    [SerializeField] private ParticleSystem shield;

    private void Start()
    {
        shield.Stop();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {collectingItemEffect.Play();
        Destroy(collision.gameObject);
        fishes++;
        PlayerMovement.fishCollecter();
        }

        if(collision.gameObject.CompareTag("Shield"))
        {
            isShieldActive = true;
            StartCoroutine(ActivateShiled());
            Destroy(collision.gameObject);
        }
    }

     IEnumerator ActivateShiled()
    {
        shield.Play();

        yield return new WaitForSeconds(duration);
        
        isShieldActive = false;
        shield.Stop();
    }
}
