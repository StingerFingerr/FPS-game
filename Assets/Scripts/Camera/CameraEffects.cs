using System;
using Character;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Camera
{
    public class CameraEffects: MonoBehaviour
    {
        public float normalFoV = 60f;
        public float sprintingFoV = 65f;
        
        private IInputService _input;
        private CharacterControllerScript _player;

        public UnityEngine.Camera camera;
        private float _targetFoV;

        [Inject]
        private void Construct(CharacterControllerScript player, IInputService input)
        {
            _player = player;
            _input = input;

            Subscribe();

            _targetFoV = normalFoV;
        }

        private void Subscribe()
        {
            _input.StartSprinting += StartSprinting;
            _input.FinishSprinting += FinishSprinting;
        }

        private void LateUpdate()
        {
            UpdateCameraFoV();
        }

        private void UpdateCameraFoV()
        {
            if(_input.GetMove() != Vector2.zero)
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, _targetFoV, Time.deltaTime * 10f);
        }

        private void StartSprinting() => 
            _targetFoV = sprintingFoV;

        private void FinishSprinting() => 
            _targetFoV = normalFoV;
    }
}