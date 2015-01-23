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

#if WIN_RT
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
#else 
using System.Security;
using PCLCrypto;
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
                return ComputeHash(data, HashAlgorithm.Sha256);    
                //return ComputeHash(data, HashAlgorithmNames.Sha256);
            }

            public byte[] ComputeSHA256Hash(Stream steam)
            {
                return ComputeHash(steam, HashAlgorithm.Sha256);
                //return ComputeHash(steam, HashAlgorithmNames.Sha256);
            }

            public byte[] ComputeMD5Hash(byte[] data)
            {
                return ComputeHash(data, HashAlgorithm.Md5);
                //return ComputeHash(data, HashAlgorithmNames.Md5);
            }

            public byte[] ComputeMD5Hash(Stream steam)
            {
                return ComputeHash(steam, HashAlgorithm.Md5);
                //return ComputeHash(steam, HashAlgorithmNames.Md5);
            }

            private byte[] ComputeHash(byte[] data, string algorithmName)
            {
                throw new NotImplementedException();
                //IBuffer inputBuffer = CryptographicBuffer.CreateFromByteArray(data);

                //var hasher = HashAlgorithmProvider.OpenAlgorithm(algorithmName);
                //var outputBuffer = hasher.HashData(inputBuffer);

                //var hash = outputBuffer.ToArray();
                //return hash;
            }

            private byte[] ComputeHash(byte[] data, HashAlgorithm algorithmName)
            {
                throw new NotImplementedException();

                //***Future implementation using PCLCrypto 

                //IBuffer inputBuffer = CryptographicBuffer.CreateFromByteArray(data);

                //var hasher = HashAlgorithmProvider.OpenAlgorithm(algorithmName);
                //var outputBuffer = hasher.HashData(inputBuffer);

                //var hash = outputBuffer.ToArray();
                //return hash;
            }

            private byte[] ComputeHash(Stream steam, HashAlgorithm algorithmName)
            {

                throw new NotImplementedException();

                //***Future implementation using PCLCrypto 

                //var hasher = HashAlgorithmProvider.OpenAlgorithm(algorithmName);
                //var hash = hasher.CreateHash();

                //int bytesRead = 0;
                //var cryptoBuffer = CryptographicBuffer.CreateFromByteArray(new byte[AWSSDKUtils.DefaultBufferSize]);
                //byte[] buffer = new byte[AWSSDKUtils.DefaultBufferSize];
                //while ((bytesRead = steam.Read(buffer, 0, buffer.Length)) != 0)
                //{
                //    if (bytesRead != cryptoBuffer.Length)
                //        cryptoBuffer = CryptographicBuffer.CreateFromByteArray(new byte[bytesRead]);

                //    buffer.CopyTo(0, cryptoBuffer, 0, bytesRead);
                //    hash.Append(cryptoBuffer);
                //}

                //return hash.GetValueAndReset().ToArray();
            }
            private byte[] ComputeHash(Stream steam, string algorithmName)
            {
                throw new NotImplementedException();

                //var hasher = HashAlgorithmProvider.OpenAlgorithm(algorithmName);
                //var hash = hasher.CreateHash();

                //int bytesRead = 0;
                //var cryptoBuffer = CryptographicBuffer.CreateFromByteArray(new byte[AWSSDKUtils.DefaultBufferSize]);
                //byte[] buffer = new byte[AWSSDKUtils.DefaultBufferSize];
                //while ((bytesRead = steam.Read(buffer, 0, buffer.Length)) != 0)
                //{
                //    if (bytesRead != cryptoBuffer.Length)
                //        cryptoBuffer = CryptographicBuffer.CreateFromByteArray(new byte[bytesRead]);

                //    buffer.CopyTo(0, cryptoBuffer, 0, bytesRead);
                //    hash.Append(cryptoBuffer);
                //}

                //return hash.GetValueAndReset().ToArray();
            }

            public string HMACSign(string data, string key, SigningAlgorithm algorithmName)
            {
                var binaryData = Encoding.UTF8.GetBytes(data);
                return HMACSign(binaryData, key, algorithmName);
            }

            public string HMACSign(byte[] data, string key, SigningAlgorithm algorithmName)
            {
                throw new NotImplementedException(); 
                //var crypt = MacAlgorithmProvider.OpenAlgorithm(ConvertToAlgorithName(algorithmName));

                //if (String.IsNullOrEmpty(key))
                //{
                //    throw new ArgumentNullException("key", "Please specify a Secret Signing Key.");
                //}

                //if (data == null || data.Length == 0)
                //{
                //    throw new ArgumentNullException("data", "Please specify data to sign.");
                //}

                //if (null == crypt)
                //{
                //    throw new ArgumentNullException("algorithm", "Please specify a KeyedHashAlgorithm to use.");
                //}

                //IBuffer dataBuffer = CryptographicBuffer.CreateFromByteArray(data);
                //var keyBuffer = CryptographicBuffer.ConvertStringToBinary(key, BinaryStringEncoding.Utf8);
                //var cryptoKey = crypt.CreateKey(keyBuffer);

                //var sigBuffer = CryptographicEngine.Sign(cryptoKey, dataBuffer);
                //string signature = CryptographicBuffer.EncodeToBase64String(sigBuffer);
                //return signature;
            }

            public byte[] HMACSignBinary(byte[] data, byte[] key, SigningAlgorithm algorithmName)
            {
                throw new NotImplementedException();

                //var crypt = MacAlgorithmProvider.OpenAlgorithm(ConvertToAlgorithName(algorithmName));

                //if (key == null || key.Length == 0)
                //{
                //    throw new ArgumentNullException("key", "Please specify a Secret Signing Key.");
                //}

                //if (data == null || data.Length == 0)
                //{
                //    throw new ArgumentNullException("data", "Please specify data to sign.");
                //}

                //if (null == crypt)
                //{
                //    throw new ArgumentNullException("algorithm", "Please specify a KeyedHashAlgorithm to use.");
                //}

                //IBuffer dataBuffer = CryptographicBuffer.CreateFromByteArray(data);
                //var keyBuffer = CryptographicBuffer.CreateFromByteArray(key);
                //var cryptoKey = crypt.CreateKey(keyBuffer);

                //var sigBuffer = CryptographicEngine.Sign(cryptoKey, dataBuffer);
                //return sigBuffer.ToArray();
            }

            string ConvertToAlgorithName(SigningAlgorithm algorithm)
            {
                throw new NotImplementedException();

                //switch (algorithm)
                //{
                //    case SigningAlgorithm.HmacSHA256:
                //        return MacAlgorithmNames.HmacSha256;
                //    case SigningAlgorithm.HmacSHA1:
                //        return MacAlgorithmNames.HmacSha1;
                //    default:
                //        throw new Exception("Unknown signing algorithm: " + algorithm.ToString());
                //}
            }
        }
    }
}
