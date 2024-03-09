using UnityEngine;

namespace Game.CameraController.StatesMachine.States
{
    public sealed class AimCameraState : CharacterCameraState
    {
        [SerializeField] private RectTransform _aimObject;

        protected override void OnEnterBefore()
        {
            _aimObject.gameObject.SetActive(true);
        }

        protected override void OnExitBefore()
        {
            _aimObject.gameObject.SetActive(false);
        }
    }
}