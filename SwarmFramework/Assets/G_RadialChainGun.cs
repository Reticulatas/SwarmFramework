using UnityEngine;
using System.Collections;

public class G_RadialChainGun : GC_StandardGun
{
    public GameObject bullet;

    public override void Fire(PlayerCharacter character) 
    {
        if (!CanFire())
            return;

        Ammo -= 1;

        PlayerHelper.MakeBullet(bullet, transform.position, character);
        PlayerHelper.MakeBullet(bullet, transform.position, character);
        PlayerHelper.MakeBullet(bullet, transform.position, character);
        PlayerHelper.MakeBullet(bullet, transform.position, character);
        PlayerHelper.MakeBullet(bullet, transform.position, character);

        base.Fire(character);
    }
}
