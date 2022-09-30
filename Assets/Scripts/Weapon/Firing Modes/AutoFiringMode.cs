using System;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Weapon.Firing_Modes
{
    public class AutoFiringMode: BaseFiringMode
    {
        public float firingRate;
        
        private float _nextShotTimer;

        private void Update()
        {
            if (_nextShotTimer >= 0)
                _nextShotTimer -= Time.deltaTime;
        }
        
        
        public override void TryShoot(Action fireCallback)
        {
            if(_nextShotTimer>0)
                return;

            _nextShotTimer = firingRate;
            fireCallback?.Invoke();
        }
    }
}