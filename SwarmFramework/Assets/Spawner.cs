using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : Entity 
{
    public List<GameObject> Spawns;

    int active;

    // Number below which to spawn more actives
    public int SpawnThreshold;
    public int SpawnMin, SpawnMax;

	void Start () {
        StartCoroutine(Do());
	}
	
	IEnumerator Do() {
	    while (true)
        {
            if (active <= SpawnThreshold)
                yield return StartCoroutine(Spawn());
	        yield return new WaitForSeconds(1);
	    }
	}

    IEnumerator Spawn()
    {
        int num = Random.Range(SpawnMin, SpawnMax);
        for (int i = 0; i < num; ++i)
        {
            var monster = (GameObject)Instantiate(Spawns[Random.Range(0, Spawns.Count)], gameObject.transform.position, Quaternion.identity);
            monster.GetComponent<Entity>().OnDeath += () =>
            {
                --active;
            };
            ++active;
            yield return null;
        }
    }
}
