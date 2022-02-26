using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventTriggerSet
{
    [Serializable]
    public class eventTrigger : EventArgs
    {
        public ResourceManager.ResourceType resourceType;
    }
}
