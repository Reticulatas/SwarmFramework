using UnityEngine;

public static class PlayerHelper
{
    public static Quaternion GetGunDirection(PlayerCharacter character)
    {
        return Quaternion.LookRotation(character.camera.transform.forward);
    }

    public static void MakeBullet(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject.Instantiate(prefab, position, rotation);
    }
    public static void MakeBullet(GameObject prefab, Vector3 position, PlayerCharacter character)
    {
        GameObject.Instantiate(prefab, position, GetGunDirection(character));
    }
}