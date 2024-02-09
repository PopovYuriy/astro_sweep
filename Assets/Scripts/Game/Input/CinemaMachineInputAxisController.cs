using UnityEngine;
using Cinemachine;

namespace Game.Input
{
    public class CinemaMachineInputAxisController : MonoBehaviour
    {
        private void Start()
        {
            CinemachineCore.GetInputAxis = GetAxisCustom;
        }
        
        public float GetAxisCustom(string axisName)
        {
            if(axisName == "Mouse X")
            {
                if (UnityEngine.Input.GetMouseButton(0))
                    return UnityEngine.Input.GetAxis("Mouse X");
                
                return 0;
            } 
                
            if (axisName == "Mouse Y")
            {
                if (UnityEngine.Input.GetMouseButton(0))
                    return UnityEngine.Input.GetAxis("Mouse Y");
                return 0;
            }
                
            return UnityEngine.Input.GetAxis(axisName);
        }
    }
}