/*******************************************************************************
 *  Copyright 2008-2013 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *  Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *  this file except in compliance with the License. A copy of the License is located at
 *
 *  http://aws.amazon.com/apache2.0
 *
 *  or in the "license" file accompanying this file.
 *  This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *  CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *  specific language governing permissions and limitations under the License.
 * *****************************************************************************
 *    __  _    _  ___
 *   (  )( \/\/ )/ __)
 *   /__\ \    / \__ \
 *  (_)(_) \/\/  (___/
 *
 *  AWS SDK for .NET
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

#if WinRT || Windows_Phone
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
#endif

using Amazon.Runtime;

namespace Amazon.Util
{
    internal static partial class CryptoUtilFactory
    {
        partial class CryptoUtil : ICryptoUtil
        {
            public byte[] ComputeSHA256Hash(byte[] data)
            {
                throw new NotImplementedException();
            }

            public byte[] ComputeSHA256Hash(Stream steam)
            {
                throw new NotImplementedException();
            }

            public byte[] ComputeMD5Hash(byte[] data)
            {
                throw new NotImplementedException();
            }

            public byte[] ComputeMD5Hash(Stream steam)
            {
                throw new NotImplementedException();
            }

            private byte[] ComputeHash(byte[] data, string algorithmName)
            {
                throw new NotImplementedException();
            }

            private byte[] ComputeHash(Stream steam, string algorithmName)
            {
                throw new NotImplementedException();
            }

            public string HMACSign(string data, string key, SigningAlgorithm algorithmName)
            {
                var binaryData = Encoding.UTF8.GetBytes(data);
                return HMACSign(binaryData, key, algorithmName);
            }

            public string HMACSign(byte[] data, string key, SigningAlgorithm algorithmName)
            {
                throw new NotImplementedException();
            }

            public byte[] HMACSignBinary(byte[] data, byte[] key, SigningAlgorithm algorithmName)
            {

                throw new NotImplementedException();
            }

            string ConvertToAlgorithName(SigningAlgorithm algorithm)
            {
                throw new NotImplementedException();
            }
        }
    }
}
