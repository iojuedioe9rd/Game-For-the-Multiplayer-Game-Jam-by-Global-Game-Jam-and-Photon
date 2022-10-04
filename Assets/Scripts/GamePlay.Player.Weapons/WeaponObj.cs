using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Player.Weapons
{
    public enum WeaponType
    {
        ex,
        s
    }

    [CreateAssetMenu()]
    public class WeaponObj : ScriptableObject
    {
        public WeaponType weaponType;
    }
}
