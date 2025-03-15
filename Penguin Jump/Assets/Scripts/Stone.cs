using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{

    [SerializeField] private GameObject[] stonePrefab;
    [SerializeField] private float secondSpawn = 2f;
    [SerializeField] private float minTras;
    [SerializeField] private float maxTras;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(StoneSpawn());
    }

    private IEnumerator StoneSpawn() {
        while(true) {
            if(PlayerMovement.topScore > 60.0f) {
                secondSpawn = 5.0f;
            }
            if(PlayerMovement.topScore > 100.0f) {
                secondSpawn = 7.0f;
            }
            var wanted = Random.Range(minTras, maxTras);
            var position = new Vector3(wanted, transform.position.y);
            var stoneGameObject = Instantiate(stonePrefab[Random.Range(0, stonePrefab.Length)], //never use gameObject as a variable!
            position, Quaternion.identity);
            yield return new WaitForSeconds(secondSpawn);
            Destroy(stoneGameObject, 5f);
        }
    }
}
