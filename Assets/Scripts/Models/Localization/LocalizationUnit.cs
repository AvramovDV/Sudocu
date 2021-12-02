using System;
using UnityEngine;

namespace Sudocu
{
    [Serializable]
    public class LocalizationUnit
    {
        [field:SerializeField] public string Key { get; private set; }

        [field: SerializeField] public string Value { get; private set; }
    }
}
