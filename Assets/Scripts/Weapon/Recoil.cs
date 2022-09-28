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
        private Vector3 _recoilPosOffset;
        private Vector3 _recoilRotOffset;

        private CharacterControllerScript _player;
        private IInputService _input;
    
        [Inject]
        private void Construct(CharacterControllerScript player, IInputService input)
        {
            _player = player;
            _input = input;
        
            Subscribe();
        }

        private void Subscribe()
        {
            _input.Fire += SetRecoilAmount;
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
            if (_player.IsAiming)
                recoil *= aimingRecoilAmountModifier;

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
            _player.IsAiming ? aimingRecoilAmountModifier : 1;
    }
}