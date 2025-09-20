using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SLC.RetroHorror.DataPersistence;
using UnityEngine;

namespace SLC.RetroHorror.Core
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private ItemCatalogue itemCatalogue;

        #region Default Methods

        protected override void Awake()
        {
            base.Awake();
            CheckForDuplicateIds();
            itemCatalogue.InitializeItemDictionary();
        }

        #endregion

        #region Error Handling

        private void CheckForDuplicateIds()
        {
            List<UniqueId> uniqueIds = FindObjectsByType<UniqueId>(FindObjectsSortMode.None).ToList();
            List<string> ids = new();
            uniqueIds.ForEach(id => ids.Add(id.uniqueId));

            ids.GroupBy(i => i).Where(g => g.Count() > 1).Select(g => g.Key).ToList().ForEach((id) =>
            {
                string error = $"Found duplicate uniqueId: {id}\nOn objects:";
                uniqueIds.Where(uid => uid.uniqueId == id).ToList().ForEach(uid => error += $" {uid.name}");
                Debug.LogError(error);
            });
        }

        #endregion

        #region Helper Methods

        public IEnumerator DoAfterDelay(Action _action, float _delay = 0f)
        {
            if (_delay == 0f) yield return null;
            else yield return new WaitForSeconds(_delay);

            _action?.Invoke();
        }

        public IEnumerator DoAfterDelay(Action _action, WaitForSeconds _wait)
        {
            yield return _wait;
            _action?.Invoke();
        }

        #endregion
    }
}