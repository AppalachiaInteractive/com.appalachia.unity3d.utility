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
    public class ObjectID : IComparable<ObjectID>, IEquatable<ObjectID>
    {
        // static constructor
        static ObjectID()
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
        ///     Initializes a new empty instance of the ObjectID class.
        /// </summary>
        public ObjectID()
        {
            Update(0, 0, 0, 0);
        }

        /// <summary>
        ///     Initializes a new instance of the ObjectID class from ObjectID vars.
        /// </summary>
        public ObjectID(int timestamp, int machine, short pid, int increment)
        {
            Update(timestamp, machine, pid, increment);
        }

        /// <summary>
        ///     Initializes a new instance of ObjectID class from another ObjectID.
        /// </summary>
        public ObjectID(ObjectID from)
        {
            _timestamp = from.Timestamp;
            _machine = from.Machine;
            _pid = from.Pid;
            _increment = from.Increment;
        }

        /// <summary>
        ///     Initializes a new instance of the ObjectID class from hex string.
        /// </summary>
        public ObjectID(string value) : this(FromHex(value))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the ObjectID class from byte array.
        /// </summary>
        public ObjectID(byte[] bytes, int startIndex = 0)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            var timestamp = (bytes[startIndex + 0] << 24) +
                            (bytes[startIndex + 1] << 16) +
                            (bytes[startIndex + 2] << 8) +
                            bytes[startIndex + 3];

            var machine = (bytes[startIndex + 4] << 16) + (bytes[startIndex + 5] << 8) + bytes[startIndex + 6];

            var pid = (short)((bytes[startIndex + 7] << 8) + bytes[startIndex + 8]);

            var increment = (bytes[startIndex + 9] << 16) + (bytes[startIndex + 10] << 8) + bytes[startIndex + 11];

            Update(timestamp, machine, pid, increment);
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

        private string _displayString;

        #endregion

        /// <summary>
        ///     A zero 12-bytes ObjectID
        /// </summary>
        public static ObjectID Empty => new ObjectID();

        public bool IsEmpty => this == Empty;

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
        ///     Creates a new ObjectID.
        /// </summary>
        public static ObjectID NewObjectID()
        {
            CalculateNew(out var timestamp, out var machine, out var pid, out var increment);
            return new ObjectID(timestamp, machine, pid, increment);
        }

        public static bool operator ==(ObjectID lhs, ObjectID rhs)
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

        public static bool operator >(ObjectID lhs, ObjectID rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }

        public static bool operator >=(ObjectID lhs, ObjectID rhs)
        {
            return lhs.CompareTo(rhs) >= 0;
        }

        public static bool operator !=(ObjectID lhs, ObjectID rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator <(ObjectID lhs, ObjectID rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }

        public static bool operator <=(ObjectID lhs, ObjectID rhs)
        {
            return lhs.CompareTo(rhs) <= 0;
        }

        /// <summary>
        ///     Determines whether the specified object is equal to this instance.
        /// </summary>
        public override bool Equals(object other)
        {
            return Equals(other as ObjectID);
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
            if (string.IsNullOrWhiteSpace(_displayString))
            {
                _displayString = BitConverter.ToString(ToByteArray()).Replace("-", "").ToLower();
            }

            return _displayString;
        }

        public void EnsureNotEmpty()
        {
            if (this == Empty)
            {
                CalculateNew(out _timestamp, out _machine, out _pid, out _increment);
            }
        }

        /// <summary>
        ///     Represent ObjectID as 12 bytes array
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

        private static void CalculateNew(out int timestamp, out int machine, out short pid, out int increment)
        {
            timestamp = (int)(long)Math.Floor((DateTime.UtcNow - Constants.UNIX_EPOCH).TotalSeconds);

            machine = _currentMachine;

            pid = _currentProcessID;

            increment = Interlocked.Increment(ref _machineIncrement) & 0x00ffffff;
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
                    $"ObjectID strings should be 24 hex characters, got {value.Length} : \"{value}\""
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

        private void Update(int timestamp, int machine, short pid, int increment)
        {
            _timestamp = timestamp;
            _machine = machine;
            _pid = pid;
            _increment = increment;
        }

        #region IComparable<ObjectID> Members

        /// <summary>
        ///     Compares two instances of ObjectID
        /// </summary>
        public int CompareTo(ObjectID other)
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

        #region IEquatable<ObjectID> Members

        /// <summary>
        ///     Checks if this ObjectID is equal to the given object. Returns true
        ///     if the given object is equal to the value of this instance.
        ///     Returns false otherwise.
        /// </summary>
        public bool Equals(ObjectID other)
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
