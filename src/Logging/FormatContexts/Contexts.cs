using System.Collections.Generic;

namespace Appalachia.Utility.Logging
{
    public class Contexts
    {
        #region Static Fields and Autoproperties

        private static HashSet<AppaLogContext> _allContexts = new();

        #endregion

        internal static HashSet<AppaLogContext> AllContexts
        {
            get
            {
                if (_allContexts == null)
                {
                    _allContexts = new HashSet<AppaLogContext>();
                }

                return _allContexts;
            }
        }

        #region Nested type: _Extra00

        public class _Extra00 : AppaLogContext<_Extra00>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra00;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra00;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra00 = format;
            }
        }

        #endregion

        #region Nested type: _Extra01

        public class _Extra01 : AppaLogContext<_Extra01>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra01;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra01;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra01 = format;
            }
        }

        #endregion

        #region Nested type: _Extra02

        public class _Extra02 : AppaLogContext<_Extra02>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra02;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra02;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra02 = format;
            }
        }

        #endregion

        #region Nested type: _Extra03

        public class _Extra03 : AppaLogContext<_Extra03>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra03;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra03;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra03 = format;
            }
        }

        #endregion

        #region Nested type: _Extra04

        public class _Extra04 : AppaLogContext<_Extra04>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra04;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra04;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra04 = format;
            }
        }

        #endregion

        #region Nested type: _Extra05

        public class _Extra05 : AppaLogContext<_Extra05>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra05;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra05;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra05 = format;
            }
        }

        #endregion

        #region Nested type: _Extra06

        public class _Extra06 : AppaLogContext<_Extra06>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra06;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra06;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra06 = format;
            }
        }

        #endregion

        #region Nested type: _Extra07

        public class _Extra07 : AppaLogContext<_Extra07>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra07;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra07;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra07 = format;
            }
        }

        #endregion

        #region Nested type: _Extra08

        public class _Extra08 : AppaLogContext<_Extra08>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra08;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra08;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra08 = format;
            }
        }

        #endregion

        #region Nested type: _Extra09

        public class _Extra09 : AppaLogContext<_Extra09>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra09;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra09;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra09 = format;
            }
        }

        #endregion

        #region Nested type: _Extra10

        public class _Extra10 : AppaLogContext<_Extra10>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra10;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra10;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra10 = format;
            }
        }

        #endregion

        #region Nested type: _Extra11

        public class _Extra11 : AppaLogContext<_Extra11>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra11;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra11;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra11 = format;
            }
        }

        #endregion

        #region Nested type: _Extra12

        public class _Extra12 : AppaLogContext<_Extra12>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra12;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra12;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra12 = format;
            }
        }

        #endregion

        #region Nested type: _Extra13

        public class _Extra13 : AppaLogContext<_Extra13>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra13;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra13;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra13 = format;
            }
        }

        #endregion

        #region Nested type: _Extra14

        public class _Extra14 : AppaLogContext<_Extra14>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra14;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra14;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra14 = format;
            }
        }

        #endregion

        #region Nested type: _Extra15

        public class _Extra15 : AppaLogContext<_Extra15>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra15;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra15;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra15 = format;
            }
        }

        #endregion

        #region Nested type: _Extra16

        public class _Extra16 : AppaLogContext<_Extra16>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra16;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra16;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra16 = format;
            }
        }

        #endregion

        #region Nested type: _Extra17

        public class _Extra17 : AppaLogContext<_Extra17>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra17;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra17;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra17 = format;
            }
        }

        #endregion

        #region Nested type: _Extra18

        public class _Extra18 : AppaLogContext<_Extra18>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra18;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra18;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra18 = format;
            }
        }

        #endregion

        #region Nested type: _Extra19

        public class _Extra19 : AppaLogContext<_Extra19>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra19;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra19;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra19 = format;
            }
        }

        #endregion

        #region Nested type: _Extra20

        public class _Extra20 : AppaLogContext<_Extra20>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra20;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra20;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra20 = format;
            }
        }

        #endregion

        #region Nested type: _Extra21

        public class _Extra21 : AppaLogContext<_Extra21>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra21;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra21;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra21 = format;
            }
        }

        #endregion

        #region Nested type: _Extra22

        public class _Extra22 : AppaLogContext<_Extra22>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra22;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra22;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra22 = format;
            }
        }

        #endregion

        #region Nested type: _Extra23

        public class _Extra23 : AppaLogContext<_Extra23>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra23;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra23;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra23 = format;
            }
        }

        #endregion

        #region Nested type: _Extra24

        public class _Extra24 : AppaLogContext<_Extra24>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra24;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra24;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra24 = format;
            }
        }

        #endregion

        #region Nested type: _Extra25

        public class _Extra25 : AppaLogContext<_Extra25>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra25;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra25;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra25 = format;
            }
        }

        #endregion

        #region Nested type: _Extra26

        public class _Extra26 : AppaLogContext<_Extra26>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra26;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra26;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra26 = format;
            }
        }

        #endregion

        #region Nested type: _Extra27

        public class _Extra27 : AppaLogContext<_Extra27>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra27;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra27;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra27 = format;
            }
        }

        #endregion

        #region Nested type: _Extra28

        public class _Extra28 : AppaLogContext<_Extra28>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra28;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra28;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra28 = format;
            }
        }

        #endregion

        #region Nested type: _Extra29

        public class _Extra29 : AppaLogContext<_Extra29>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra29;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra29;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra29 = format;
            }
        }

        #endregion

        #region Nested type: _Extra30

        public class _Extra30 : AppaLogContext<_Extra30>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra30;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra30;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra30 = format;
            }
        }

        #endregion

        #region Nested type: _Extra31

        public class _Extra31 : AppaLogContext<_Extra31>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra31;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra31;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra31 = format;
            }
        }

        #endregion

        #region Nested type: _Extra32

        public class _Extra32 : AppaLogContext<_Extra32>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra32;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra32;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra32 = format;
            }
        }

        #endregion

        #region Nested type: _Extra33

        public class _Extra33 : AppaLogContext<_Extra33>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra33;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra33;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra33 = format;
            }
        }

        #endregion

        #region Nested type: _Extra34

        public class _Extra34 : AppaLogContext<_Extra34>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra34;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra34;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra34 = format;
            }
        }

        #endregion

        #region Nested type: _Extra35

        public class _Extra35 : AppaLogContext<_Extra35>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra35;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra35;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra35 = format;
            }
        }

        #endregion

        #region Nested type: _Extra36

        public class _Extra36 : AppaLogContext<_Extra36>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra36;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra36;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra36 = format;
            }
        }

        #endregion

        #region Nested type: _Extra37

        public class _Extra37 : AppaLogContext<_Extra37>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra37;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra37;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra37 = format;
            }
        }

        #endregion

        #region Nested type: _Extra38

        public class _Extra38 : AppaLogContext<_Extra38>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra38;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra38;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra38 = format;
            }
        }

        #endregion

        #region Nested type: _Extra39

        public class _Extra39 : AppaLogContext<_Extra39>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra39;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra39;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra39 = format;
            }
        }

        #endregion

        #region Nested type: _Extra40

        public class _Extra40 : AppaLogContext<_Extra40>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra40;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra40;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra40 = format;
            }
        }

        #endregion

        #region Nested type: _Extra41

        public class _Extra41 : AppaLogContext<_Extra41>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra41;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra41;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra41 = format;
            }
        }

        #endregion

        #region Nested type: _Extra42

        public class _Extra42 : AppaLogContext<_Extra42>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra42;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra42;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra42 = format;
            }
        }

        #endregion

        #region Nested type: _Extra43

        public class _Extra43 : AppaLogContext<_Extra43>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra43;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra43;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra43 = format;
            }
        }

        #endregion

        #region Nested type: _Extra44

        public class _Extra44 : AppaLogContext<_Extra44>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra44;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra44;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra44 = format;
            }
        }

        #endregion

        #region Nested type: _Extra45

        public class _Extra45 : AppaLogContext<_Extra45>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra45;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra45;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra45 = format;
            }
        }

        #endregion

        #region Nested type: _Extra46

        public class _Extra46 : AppaLogContext<_Extra46>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra46;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra46;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra46 = format;
            }
        }

        #endregion

        #region Nested type: _Extra47

        public class _Extra47 : AppaLogContext<_Extra47>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra47;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra47;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra47 = format;
            }
        }

        #endregion

        #region Nested type: _Extra48

        public class _Extra48 : AppaLogContext<_Extra48>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra48;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra48;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra48 = format;
            }
        }

        #endregion

        #region Nested type: _Extra49

        public class _Extra49 : AppaLogContext<_Extra49>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra49;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra49;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra49 = format;
            }
        }

        #endregion

        #region Nested type: _Extra50

        public class _Extra50 : AppaLogContext<_Extra50>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra50;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra50;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra50 = format;
            }
        }

        #endregion

        #region Nested type: _Extra51

        public class _Extra51 : AppaLogContext<_Extra51>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra51;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra51;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra51 = format;
            }
        }

        #endregion

        #region Nested type: _Extra52

        public class _Extra52 : AppaLogContext<_Extra52>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra52;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra52;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra52 = format;
            }
        }

        #endregion

        #region Nested type: _Extra53

        public class _Extra53 : AppaLogContext<_Extra53>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra53;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra53;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra53 = format;
            }
        }

        #endregion

        #region Nested type: _Extra54

        public class _Extra54 : AppaLogContext<_Extra54>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra54;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra54;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra54 = format;
            }
        }

        #endregion

        #region Nested type: _Extra55

        public class _Extra55 : AppaLogContext<_Extra55>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra55;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra55;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra55 = format;
            }
        }

        #endregion

        #region Nested type: _Extra56

        public class _Extra56 : AppaLogContext<_Extra56>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra56;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra56;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra56 = format;
            }
        }

        #endregion

        #region Nested type: _Extra57

        public class _Extra57 : AppaLogContext<_Extra57>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra57;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra57;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra57 = format;
            }
        }

        #endregion

        #region Nested type: _Extra58

        public class _Extra58 : AppaLogContext<_Extra58>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra58;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra58;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra58 = format;
            }
        }

        #endregion

        #region Nested type: _Extra59

        public class _Extra59 : AppaLogContext<_Extra59>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra59;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra59;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra59 = format;
            }
        }

        #endregion

        #region Nested type: _Extra60

        public class _Extra60 : AppaLogContext<_Extra60>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra60;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra60;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra60 = format;
            }
        }

        #endregion

        #region Nested type: _Extra61

        public class _Extra61 : AppaLogContext<_Extra61>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra61;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra61;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra61 = format;
            }
        }

        #endregion

        #region Nested type: _Extra62

        public class _Extra62 : AppaLogContext<_Extra62>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra62;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra62;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra62 = format;
            }
        }

        #endregion

        #region Nested type: _Extra63

        public class _Extra63 : AppaLogContext<_Extra63>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra63;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra63;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra63 = format;
            }
        }

        #endregion

        #region Nested type: _Extra64

        public class _Extra64 : AppaLogContext<_Extra64>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra64;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra64;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra64 = format;
            }
        }

        #endregion

        #region Nested type: _Extra65

        public class _Extra65 : AppaLogContext<_Extra65>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra65;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra65;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra65 = format;
            }
        }

        #endregion

        #region Nested type: _Extra66

        public class _Extra66 : AppaLogContext<_Extra66>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra66;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra66;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra66 = format;
            }
        }

        #endregion

        #region Nested type: _Extra67

        public class _Extra67 : AppaLogContext<_Extra67>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra67;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra67;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra67 = format;
            }
        }

        #endregion

        #region Nested type: _Extra68

        public class _Extra68 : AppaLogContext<_Extra68>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra68;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra68;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra68 = format;
            }
        }

        #endregion

        #region Nested type: _Extra69

        public class _Extra69 : AppaLogContext<_Extra69>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra69;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra69;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra69 = format;
            }
        }

        #endregion

        #region Nested type: _Extra70

        public class _Extra70 : AppaLogContext<_Extra70>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra70;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra70;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra70 = format;
            }
        }

        #endregion

        #region Nested type: _Extra71

        public class _Extra71 : AppaLogContext<_Extra71>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra71;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra71;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra71 = format;
            }
        }

        #endregion

        #region Nested type: _Extra72

        public class _Extra72 : AppaLogContext<_Extra72>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra72;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra72;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra72 = format;
            }
        }

        #endregion

        #region Nested type: _Extra73

        public class _Extra73 : AppaLogContext<_Extra73>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra73;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra73;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra73 = format;
            }
        }

        #endregion

        #region Nested type: _Extra74

        public class _Extra74 : AppaLogContext<_Extra74>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra74;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra74;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra74 = format;
            }
        }

        #endregion

        #region Nested type: _Extra75

        public class _Extra75 : AppaLogContext<_Extra75>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra75;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra75;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra75 = format;
            }
        }

        #endregion

        #region Nested type: _Extra76

        public class _Extra76 : AppaLogContext<_Extra76>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra76;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra76;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra76 = format;
            }
        }

        #endregion

        #region Nested type: _Extra77

        public class _Extra77 : AppaLogContext<_Extra77>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra77;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra77;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra77 = format;
            }
        }

        #endregion

        #region Nested type: _Extra78

        public class _Extra78 : AppaLogContext<_Extra78>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra78;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra78;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra78 = format;
            }
        }

        #endregion

        #region Nested type: _Extra79

        public class _Extra79 : AppaLogContext<_Extra79>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra79;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra79;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra79 = format;
            }
        }

        #endregion

        #region Nested type: _Extra80

        public class _Extra80 : AppaLogContext<_Extra80>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra80;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra80;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra80 = format;
            }
        }

        #endregion

        #region Nested type: _Extra81

        public class _Extra81 : AppaLogContext<_Extra81>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra81;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra81;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra81 = format;
            }
        }

        #endregion

        #region Nested type: _Extra82

        public class _Extra82 : AppaLogContext<_Extra82>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra82;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra82;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra82 = format;
            }
        }

        #endregion

        #region Nested type: _Extra83

        public class _Extra83 : AppaLogContext<_Extra83>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra83;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra83;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra83 = format;
            }
        }

        #endregion

        #region Nested type: _Extra84

        public class _Extra84 : AppaLogContext<_Extra84>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra84;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra84;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra84 = format;
            }
        }

        #endregion

        #region Nested type: _Extra85

        public class _Extra85 : AppaLogContext<_Extra85>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra85;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra85;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra85 = format;
            }
        }

        #endregion

        #region Nested type: _Extra86

        public class _Extra86 : AppaLogContext<_Extra86>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra86;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra86;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra86 = format;
            }
        }

        #endregion

        #region Nested type: _Extra87

        public class _Extra87 : AppaLogContext<_Extra87>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra87;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra87;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra87 = format;
            }
        }

        #endregion

        #region Nested type: _Extra88

        public class _Extra88 : AppaLogContext<_Extra88>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra88;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra88;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra88 = format;
            }
        }

        #endregion

        #region Nested type: _Extra89

        public class _Extra89 : AppaLogContext<_Extra89>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra89;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra89;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra89 = format;
            }
        }

        #endregion

        #region Nested type: _Extra90

        public class _Extra90 : AppaLogContext<_Extra90>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra90;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra90;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra90 = format;
            }
        }

        #endregion

        #region Nested type: _Extra91

        public class _Extra91 : AppaLogContext<_Extra91>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra91;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra91;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra91 = format;
            }
        }

        #endregion

        #region Nested type: _Extra92

        public class _Extra92 : AppaLogContext<_Extra92>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra92;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra92;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra92 = format;
            }
        }

        #endregion

        #region Nested type: _Extra93

        public class _Extra93 : AppaLogContext<_Extra93>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra93;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra93;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra93 = format;
            }
        }

        #endregion

        #region Nested type: _Extra94

        public class _Extra94 : AppaLogContext<_Extra94>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra94;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra94;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra94 = format;
            }
        }

        #endregion

        #region Nested type: _Extra95

        public class _Extra95 : AppaLogContext<_Extra95>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra95;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra95;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra95 = format;
            }
        }

        #endregion

        #region Nested type: _Extra96

        public class _Extra96 : AppaLogContext<_Extra96>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra96;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra96;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra96 = format;
            }
        }

        #endregion

        #region Nested type: _Extra97

        public class _Extra97 : AppaLogContext<_Extra97>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra97;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra97;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra97 = format;
            }
        }

        #endregion

        #region Nested type: _Extra98

        public class _Extra98 : AppaLogContext<_Extra98>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts._extra98;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts._extra98;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts._extra98 = format;
            }
        }

        #endregion

        #region Nested type: Animal

        public class Animal : AppaLogContext<Animal>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.animal;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.animal;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.animal = format;
            }
        }

        #endregion

        #region Nested type: Animation

        public class Animation : AppaLogContext<Animation>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.animation;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.animation;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.animation = format;
            }
        }

        #endregion

        #region Nested type: Application

        public class Application : AppaLogContext<Application>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.application;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.application;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.application = format;
            }
        }

        #endregion

        #region Nested type: Area

        public class Area : AppaLogContext<Area>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.area;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.area;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.area = format;
            }
        }

        #endregion

        #region Nested type: ArrayPooling

        public class ArrayPooling : AppaLogContext<ArrayPooling>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.arrayPooling;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.arrayPooling;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.arrayPooling = format;
            }
        }

        #endregion

        #region Nested type: Assets

        public class Assets : AppaLogContext<Assets>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.assets;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.assets;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.assets = format;
            }
        }

        #endregion

        #region Nested type: Audio

        public class Audio : AppaLogContext<Audio>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.audio;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.audio;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.audio = format;
            }
        }

        #endregion

        #region Nested type: Bazooka

        public class Bazooka : AppaLogContext<Bazooka>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.bazooka;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.bazooka;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.bazooka = format;
            }
        }

        #endregion

        #region Nested type: Behaviours

        public class Behaviours : AppaLogContext<Behaviours>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.behaviours;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.behaviours;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.behaviours = format;
            }
        }

        #endregion

        #region Nested type: Bootload

        public class Bootload : AppaLogContext<Bootload>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.bootload;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.bootload;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.bootload = format;
            }
        }

        #endregion

        #region Nested type: Caching

        public class Caching : AppaLogContext<Caching>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.caching;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.caching;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.caching = format;
            }
        }

        #endregion

        #region Nested type: Character

        public class Character : AppaLogContext<Character>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.character;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.character;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.character = format;
            }
        }

        #endregion

        #region Nested type: CI

        public class CI : AppaLogContext<CI>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.ci;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.ci;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.ci = format;
            }
        }

        #endregion

        #region Nested type: Clock

        public class Clock : AppaLogContext<Clock>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.clock;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.clock;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.clock = format;
            }
        }

        #endregion

        #region Nested type: Collections

        public class Collections : AppaLogContext<Collections>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.collections;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.collections;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.collections = format;
            }
        }

        #endregion

        #region Nested type: Components

        public class Components : AppaLogContext<Components>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.components;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.components;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.components = format;
            }
        }

        #endregion

        #region Nested type: ConvexDecomposition

        public class ConvexDecomposition : AppaLogContext<ConvexDecomposition>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.convexDecomposition;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.convexDecomposition;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.convexDecomposition = format;
            }
        }

        #endregion

        #region Nested type: Core

        public class Core : AppaLogContext<Core>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.core;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.core;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.core = format;
            }
        }

        #endregion

        #region Nested type: Crafting

        public class Crafting : AppaLogContext<Crafting>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.crafting;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.crafting;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.crafting = format;
            }
        }

        #endregion

        #region Nested type: Cursor

        public class Cursor : AppaLogContext<Cursor>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.cursor;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.cursor;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.cursor = format;
            }
        }

        #endregion

        #region Nested type: Data

        public class Data : AppaLogContext<Data>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.data;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.data;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.data = format;
            }
        }

        #endregion

        #region Nested type: Database

        public class Database : AppaLogContext<Database>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.database;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.database;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.database = format;
            }
        }

        #endregion

        #region Nested type: DebugOverlay

        public class DebugOverlay : AppaLogContext<DebugOverlay>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.debugOverlay;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.debugOverlay;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.debugOverlay = format;
            }
        }

        #endregion

        #region Nested type: Dependencies

        public class Dependencies : AppaLogContext<Dependencies>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.dependencies;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.dependencies;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.dependencies = format;
            }
        }

        #endregion

        #region Nested type: DevConsole

        public class DevConsole : AppaLogContext<DevConsole>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.devConsole;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.devConsole;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.devConsole = format;
            }
        }

        #endregion

        #region Nested type: Editing

        public class Editing : AppaLogContext<Editing>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.editing;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.editing;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.editing = format;
            }
        }

        #endregion

        #region Nested type: Editor

        public class Editor : AppaLogContext<Editor>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.editor;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.editor;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.editor = format;
            }
        }

        #endregion

        #region Nested type: Execution

        public class Execution : AppaLogContext<Execution>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.execution;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.execution;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.execution = format;
            }
        }

        #endregion

        #region Nested type: Extensions

        public class Extensions : AppaLogContext<Extensions>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.extensions;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.extensions;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.extensions = format;
            }
        }

        #endregion

        #region Nested type: Filtering

        public class Filtering : AppaLogContext<Filtering>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.filtering;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.filtering;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.filtering = format;
            }
        }

        #endregion

        #region Nested type: Fire

        public class Fire : AppaLogContext<Fire>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.fire;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.fire;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.fire = format;
            }
        }

        #endregion

        #region Nested type: FrameEvent

        public class FrameEvent : AppaLogContext<FrameEvent>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.frameEvent;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.frameEvent;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.frameEvent = format;
            }
        }

        #endregion

        #region Nested type: Game

        public class Game : AppaLogContext<Game>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.game;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.game;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.game = format;
            }
        }

        #endregion

        #region Nested type: Gameplay

        public class Gameplay : AppaLogContext<Gameplay>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.gameplay;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.gameplay;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.gameplay = format;
            }
        }

        #endregion

        #region Nested type: Globals

        public class Globals : AppaLogContext<Globals>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.globals;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.globals;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.globals = format;
            }
        }

        #endregion

        #region Nested type: HUD

        public class HUD : AppaLogContext<HUD>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.hud;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.hud;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.hud = format;
            }
        }

        #endregion

        #region Nested type: InGameMenu

        public class InGameMenu : AppaLogContext<InGameMenu>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.inGameMenu;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.inGameMenu;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.inGameMenu = format;
            }
        }

        #endregion

        #region Nested type: Initialize

        public class Initialize : AppaLogContext<Initialize>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.initialize;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.initialize;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.initialize = format;
            }
        }

        #endregion

        #region Nested type: Input

        public class Input : AppaLogContext<Input>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.input;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.input;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.input = format;
            }
        }

        #endregion

        #region Nested type: Inventory

        public class Inventory : AppaLogContext<Inventory>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.inventory;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.inventory;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.inventory = format;
            }
        }

        #endregion

        #region Nested type: Jobs

        public class Jobs : AppaLogContext<Jobs>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.jobs;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.jobs;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.jobs = format;
            }
        }

        #endregion

        #region Nested type: KOC

        public class KOC : AppaLogContext<KOC>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.koc;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.koc;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.koc = format;
            }
        }

        #endregion

        #region Nested type: Labels

        public class Labels : AppaLogContext<Labels>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.labels;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.labels;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.labels = format;
            }
        }

        #endregion

        #region Nested type: Layers

        public class Layers : AppaLogContext<Layers>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.layers;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.layers;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.layers = format;
            }
        }

        #endregion

        #region Nested type: Lifetime

        public class Lifetime : AppaLogContext<Lifetime>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.lifetime;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.lifetime;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.lifetime = format;
            }
        }

        #endregion

        #region Nested type: Lighting

        public class Lighting : AppaLogContext<Lighting>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.lighting;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.lighting;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.lighting = format;
            }
        }

        #endregion

        #region Nested type: LoadingScreen

        public class LoadingScreen : AppaLogContext<LoadingScreen>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.loadingScreen;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.loadingScreen;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.loadingScreen = format;
            }
        }

        #endregion

        #region Nested type: MainMenu

        public class MainMenu : AppaLogContext<MainMenu>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.mainMenu;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.mainMenu;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.mainMenu = format;
            }
        }

        #endregion

        #region Nested type: Maintenance

        public class Maintenance : AppaLogContext<Maintenance>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.maintenance;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.maintenance;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.maintenance = format;
            }
        }

        #endregion

        #region Nested type: Math

        public class Math : AppaLogContext<Math>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.math;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.math;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.math = format;
            }
        }

        #endregion

        #region Nested type: MeshBurial

        public class MeshBurial : AppaLogContext<MeshBurial>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.meshBurial;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.meshBurial;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.meshBurial = format;
            }
        }

        #endregion

        #region Nested type: MeshData

        public class MeshData : AppaLogContext<MeshData>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.meshData;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.meshData;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.meshData = format;
            }
        }

        #endregion

        #region Nested type: Obi

        public class Obi : AppaLogContext<Obi>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.obi;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.obi;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.obi = format;
            }
        }

        #endregion

        #region Nested type: ObjectPooling

        public class ObjectPooling : AppaLogContext<ObjectPooling>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.objectPooling;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.objectPooling;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.objectPooling = format;
            }
        }

        #endregion

        #region Nested type: Octree

        public class Octree : AppaLogContext<Octree>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.octree;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.octree;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.octree = format;
            }
        }

        #endregion

        #region Nested type: Optimization

        public class Optimization : AppaLogContext<Optimization>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.optimization;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.optimization;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.optimization = format;
            }
        }

        #endregion

        #region Nested type: Overrides

        public class Overrides : AppaLogContext<Overrides>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.overrides;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.overrides;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.overrides = format;
            }
        }

        #endregion

        #region Nested type: PauseMenu

        public class PauseMenu : AppaLogContext<PauseMenu>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.pauseMenu;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.pauseMenu;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.pauseMenu = format;
            }
        }

        #endregion

        #region Nested type: Playables

        public class Playables : AppaLogContext<Playables>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.playables;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.playables;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.playables = format;
            }
        }

        #endregion

        #region Nested type: PostProcessing

        public class PostProcessing : AppaLogContext<PostProcessing>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.postProcessing;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.postProcessing;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.postProcessing = format;
            }
        }

        #endregion

        #region Nested type: PrefabRendering

        public class PrefabRendering : AppaLogContext<PrefabRendering>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.prefabRendering;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.prefabRendering;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.prefabRendering = format;
            }
        }

        #endregion

        #region Nested type: Prefabs

        public class Prefabs : AppaLogContext<Prefabs>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.prefabs;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.prefabs;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.prefabs = format;
            }
        }

        #endregion

        #region Nested type: Preferences

        public class Preferences : AppaLogContext<Preferences>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.preferences;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.preferences;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.preferences = format;
            }
        }

        #endregion

        #region Nested type: Prototype

        public class Prototype : AppaLogContext<Prototype>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.prototype;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.prototype;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.prototype = format;
            }
        }

        #endregion

        #region Nested type: ReactionSystem

        public class ReactionSystem : AppaLogContext<ReactionSystem>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.reactionSystem;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.reactionSystem;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.reactionSystem = format;
            }
        }

        #endregion

        #region Nested type: Rendering

        public class Rendering : AppaLogContext<Rendering>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.rendering;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.rendering;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.rendering = format;
            }
        }

        #endregion

        #region Nested type: RuntimeGraphs

        public class RuntimeGraphs : AppaLogContext<RuntimeGraphs>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.runtimeGraphs;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.runtimeGraphs;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.runtimeGraphs = format;
            }
        }

        #endregion

        #region Nested type: Scriptables

        public class Scriptables : AppaLogContext<Scriptables>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.scriptables;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.scriptables;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.scriptables = format;
            }
        }

        #endregion

        #region Nested type: SDF

        public class SDF : AppaLogContext<SDF>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.sdf;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.sdf;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.sdf = format;
            }
        }

        #endregion

        #region Nested type: Shading

        public class Shading : AppaLogContext<Shading>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.shading;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.shading;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.shading = format;
            }
        }

        #endregion

        #region Nested type: Shell

        public class Shell : AppaLogContext<Shell>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.shell;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.shell;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.shell = format;
            }
        }

        #endregion

        #region Nested type: Simulation

        public class Simulation : AppaLogContext<Simulation>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.simulation;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.simulation;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.simulation = format;
            }
        }

        #endregion

        #region Nested type: Singleton

        public class Singleton : AppaLogContext<Singleton>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.singleton;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.singleton;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.singleton = format;
            }
        }

        #endregion

        #region Nested type: Spatial

        public class Spatial : AppaLogContext<Spatial>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.spatial;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.spatial;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.spatial = format;
            }
        }

        #endregion

        #region Nested type: SplashScreen

        public class SplashScreen : AppaLogContext<SplashScreen>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.splashScreen;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.splashScreen;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.splashScreen = format;
            }
        }

        #endregion

        #region Nested type: StartEnvironment

        public class StartEnvironment : AppaLogContext<StartEnvironment>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.startEnvironment;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.startEnvironment;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.startEnvironment = format;
            }
        }

        #endregion

        #region Nested type: StartScreen

        public class StartScreen : AppaLogContext<StartScreen>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.startScreen;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.startScreen;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.startScreen = format;
            }
        }

        #endregion

        #region Nested type: Styling

        public class Styling : AppaLogContext<Styling>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.styling;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.styling;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.styling = format;
            }
        }

        #endregion

        #region Nested type: Terrain

        public class Terrain : AppaLogContext<Terrain>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.terrain;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.terrain;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.terrain = format;
            }
        }

        #endregion

        #region Nested type: Timeline

        public class Timeline : AppaLogContext<Timeline>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.timeline;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.timeline;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.timeline = format;
            }
        }

        #endregion

        #region Nested type: TouchBend

        public class TouchBend : AppaLogContext<TouchBend>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.touchBend;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.touchBend;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.touchBend = format;
            }
        }

        #endregion

        #region Nested type: Trees

        public class Trees : AppaLogContext<Trees>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.trees;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.trees;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.trees = format;
            }
        }

        #endregion

        #region Nested type: UI

        public class UI : AppaLogContext<UI>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.ui;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.ui;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.ui = format;
            }
        }

        #endregion

        #region Nested type: Uncategorized

        public class Uncategorized : AppaLogContext<Uncategorized>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.uncategorized;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.uncategorized;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.uncategorized = format;
            }
        }

        #endregion

        #region Nested type: Utility

        public class Utility : AppaLogContext<Utility>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.utility;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.utility;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.utility = format;
            }
        }

        #endregion

        #region Nested type: VFX

        public class VFX : AppaLogContext<VFX>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.vfx;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.vfx;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.vfx = format;
            }
        }

        #endregion

        #region Nested type: Visualizers

        public class Visualizers : AppaLogContext<Visualizers>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.visualizers;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.visualizers;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.visualizers = format;
            }
        }

        #endregion

        #region Nested type: Volumes

        public class Volumes : AppaLogContext<Volumes>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.volumes;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.volumes;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.volumes = format;
            }
        }

        #endregion

        #region Nested type: Voxels

        public class Voxels : AppaLogContext<Voxels>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.voxels;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.voxels;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.voxels = format;
            }
        }

        #endregion

        #region Nested type: Water

        public class Water : AppaLogContext<Water>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.water;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.water;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.water = format;
            }
        }

        #endregion

        #region Nested type: Wind

        public class Wind : AppaLogContext<Wind>
        {
            internal override AppaLogFormats.LogFormat GetPrefixFormat()
            {
                return AppaLogFormatHolder.contexts.wind;
            }

            internal override AppaLogFormats.LogFormat GetPrefixFormatInstance()
            {
                return AppaLogFormatHolder.formats.contexts.wind;
            }

            internal override void UpdateFormatInstance(AppaLogFormats.LogFormat format)
            {
                AppaLogFormatHolder.formats.contexts.wind = format;
            }
        }

        #endregion
    }
}
