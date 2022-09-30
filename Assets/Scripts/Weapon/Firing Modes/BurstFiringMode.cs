using System;
using System.Collections;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Weapon.Firing_Modes
{
    public class BurstFiringMode: BaseFiringMode
    {
        public int burstSize = 2;
        public float delayBetweenShots = .1f;

        private IInputService _input;
        
        private float _nextShotTimer;
        private bool _nextShotAvailable = true;
        private bool _nextBurstAvailable = true;

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
            if (_nextBurstAvailable && _nextShotAvailable)
                StartCoroutine(ShootBurst(fireCallback));
        }

        private IEnumerator ShootBurst(Action fireCallback)
        {
            _nextShotAvailable = false;
            _nextBurstAvailable = false;
            
            for (int i = 0; i < burstSize; i++)
            {
                fireCallback?.Invoke();
                yield return new WaitForSeconds(delayBetweenShots);
            }

            _nextBurstAvailable = true;
        }
    }
}