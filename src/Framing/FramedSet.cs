using System.Collections.Generic;
using UnityEngine;

namespace Appalachia.Utility.Framing
{
    internal class FramedSet
    {
        public FramedSet(GameObject gameObject)
        {
            _targets = new List<GameObject> {gameObject};
        }

        public FramedSet(IEnumerable<GameObject> gameObjects)
        {
            _targets = new List<GameObject>();

            _targets.AddRange(gameObjects);
        }

        public FramedSet()
        {
            _targets = new List<GameObject>();
        }

        #region Fields and Autoproperties

        private readonly List<GameObject> _targets;

        #endregion

        public IReadOnlyList<GameObject> targets => _targets;
    }
}
