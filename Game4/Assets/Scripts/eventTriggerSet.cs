using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventTriggerSet
{
    [Serializable]
    public class resourceEventTrigger : EventArgs
    {
        public ResourceManager.ResourceType resourceType;
    }

    [Serializable]
    public class itemEventTrigger : EventArgs
    {
        public ResourceManager.ItemType itemType;
    }
}
