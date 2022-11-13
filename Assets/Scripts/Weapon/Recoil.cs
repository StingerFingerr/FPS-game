using System;
using Character;
using Services.Input;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Weapon
{
    public class Recoil: MonoBehaviour
    {
        public float recoilSmooth;
        public float recoilAmountX;
        public float recoilMinAmountY;
        public float recoilMaxAmountY;
        public Vector3 recoilPositionOffset;
        public Vector3 recoilRotationOffset;
        public float aimingRecoilAmountModifier;
        public Weapon weapon;
        
        private Vector3 _recoilPosOffset;
        private Vector3 _recoilRotOffset;


        private void OnEnable() => 
            Subscribe();

        private void Subscribe()
        {
            weapon.OnShot += SetRecoilAmount;
        }
    
        private void FixedUpdate()
        {
            CalculateRecoil();
        }

        private void SetRecoilAmount()
        {
            _recoilPosOffset = recoilPositionOffset * AimingRecoilModifier();
            _recoilRotOffset = recoilRotationOffset * AimingRecoilModifier();
        }

        public Vector2 GetRecoilAmount()
        {
            Vector2 recoil = Vector2.zero;
            recoil.x = Random.Range(-recoilAmountX, recoilAmountX);
            recoil.y = Random.Range(recoilMinAmountY, recoilMaxAmountY);

            recoil *= AimingRecoilModifier();

            return recoil;
        }

        private void CalculateRecoil()
        {
            _recoilPosOffset = Vector3.Lerp(_recoilPosOffset, Vector3.zero, Time.fixedDeltaTime * recoilSmooth);
            _recoilRotOffset = Vector3.Lerp(_recoilRotOffset, Vector3.zero, Time.fixedDeltaTime * recoilSmooth);

            transform.localPosition += _recoilPosOffset;
            transform.localRotation *= quaternion.Euler(_recoilRotOffset);
        }

        private float AimingRecoilModifier() => 
            weapon.IsAiming ? aimingRecoilAmountModifier : 1;
    }
}