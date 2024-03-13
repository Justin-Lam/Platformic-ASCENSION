using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon
{
	[SerializeField] int damage;												public int Damage => damage;
	[SerializeField][Tooltip("In attacks per second")] float attackSpeed;		public float AttackSpeed => attackSpeed;
}

[System.Serializable]
public class Sword : Weapon
{
	[SerializeField] float range;												public float Range => range;
}

[System.Serializable]
public class Gun : Weapon
{

}


public class Player : Unit
{
	[Header("Weapon Stats")]
	[SerializeField] Sword sword;		public Sword Sword => sword;
	[SerializeField] Gun gun;			public Gun Gun => gun;
}
