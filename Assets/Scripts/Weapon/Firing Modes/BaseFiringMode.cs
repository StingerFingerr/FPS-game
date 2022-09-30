using System;
using UnityEngine;

namespace Weapon.Firing_Modes
{
    public abstract class BaseFiringMode: MonoBehaviour
    {
        public abstract void TryShoot(Action fireCallback);
    }
}