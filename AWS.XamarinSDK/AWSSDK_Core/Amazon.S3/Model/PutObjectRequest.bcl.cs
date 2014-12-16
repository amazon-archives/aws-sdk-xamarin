/*
 * Copyright 2010-2013 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 * 
 *  http://aws.amazon.com/apache2.0
 * 
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;


using Amazon.Runtime;
using Amazon.Runtime.Internal;
using PCLStorage;
using System.Threading;
using System.Threading.Tasks;

namespace Amazon.S3.Model
{
    /// <summary>
    /// Container for the parameters to the PutObject operation.
    /// <para>Adds an object to a bucket.</para>
    /// </summary>
    public partial class PutObjectRequest : PutWithACLRequest
    {
        internal void SetupForFilePath()
        {
           
            
            //this.InputStream = new FileStream(this.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            //*TEW: Code added to make IO available cross-plat in PCL
            IFile file;
            Func<Task> fileStream = async() => 
            {
                string dirPath = this.filePath.Substring(0, filePath.LastIndexOf(PortablePath.DirectorySeparatorChar) - 1);
                file = await FileSystem.Current.GetFileFromPathAsync(this.FilePath);
                this.InputStream =  await file.OpenAsync(FileAccess.Read);
            };

            fileStream();

            if (string.IsNullOrEmpty(this.Key))
            {
                this.Key = Path.GetFileName(this.FilePath);
            }
        }
    }
}
