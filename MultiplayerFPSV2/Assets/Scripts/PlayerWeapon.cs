using UnityEngine;

[System.Serializable]
public class PlayerWeapon{

    public string name = "Glock";

    public int damage = 10;
    public float range = 200f;

    public float fireRate = 0f;

    public int maxBullets = 30;
    [HideInInspector]
    public int bullets;

    public GameObject graphics;

    public float reloadTime = 1f;

    public PlayerWeapon()
    {
        bullets = maxBullets;
    }

}
