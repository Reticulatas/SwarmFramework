using UnityEngine;

public class GC_StandardGun : GC_Gun
{
    public float ReloadTime;
    public bool Reloading;

    public override void Fire(PlayerCharacter character)
    {
        if (Ammo <= 0)
        {
            Reload();
        }
    }

    public override void Reload()
    {
        Reloading = true;
        Invoke("ReloadDone", ReloadTime * ((MaxAmmo - Ammo) / MaxAmmo));
    }

    public virtual void ReloadDone()
    {
        Reloading = false;
        Ammo = MaxAmmo;
    }

    public virtual bool CanFire()
    {
        return !Reloading;
    }

}