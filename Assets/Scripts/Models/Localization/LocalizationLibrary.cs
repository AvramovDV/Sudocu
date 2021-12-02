using System.Collections.Generic;
using UnityEngine;

namespace Sudocu
{
    [CreateAssetMenu(menuName = "Sudocu/LocaliaztionLib")]
    public class LocalizationLibrary : ScriptableObject
    {
        [field: SerializeField] public List<LocalizationSet> Localizations { get; private set; }

        public LocalizationSet GetLocalization(Language language)
        {
            LocalizationSet result = Localizations[0];

            foreach (var item in Localizations)
            {
                if (item.Language == language)
                {
                    result = item;
                    break;
                }
            }

            return result;
        }
    }
}
