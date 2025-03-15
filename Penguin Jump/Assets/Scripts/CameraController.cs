using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Transform Background; //what is background1 and background? lowercase

    public Transform Background1;

    private float size;
   
   [SerializeField]private Transform player;

   private void Start()
    {
        size = Background.GetComponent<BoxCollider2D>().size.y;

    }

    private void Update()
    {
        var playerPosition = player.position;
        var newPlayerPosition = new Vector3(playerPosition.x,playerPosition.y,playerPosition.z);

        //Camera
        var targetPosition = new Vector3(target.position.x, target.position.y, newPlayerPosition.z);

        newPlayerPosition = Vector3.Lerp(newPlayerPosition, targetPosition, 0.2f);
        transform.position = newPlayerPosition;

        //Background
        if(transform.position.y >= Background1.position.y)
        {
            var backgroundPosition = Background.position;
            backgroundPosition = new Vector3(backgroundPosition.x, Background1.position.y + size, backgroundPosition.z);
            Background.position = backgroundPosition;
            SwitchBg();
        }

    }
    private void SwitchBg()
    {
        (Background, Background1) = (Background1, Background);
    }
}
