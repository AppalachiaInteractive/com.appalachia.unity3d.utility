namespace Appalachia.Utility.Events.Base
{
    public abstract class ValueBaseArgs<TD, TV> : DelegateBaseArgs<TD>
        where TD : ValueBaseArgs<TD, TV>, new()
    {
        #region Fields and Autoproperties

        /// <summary>
        ///     The current value.
        /// </summary>
        public TV value;

        #endregion

        /// <inheritdoc />
        protected override void OnInitialize()
        {
            using (_PRF_OnInitialize.Auto())
            {
                base.OnInitialize();
            }
        }

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
