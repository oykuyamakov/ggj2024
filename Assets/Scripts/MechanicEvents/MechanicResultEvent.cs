using Events;
using UnityEngine;

namespace MechanicEvents
{
    public class MechanicResultEvent : Event<MechanicResultEvent>
    {  
        public bool result;
        
        public static MechanicResultEvent Get(bool result)
        {
            var evt = GetPooledInternal();
            evt.result = result;
            return evt;
        }
        
    }
}
