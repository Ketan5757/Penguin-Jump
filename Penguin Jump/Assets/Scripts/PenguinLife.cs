using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PenguinLife : MonoBehaviour
{
   private Animator anim;
   private Rigidbody2D penguinRigidbody;
   [SerializeField] private AudioSource deathSoundEffect;

   private void Start()
    {
        penguinRigidbody = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Trap") && !ItemCollector.isShieldActive)
        {
            Die();
        }
    }

    private void Die()
    {
        deathSoundEffect.Play();
        penguinRigidbody.bodyType = RigidbodyType2D.Static;
        PlayerMovement.topScore = 0;
        anim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);; //only works as we don't save the progress. but it's ok for the prototype!
    }   
}
