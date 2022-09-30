using System;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Weapon.Firing_Modes
{
    public class SingleFiringMode: BaseFiringMode
    {
        public float delayBetweenShots;

        private IInputService _input;
        
        private float _nextShotTimer;
        private bool _nextShotAvailable = true;

        [Inject]
        private void Construct(IInputService input)
        {
            _input = input;
            
            Subscribe();
        }

        private void Subscribe()
        {
            _input.FinishFiring += () => _nextShotAvailable = true;
        }
        
        private void Update()
        {
            if (_nextShotTimer >= 0)
                _nextShotTimer -= Time.deltaTime;
        }

        public override void TryShoot(Action fireCallback)
        {
            if(_nextShotTimer>0)
                return;

            if(_nextShotAvailable is false)
                return;
            
            _nextShotTimer = delayBetweenShots;
            _nextShotAvailable = false;

            fireCallback?.Invoke();
        }
        
    }
}