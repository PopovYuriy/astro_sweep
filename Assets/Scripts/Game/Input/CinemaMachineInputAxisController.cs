using UnityEngine;
using Cinemachine;

namespace Game.Input
{
    public class CinemaMachineInputAxisController : MonoBehaviour
    {
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";
        
        private void Start()
        {
            CinemachineCore.GetInputAxis = GetAxisCustom;
        }
        
        private float GetAxisCustom(string axisName)
        {
            return axisName switch
            {
                MouseX when UnityEngine.Input.GetMouseButton(0) => UnityEngine.Input.GetAxis(MouseX),
                MouseX => 0,
                MouseY when UnityEngine.Input.GetMouseButton(0) => UnityEngine.Input.GetAxis(MouseY),
                MouseY => 0,
                _ => UnityEngine.Input.GetAxis(axisName)
            };
        }
    }
}