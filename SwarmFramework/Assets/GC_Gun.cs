using UnityEngine;

public abstract class GC_Gun : MonoBehaviour
{
    public abstract void Fire(PlayerCharacter character);
    public abstract void Reload();

    public float Ammo;
    public float MaxAmmo;
}