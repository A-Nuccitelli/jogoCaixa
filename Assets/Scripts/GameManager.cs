using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject hazardPrefab;

    public int maxHazardsToSpawn = 3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnHazards());  
    }

    private IEnumerator SpawnHazards()
    {
        var hazardToSpaw = Random.Range(1, maxHazardsToSpawn);
        for (int i = 0; i < hazardToSpaw; i++)
        {
            var x = Random.Range(-7, 7);
            var drag = Random.Range(0f, 3f);
            var hazard = Instantiate(hazardPrefab, new Vector3(x, 11, 0), Quaternion.identity);
            hazard.GetComponent<Rigidbody>().drag = drag;
        }

        var timeToWait = Random.Range(0.3f, 1.8f);

        yield return new WaitForSeconds(timeToWait);

        yield return SpawnHazards();
    }
}
