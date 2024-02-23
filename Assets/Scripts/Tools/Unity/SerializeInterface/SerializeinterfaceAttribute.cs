using UnityEngine;

namespace Tools.Unity.SerializeInterface
{
    public sealed class SerializeInterfaceAttribute : PropertyAttribute
    {
        public System.Type SerializedType { get; }
 
        public SerializeInterfaceAttribute(System.Type type)
        {
            SerializedType = type;
        }
    }
}