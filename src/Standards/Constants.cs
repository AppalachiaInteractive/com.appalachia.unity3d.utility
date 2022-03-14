using System;
using UnityEngine;

namespace Appalachia.Utility.Standards
{
    public static class Constants
    {
        #region Constants and Static Readonly

        public static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public const int REFERENCE_RESOLUTION_HEIGHT = 1080;
        public const int REFERENCE_RESOLUTION_WIDTH = 1920;

        public static readonly Vector2 REFERENCE_RESOLUTION = new Vector2(
            REFERENCE_RESOLUTION_WIDTH,
            REFERENCE_RESOLUTION_HEIGHT
        );

        public static readonly Vector2 SCREEN_CENTER = REFERENCE_RESOLUTION * .5F;

        #endregion
    }
}
