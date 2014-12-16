using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace XPlat.Security.Cryptography
{
    public interface IHashAlgorithm : IDisposable
    {
        //int State { get; }
        byte[] HashValue{get;}
        
        
        //
        // public properties
        //

        int HashSize {get; }

        byte[] Hash { get; }
        

        //
        // public methods
        //

        IHashAlgorithm Create();

        IHashAlgorithm Create(String hashName);

        byte[] ComputeHash(Stream inputStream);

        byte[] ComputeHash(byte[] buffer);

        byte[] ComputeHash(byte[] buffer, int offset, int count);
    
        // ICryptoTransform methods

        // we assume any HashAlgorithm can take input a byte at a time
        int InputBlockSize { get; }

        int OutputBlockSize { get;}

        bool CanTransformMultipleBlocks { get; }

        bool CanReuseTransform   { get;}

        // We implement TransformBlock and TransformFinalBlock here
        int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);
        
        byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);
        // IDisposable methods

        void Dispose();

        void Clear();

        void Dispose(bool disposing);
        
        //
        // abstract public methods
        //

        void Initialize();

        void HashCore(byte[] array, int ibStart, int cbSize);

        byte[] HashFinal();

    }

    public class XHashAlgorithm: IHashAlgorithm  //System.Security.Cryptography.HashAlgorithm, IHashAlgorithm
    {
        protected int HashSizeValue;
        protected byte[] _HashValue;
        private bool _disposed;
        private  byte[] _data;
        private  int _dataSize;
        private  int _totalLength;
        private int _ibStart;
        private  ThirdParty.MD5.ABCDStruct _abcd;
        static System.Security.Cryptography.HashAlgorithm _hashAlgorithm;
        static IHashAlgorithm xHashAlgorithm;


        public XHashAlgorithm() 
        {
            xHashAlgorithm = (IHashAlgorithm)_hashAlgorithm;
        }
        //{xhash = this;}

        public int State {get {return 0;} }
        public virtual int HashSize
        {
            get { return HashSizeValue; }
        }

        public virtual byte[] HashValue
        {
            get
            {
                return _HashValue = _HashValue != null ? _HashValue : new byte[HashSizeValue];}
        }


        public virtual byte[] Hash
        {
            get
            {
                if (_disposed)
                    throw new ObjectDisposedException(null);
                if (State != 0)
                    throw new System.Security.Cryptography.CryptographicException("Cryptography_HashNotYetFinalized");
                                        //CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_HashNotYetFinalized"));
                return (byte[])HashValue.Clone();
            }
        }

        public virtual byte[] HashFinal() 
        {
             _HashValue = ThirdParty.MD5.MD5Core.GetHash(HashValue);
             return _HashValue;
        }

        public void Initialize()
        {
            _data = new byte[64];
            _dataSize = 0;
            _totalLength = 0;
            _abcd = new ThirdParty.MD5.ABCDStruct();
            //Intitial values as defined in RFC 1321
            _abcd.A = 0x67452301;
            _abcd.B = 0xefcdab89;
            _abcd.C = 0x98badcfe;
            _abcd.D = 0x10325476;
        }

        public virtual void Clear()
        {
            _hashAlgorithm.Clear();
        }

        public virtual void HashCore(byte[] array, int ibStart, int cbSize) 
        {
            int startIndex = ibStart;
            _ibStart = ibStart;
            int totalArrayLength = _dataSize + cbSize;
            if (totalArrayLength >= 64)
            {
                Array.Copy(array, startIndex, _data, _dataSize, 64 - _dataSize);
                // Process message of 64 bytes (512 bits)
                ThirdParty.MD5.MD5Core.GetHashBlock(_data, ref _abcd, 0);
                startIndex += 64 - _dataSize;
                totalArrayLength -= 64;
                while (totalArrayLength >= 64)
                {
                    Array.Copy(array, startIndex, _data, 0, 64);
                    ThirdParty.MD5.MD5Core.GetHashBlock(array, ref _abcd, startIndex);
                    totalArrayLength -= 64;
                    startIndex += 64;
                }
                _dataSize = totalArrayLength;
                Array.Copy(array, startIndex, _data, 0, totalArrayLength);
            }
        }

        public virtual void Dispose(bool _disposing) { _hashAlgorithm.Dispose(); }
        public virtual void Dispose() { _hashAlgorithm.Dispose();}
        public virtual byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            return ThirdParty.MD5.MD5Core.GetHashFinalBlock(_data, 0, _dataSize, _abcd, _totalLength * 8);
        }

        public virtual int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            return Convert.ToInt32(_hashAlgorithm.ComputeHash(_data));
        }

        public static IHashAlgorithm Create_()
        {
            return xHashAlgorithm;
        }
        public IHashAlgorithm Create()
        {
            return this;
        }

        public IHashAlgorithm Create(String algName)
        {
            return Create();
        }

        public int InputBlockSize { get { return (1); } }
        
        public int OutputBlockSize { get { return (1); } }
        
        public bool CanTransformMultipleBlocks { get { return true; } }
        
        public bool CanReuseTransform { get { return true; } }

        public byte[] ComputeHash(Stream inputStream) 
        {
            return _hashAlgorithm.ComputeHash(inputStream);
        }

        public byte[] ComputeHash(byte[] buffer) 
        { 
            return _hashAlgorithm.ComputeHash(buffer); 
        }

        public byte[] ComputeHash(byte[] buffer, int offset, int count) 
        {
            return _hashAlgorithm.ComputeHash(buffer, offset, count);
        }
    
    }

    public abstract class MD5 : IHashAlgorithm
    {
        protected int HashSizeValue;
        protected internal byte[] _HashValue;
        protected int _State = 0;
        private bool m_bDisposed = false;
        private static IHashAlgorithm _hashAlgorithm;
        protected MD5()
        {
            HashSizeValue = 128;
        }

        public int HashSize { get { return _hashAlgorithm.HashSize; } }
        public byte[] HashValue 
        { 
            get
            {  
                _HashValue = _hashAlgorithm.HashValue;
                return _HashValue;
            } 
        }

        public void Initialize()
        {
            _hashAlgorithm.Initialize();
        }
        protected int State { get { return _State; } }

        public virtual byte[] Hash
        {
            get
            {
                if (m_bDisposed)
                    throw new ObjectDisposedException(null);
                if (State != 0)
                    throw new System.Security.Cryptography.CryptographicException("Cryptography_HashNotYetFinalized");
                return (byte[])_HashValue.Clone();
            }
        }

        public virtual void HashCore(byte[] array, int ibStart, int cbSize) 
        {
            _hashAlgorithm.HashCore(array, ibStart, cbSize);
        }

        
        public static MD5 Create_()
        {
            return (MD5)(_hashAlgorithm as IHashAlgorithm);
        }

        public static MD5 Create_(String algName)
        {
            return (MD5)(_hashAlgorithm as IHashAlgorithm);
        }
        public IHashAlgorithm Create(String algName)
        {
            return _hashAlgorithm;
        }

        public IHashAlgorithm Create()
        {
            return _hashAlgorithm;
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_HashValue != null)
                    Array.Clear(_HashValue, 0, _HashValue.Length);
                _HashValue = null;
                m_bDisposed = true;
            }

            _hashAlgorithm.Dispose(disposing);
        }

        public virtual void Dispose() 
        {
            _hashAlgorithm.Dispose();
        }

        public virtual void Clear() 
        {
            _hashAlgorithm.Clear();
        }

        public virtual int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            return _hashAlgorithm.TransformBlock(inputBuffer,inputOffset, inputCount, outputBuffer,outputOffset);
        }

        public virtual byte[] HashFinal()
        {
            return _hashAlgorithm.HashFinal();
        }

        public int InputBlockSize { get { return (1);} }

        public int OutputBlockSize { get { return (1); } }

        public bool CanTransformMultipleBlocks { get {return true;} }

        public bool CanReuseTransform { get { return true; } }

        // We implement TransformBlock and TransformFinalBlock here
       public virtual byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
       {
           return _hashAlgorithm.TransformFinalBlock(inputBuffer, inputOffset, inputCount);
       }
       
       public virtual byte[] ComputeHash(byte[] buffer, int offset, int count) 
       {
           return _hashAlgorithm.ComputeHash(buffer, offset, count);
       }

       public virtual byte[] ComputeHash(byte[] buffer) 
       {
           return _hashAlgorithm.ComputeHash(buffer);
       }

       public virtual byte[] ComputeHash(Stream inputStream) 
       {
           return _hashAlgorithm.ComputeHash(inputStream);
       }

    }


//    public abstract class HashAlgorithm : IDisposable, IHashAlgorithm
//    {
//        protected int HashSizeValue;
//        protected internal byte[] HashValue;
//        protected int State = 0;

//        private bool m_bDisposed = false;

//        protected HashAlgorithm() { }

//        //
//        // public properties
//        //

//        public virtual int HashSize
//        {
//            get { return HashSizeValue; }
//        }

//        public virtual byte[] Hash
//        {
//            get
//            {
//                if (m_bDisposed)
//                    throw new ObjectDisposedException(null);
//                if (State != 0)
//                    throw new System.Security.Cryptography.CryptographicException("Cryptography_HashNotYetFinalized");
//                                        //CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_HashNotYetFinalized"));
//                return (byte[])HashValue.Clone();
//            }
//        }

//        //
//        // public methods
//        //

//        static public HashAlgorithm Create()
//        {
//            return Create("System.Security.Cryptography.HashAlgorithm");
//        }

//        static public HashAlgorithm Create(String hashName)
//        {
//            return (HashAlgorithm)CryptoConfig.CreateFromName(hashName);
//        }

//        public byte[] ComputeHash(Stream inputStream)
//        {
//            if (m_bDisposed)
//                throw new ObjectDisposedException(null);

//            // Default the buffer size to 4K.
//            byte[] buffer = new byte[4096];
//            int bytesRead;
//            do
//            {
//                bytesRead = inputStream.Read(buffer, 0, 4096);
//                if (bytesRead > 0)
//                {
//                    HashCore(buffer, 0, bytesRead);
//                }
//            } while (bytesRead > 0);

//            HashValue = HashFinal();
//            byte[] Tmp = (byte[])HashValue.Clone();
//            Initialize();
//            return (Tmp);
//        }

//        public byte[] ComputeHash(byte[] buffer)
//        {
//            if (m_bDisposed)
//                throw new ObjectDisposedException(null);

//            // Do some validation
//            if (buffer == null) throw new ArgumentNullException("buffer");

//            HashCore(buffer, 0, buffer.Length);
//            HashValue = HashFinal();
//            byte[] Tmp = (byte[])HashValue.Clone();
//            Initialize();
//            return (Tmp);
//        }

//        public byte[] ComputeHash(byte[] buffer, int offset, int count)
//        {
//            // Do some validation
//            if (buffer == null)
//                throw new ArgumentNullException("buffer");
//            if (offset < 0)
//                throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
//            if (count < 0 || (count > buffer.Length))
//                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
//            if ((buffer.Length - count) < offset)
//                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
//            Contract.EndContractBlock();

//            if (m_bDisposed)
//                throw new ObjectDisposedException(null);

//            HashCore(buffer, offset, count);
//            HashValue = HashFinal();
//            byte[] Tmp = (byte[])HashValue.Clone();
//            Initialize();
//            return (Tmp);
//        }

//        // ICryptoTransform methods

//        // we assume any HashAlgorithm can take input a byte at a time
//        public virtual int InputBlockSize
//        {
//            get { return (1); }
//        }

//        public virtual int OutputBlockSize
//        {
//            get { return (1); }
//        }

//        public virtual bool CanTransformMultipleBlocks
//        {
//            get { return (true); }
//        }

//        public virtual bool CanReuseTransform
//        {
//            get { return (true); }
//        }

//        // We implement TransformBlock and TransformFinalBlock here
//        public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
//        {
//            // Do some validation, we let BlockCopy do the destination array validation
//            if (inputBuffer == null)
//                throw new ArgumentNullException("inputBuffer");
//            if (inputOffset < 0)
//                throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
//            if (inputCount < 0 || (inputCount > inputBuffer.Length))
//                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
//            if ((inputBuffer.Length - inputCount) < inputOffset)
//                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
//            Contract.EndContractBlock();

//            if (m_bDisposed)
//                throw new ObjectDisposedException(null);

//            // Change the State value
//            State = 1;
//            HashCore(inputBuffer, inputOffset, inputCount);
//            if ((outputBuffer != null) && ((inputBuffer != outputBuffer) || (inputOffset != outputOffset)))
//                Buffer.BlockCopy(inputBuffer, inputOffset, outputBuffer, outputOffset, inputCount);
//            return inputCount;
//        }

//        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
//        {
//            // Do some validation
//            if (inputBuffer == null)
//                throw new ArgumentNullException("inputBuffer");
//            if (inputOffset < 0)
//                throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
//            if (inputCount < 0 || (inputCount > inputBuffer.Length))
//                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
//            if ((inputBuffer.Length - inputCount) < inputOffset)
//                throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
//            Contract.EndContractBlock();

//            if (m_bDisposed)
//                throw new ObjectDisposedException(null);

//            HashCore(inputBuffer, inputOffset, inputCount);
//            HashValue = HashFinal();
//            byte[] outputBytes;
//            if (inputCount != 0)
//            {
//                outputBytes = new byte[inputCount];
//                Buffer.InternalBlockCopy(inputBuffer, inputOffset, outputBytes, 0, inputCount);
//            }
//            else
//            {
//                outputBytes = EmptyArray<Byte>.Value;
//            }
//            // reset the State value
//            State = 0;
//            return outputBytes;
//        }

//        // IDisposable methods

//        // To keep mscorlib compatibility with Orcas, CoreCLR's HashAlgorithm has an explicit IDisposable
//        // implementation. Post-Orcas the desktop has an implicit IDispoable implementation.
//#if FEATURE_CORECLR
//        void IDisposable.Dispose()
//#else
//        public void Dispose()
//#endif // FEATURE_CORECLR
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }

//        public void Clear()
//        {
//            (this as IDisposable).Dispose();
//        }

//        protected virtual void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                if (HashValue != null)
//                    Array.Clear(HashValue, 0, HashValue.Length);
//                HashValue = null;
//                m_bDisposed = true;
//            }
//        }

//        //
//        // abstract public methods
//        //

//        public abstract void Initialize();

//        protected abstract void HashCore(byte[] array, int ibStart, int cbSize);

//        protected abstract byte[] HashFinal();
    
//    }
}
