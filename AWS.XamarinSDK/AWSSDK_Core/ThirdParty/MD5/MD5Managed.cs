//Copyright (c) Microsoft Corporation.  All rights reserved.
using System;
using System.IO;
using System.Security.Cryptography;
using XPlat.Security.Cryptography;

// **************************************************************
// * Raw implementation of the MD5 hash algorithm
// * from RFC 1321.
// *
// * Written By: Reid Borsuk and Jenny Zheng
// * Copyright (c) Microsoft Corporation.  All rights reserved.
// **************************************************************

namespace ThirdParty.MD5
{

//#if SILVERLIGHT
//    public class MD5Managed : HashAlgorithm
//#else
    public class MD5Managed : XPlat.Security.Cryptography.MD5, IHashAlgorithm
//#endif
    {
        private byte[] _data;
        private ABCDStruct _abcd;
        private Int64 _totalLength;
        private int _dataSize;
        //Int32 HashSizeValue;
        //byte[] HashValue;

        public MD5Managed() 
        {
            base.HashSizeValue  = 0x80;
            this.Initialize();
            
        }
        
        public void Initialize()
        {
            _data = new byte[64];
            _dataSize = 0;
            _totalLength = 0;
            _abcd = new ABCDStruct();
            //Intitial values as defined in RFC 1321
            _abcd.A = 0x67452301;
            _abcd.B = 0xefcdab89;
            _abcd.C = 0x98badcfe;
            _abcd.D = 0x10325476;
        }

        public void HashCore(byte[] array, int ibStart, int cbSize)
        {
            int startIndex = ibStart;
            int totalArrayLength = _dataSize + cbSize;
            if (totalArrayLength >= 64)
            {
                Array.Copy(array, startIndex, _data, _dataSize, 64 - _dataSize);
                // Process message of 64 bytes (512 bits)
                MD5Core.GetHashBlock(_data, ref _abcd, 0);
                startIndex += 64 - _dataSize;
                totalArrayLength -= 64;
                while (totalArrayLength >= 64)
                {
                    Array.Copy(array, startIndex, _data, 0, 64);
                    MD5Core.GetHashBlock(array, ref _abcd, startIndex);
                    totalArrayLength -= 64;
                    startIndex += 64;
                }
                _dataSize = totalArrayLength;
                Array.Copy(array, startIndex, _data, 0, totalArrayLength);
            }
            else
            {
                Array.Copy(array, startIndex, _data, _dataSize, cbSize);
                _dataSize = totalArrayLength;
            }
            _totalLength += cbSize;
        }

        public byte[] HashFinal()
        {
            
            base._HashValue = MD5Core.GetHashFinalBlock(_data, 0, _dataSize, _abcd, _totalLength * 8);
            return HashValue;
        }

        public void Dispose() 
        {
            base.Dispose();
        }

        public void Dispose(bool dispose) 
        {
            base.Dispose(true);
        }

        public void Clear() 
        {
            base.Clear();
        }

        public int InputBlockSize { get { return base.InputBlockSize; } }

        public int OutputBlockSize { get { return base.OutputBlockSize; } }

        public byte[] TransformFinalBlock(byte[] _inputBuffer, int _inputOffset, int _inputCount)
        {
            return base.TransformFinalBlock(_inputBuffer, _inputOffset, _inputCount);
        }

        public bool CanReuseTransform { get { return base.CanReuseTransform; } }

        public bool CanTransformMultipleBlocks { get { return base.CanTransformMultipleBlocks; } }

        public int TransformBlock(byte[] _inputBuffer, int _inputOffset, int _inputCount, byte[] _outputBuffer, int _outputOffset) 
        { return base.TransformBlock(_inputBuffer, _inputOffset, _inputCount, _outputBuffer, _outputOffset); }

        public byte[] ComputeHash(byte[] buffer) 
        {
            return base.ComputeHash(buffer);
        }

        public byte[] ComputeHash(byte[] buffer, int inputOffset, int Count) 
        {
            return base.ComputeHash(buffer, inputOffset, Count);
        }

        public byte[] ComputeHash(Stream inputStream) 
        {
            return base.ComputeHash(inputStream);
        }

        public IHashAlgorithm Create() 
        {
            return XPlat.Security.Cryptography.MD5.Create_();
        }

        public IHashAlgorithm Create(String hashName) 
        {
            return XPlat.Security.Cryptography.MD5.Create_(hashName);
        }

        public int HashSize { get { return base.HashSizeValue; } }

        public byte[] HashValue { get { return base._HashValue; } }
    }
}