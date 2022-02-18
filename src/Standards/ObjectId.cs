using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using UnityEngine;
using Random = System.Random;

namespace Appalachia.Utility.Standards
{
    /// <summary>
    ///     Represent a 12-bytes BSON type used in document Id
    /// </summary>
    [Serializable]
    public class ObjectId : IComparable<ObjectId>, IEquatable<ObjectId>
    {
        // static constructor
        static ObjectId()
        {
            _currentMachine = (GetMachineHash() + AppDomain.CurrentDomain.Id) & 0x00ffffff;
            _machineIncrement = new Random().Next();

            try
            {
                _currentProcessID = (short)GetCurrentProcessId();
            }
            catch (SecurityException)
            {
                _currentProcessID = 0;
            }
        }

        /// <summary>
        ///     Initializes a new empty instance of the ObjectId class.
        /// </summary>
        public ObjectId()
        {
            _timestamp = 0;
            _machine = 0;
            _pid = 0;
            _increment = 0;
        }

        /// <summary>
        ///     Initializes a new instance of the ObjectId class from ObjectId vars.
        /// </summary>
        public ObjectId(int timestamp, int machine, short pid, int increment)
        {
            _timestamp = timestamp;
            _machine = machine;
            _pid = pid;
            _increment = increment;
        }

        /// <summary>
        ///     Initializes a new instance of ObjectId class from another ObjectId.
        /// </summary>
        public ObjectId(ObjectId from)
        {
            _timestamp = from.Timestamp;
            _machine = from.Machine;
            _pid = from.Pid;
            _increment = from.Increment;
        }

        /// <summary>
        ///     Initializes a new instance of the ObjectId class from hex string.
        /// </summary>
        public ObjectId(string value) : this(FromHex(value))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the ObjectId class from byte array.
        /// </summary>
        public ObjectId(byte[] bytes, int startIndex = 0)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            _timestamp = (bytes[startIndex + 0] << 24) +
                         (bytes[startIndex + 1] << 16) +
                         (bytes[startIndex + 2] << 8) +
                         bytes[startIndex + 3];

            _machine = (bytes[startIndex + 4] << 16) + (bytes[startIndex + 5] << 8) + bytes[startIndex + 6];

            _pid = (short)((bytes[startIndex + 7] << 8) + bytes[startIndex + 8]);

            _increment = (bytes[startIndex + 9] << 16) +
                         (bytes[startIndex + 10] << 8) +
                         bytes[startIndex + 11];
        }

        #region Static Fields and Autoproperties

        private static int _currentMachine;
        private static int _machineIncrement;
        private static short _currentProcessID;

        #endregion

        #region Fields and Autoproperties

        [SerializeField] private int _increment;
        [SerializeField] private int _machine;
        [SerializeField] private int _timestamp;
        [SerializeField] private short _pid;

        #endregion

        /// <summary>
        ///     A zero 12-bytes ObjectId
        /// </summary>
        public static ObjectId Empty => new ObjectId();

        /// <summary>
        ///     Get creation time
        /// </summary>
        public DateTime CreationTime => Constants.UNIX_EPOCH.AddSeconds(Timestamp);

        /// <summary>
        ///     Get increment
        /// </summary>
        public int Increment => _increment;

        /// <summary>
        ///     Get machine number
        /// </summary>
        public int Machine => _machine;

        /// <summary>
        ///     Get timestamp
        /// </summary>
        public int Timestamp => _timestamp;

        /// <summary>
        ///     Get pid number
        /// </summary>
        public short Pid => _pid;

        /// <summary>
        ///     Creates a new ObjectId.
        /// </summary>
        public static ObjectId NewObjectId()
        {
            var timestamp = (long)Math.Floor((DateTime.UtcNow - Constants.UNIX_EPOCH).TotalSeconds);

            var inc = Interlocked.Increment(ref _machineIncrement) & 0x00ffffff;

            return new ObjectId((int)timestamp, _currentMachine, _currentProcessID, inc);
        }

        public static bool operator ==(ObjectId lhs, ObjectId rhs)
        {
            if (lhs is null)
            {
                return rhs is null;
            }

            if (rhs is null)
            {
                return false; // don't check type because sometimes different types can be ==
            }

            return lhs.Equals(rhs);
        }

        public static bool operator >(ObjectId lhs, ObjectId rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }

        public static bool operator >=(ObjectId lhs, ObjectId rhs)
        {
            return lhs.CompareTo(rhs) >= 0;
        }

        public static bool operator !=(ObjectId lhs, ObjectId rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator <(ObjectId lhs, ObjectId rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }

        public static bool operator <=(ObjectId lhs, ObjectId rhs)
        {
            return lhs.CompareTo(rhs) <= 0;
        }

        /// <summary>
        ///     Determines whether the specified object is equal to this instance.
        /// </summary>
        public override bool Equals(object other)
        {
            return Equals(other as ObjectId);
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            var hash = 17;
            hash = (37 * hash) + Timestamp.GetHashCode();
            hash = (37 * hash) + Machine.GetHashCode();
            hash = (37 * hash) + Pid.GetHashCode();
            hash = (37 * hash) + Increment.GetHashCode();
            return hash;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return BitConverter.ToString(ToByteArray()).Replace("-", "").ToLower();
        }

        /// <summary>
        ///     Represent ObjectId as 12 bytes array
        /// </summary>
        public void ToByteArray(byte[] bytes, int startIndex)
        {
            bytes[startIndex + 0] = (byte)(Timestamp >> 24);
            bytes[startIndex + 1] = (byte)(Timestamp >> 16);
            bytes[startIndex + 2] = (byte)(Timestamp >> 8);
            bytes[startIndex + 3] = (byte)Timestamp;
            bytes[startIndex + 4] = (byte)(Machine >> 16);
            bytes[startIndex + 5] = (byte)(Machine >> 8);
            bytes[startIndex + 6] = (byte)Machine;
            bytes[startIndex + 7] = (byte)(Pid >> 8);
            bytes[startIndex + 8] = (byte)Pid;
            bytes[startIndex + 9] = (byte)(Increment >> 16);
            bytes[startIndex + 10] = (byte)(Increment >> 8);
            bytes[startIndex + 11] = (byte)Increment;
        }

        public byte[] ToByteArray()
        {
            var bytes = new byte[12];

            ToByteArray(bytes, 0);

            return bytes;
        }

        /// <summary>
        ///     Convert hex value string in byte array
        /// </summary>
        private static byte[] FromHex(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Length != 24)
            {
                throw new ArgumentException(
                    $"ObjectId strings should be 24 hex characters, got {value.Length} : \"{value}\""
                );
            }

            var bytes = new byte[12];

            for (var i = 0; i < 24; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
            }

            return bytes;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int GetCurrentProcessId()
        {
            return Process.GetCurrentProcess().Id;
        }

        private static int GetMachineHash()
        {
            var hostName = Environment.MachineName; // use instead of Dns.HostName so it will work offline

            return 0x00ffffff & hostName.GetHashCode(); // use first 3 bytes of hash
        }

        #region IComparable<ObjectId> Members

        /// <summary>
        ///     Compares two instances of ObjectId
        /// </summary>
        public int CompareTo(ObjectId other)
        {
            var r = Timestamp.CompareTo(other.Timestamp);
            if (r != 0)
            {
                return r;
            }

            r = Machine.CompareTo(other.Machine);
            if (r != 0)
            {
                return r;
            }

            r = Pid.CompareTo(other.Pid);
            if (r != 0)
            {
                return r < 0 ? -1 : 1;
            }

            return Increment.CompareTo(other.Increment);
        }

        #endregion

        #region IEquatable<ObjectId> Members

        /// <summary>
        ///     Checks if this ObjectId is equal to the given object. Returns true
        ///     if the given object is equal to the value of this instance.
        ///     Returns false otherwise.
        /// </summary>
        public bool Equals(ObjectId other)
        {
            return (other != null) &&
                   (Timestamp == other.Timestamp) &&
                   (Machine == other.Machine) &&
                   (Pid == other.Pid) &&
                   (Increment == other.Increment);
        }

        #endregion
    }
}
