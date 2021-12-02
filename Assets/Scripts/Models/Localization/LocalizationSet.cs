using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sudocu
{
    [Serializable]
    public class LocalizationSet
    {
        [field: SerializeField] public string Name { get; private set; }

        [field: SerializeField] public Language Language { get; private set; }

        [field: SerializeField] public List<LocalizationUnit> WordList { get; private set; }
    }
}
