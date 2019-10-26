﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace fsm {
    [Serializable]
    public class SerializableFsmTransition {
        [SerializeField] public List<SerializableFsmCondition> serializableFsmConditions = new List<SerializableFsmCondition>();
        [SerializeField] public string targetState;
    }
}
