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
using System.Reflection;
using System.Linq;
using System.Text;
using System.Net;

namespace Amazon.Util
{
    public static partial class AWSSDKUtils
    {
			#if __IOS__
			static string _userAgentBaseName = "aws-sdk-dotnet-ios";
			#elif __ANDROID__
			static string _userAgentBaseName = "aws-sdk-dotnet-android";
			#endif

        static string DetermineRuntime()
        {
				#if __IOS__
				return "iOS";
				#elif __ANDROID__
				return "Android";
				#endif
			return "";
        }

        static string DetermineFramework()
        {
            return "4.0";
        }

        static string DetermineOSVersion()
        {
				#if __IOS__
				return MonoTouch.Constants.Version;
				#elif __ANDROID__
				return  Android.OS.Build.VERSION.Release;
				#endif

            return "Unknown";
        }

        internal static void ForceCanonicalPathAndQuery(Uri uri)
        {
        }

        internal static void PreserveStackTrace(Exception exception)
        {
        }

        internal static int GetConnectionLimit(int? clientConfigValue)
        {
            if (clientConfigValue.HasValue)
                return clientConfigValue.Value;

            return AWSSDKUtils.DefaultConnectionLimit;
        }

        public static void Sleep(int ms)
        {
            new System.Threading.ManualResetEvent(false).WaitOne(ms);
        }

		private const int _defaultMaxIdleTime = 100 * 1000;
		internal static int GetMaxIdleTime(int? clientConfigValue)
		{
			// MaxIdleTime has been explicitly set on the client.
			if (clientConfigValue.HasValue)
				return clientConfigValue.Value;

			// If default has been left at the system default return the SDK default.
			if (ServicePointManager.MaxServicePointIdleTime == _defaultMaxIdleTime)
				return AWSSDKUtils.DefaultMaxIdleTime;

			// The system default has been explicitly changed so we will honor that value.
			return ServicePointManager.MaxServicePointIdleTime;
		}
    }
}
