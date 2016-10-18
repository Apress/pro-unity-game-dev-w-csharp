//------------------------------------------------
using UnityEngine;
using System.Collections;
//------------------------------------------------
public class Weapon : MonoBehaviour
{
	//Custom enum for weapon types
	public enum WEAPON_TYPE {Punch=0, Gun=1};

	//Weapon type
	public WEAPON_TYPE Type = WEAPON_TYPE.Punch;

	//Damage this weapon causes
	public float Damage = 0.0f;

	//Range of weapon (linear distance outwards from camera) measured in world units
	public float Range = 1.0f;

	//Amount of ammo remaining (-1 = infinite)
	public int Ammo = -1;

	//Recovery delay
	//Amount of time in seconds before weapon can be used again
	public float RecoveryDelay = 0.0f;

	//Has this weapon been collected?
	public bool Collected = false;

	//Is this weapon currently equipped on player
	public bool IsEquipped = false;

	//Can this weapon be fired
	public bool CanFire = true;

	//Next weapon in cycle
	public Weapon NextWeapon = null;
}
//------------------------------------------------
