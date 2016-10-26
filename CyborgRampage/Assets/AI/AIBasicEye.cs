using UnityEngine;
using System.Collections;

public class AIBasicEye : AITurretDetectAndShoot {

    private bool bigger_projectile = false;
    private int bigger_projectile_perc_chance = 1;
    private BasicGun _bpWeapon;

	// Use this for initialization
	public override void Start () {

        base.Start();

        BasicGun[] weapons = GetComponents<BasicGun>();
        for (int i = 0; i < weapons.Length; ++i)
        {
            BasicGun weapon = weapons[i];
            if (weapon == null) continue;
            if (weapon.name == "secondary_weapon")
                _bpWeapon = weapon;
        }
    }
	
	// Update is called once per frame
	public override void Update () {

        float bp_chance = Random.Range(0, 100);
        bigger_projectile = (bp_chance < bigger_projectile_perc_chance);
        if (bigger_projectile)
            _bpWeapon.Fire(true);
        base.Update();
	}
}
