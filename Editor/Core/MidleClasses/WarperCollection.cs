using System;
using System.Collections.Generic;
using UnityEngine;

namespace UniGameTools.BuildMechine
{
    [Serializable]
    public class WarperCollection
    {
        [SerializeField]
        public List<ActionWarper> Warpers;
    }
}