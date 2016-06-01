using UnityEngine;
using System.Collections;

public class DealDamage : MonoBehaviour
{
    public int Amount = 10;
    public bool DestroyOnDealDamage = false;
    public GameObject HitSpawn;

    public enum HitType
    {
        Friendlies,
        Enemies
    };
    public HitType HitWhat = HitType.Friendlies;

    void OnTriggerEnter(Collider c)
    {
        var tag = HitWhat == HitType.Enemies ? "Enemy" : "Friend";
        if (c.gameObject.tag == tag)
        {
            var entity = c.gameObject.GetComponent<Entity>();
            if (entity == null)
                Debug.LogError("Damagable must be entity");
            entity.DealDamage(Amount);

            if (HitSpawn)
                Instantiate(HitSpawn, c.bounds.center, Quaternion.identity);
            if (DestroyOnDealDamage)
                Destroy(gameObject);

        }
    }
}
