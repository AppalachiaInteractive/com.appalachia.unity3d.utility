namespace Appalachia.Utility.Events.Base
{
    public abstract class GameObjectValueBaseArgs<TD, TV> : GameObjectBaseArgs<TD>
        where TD : GameObjectValueBaseArgs<TD, TV>, new()
    {
        #region Fields and Autoproperties

        /// <summary>
        ///     The current value.
        /// </summary>
        public TV value;

        #endregion

        /// <inheritdoc />
        protected override void OnReset()
        {
            using (_PRF_OnReset.Auto())
            {
                base.OnReset();
                value = default;
            }
        }
    }
}
