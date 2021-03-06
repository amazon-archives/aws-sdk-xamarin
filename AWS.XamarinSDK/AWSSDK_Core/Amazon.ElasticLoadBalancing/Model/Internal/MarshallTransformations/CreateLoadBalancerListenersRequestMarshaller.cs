/*
 * Copyright 2010-2014 Amazon.com, Inc. or its affiliates. All Rights Reserved.
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

/*
 * Do not modify this file. This file is generated from the elasticloadbalancing-2012-06-01.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml.Serialization;

using Amazon.ElasticLoadBalancing.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
namespace Amazon.ElasticLoadBalancing.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// CreateLoadBalancerListeners Request Marshaller
    /// </summary>       
    public class CreateLoadBalancerListenersRequestMarshaller : IMarshaller<IRequest, CreateLoadBalancerListenersRequest> , IMarshaller<IRequest,AmazonWebServiceRequest>
    {
        public IRequest Marshall(AmazonWebServiceRequest input)
        {
            return this.Marshall((CreateLoadBalancerListenersRequest)input);
        }
    
        public IRequest Marshall(CreateLoadBalancerListenersRequest publicRequest)
        {
            IRequest request = new DefaultRequest(publicRequest, "Amazon.ElasticLoadBalancing");
            request.Parameters.Add("Action", "CreateLoadBalancerListeners");
            request.Parameters.Add("Version", "2012-06-01");

            if(publicRequest != null)
            {
                if(publicRequest.IsSetListeners())
                {
                    int publicRequestlistValueIndex = 1;
                    foreach(var publicRequestlistValue in publicRequest.Listeners)
                    {
                        if(publicRequestlistValue.IsSetInstancePort())
                        {
                            request.Parameters.Add("Listeners" + "." + "member" + "." + publicRequestlistValueIndex + "." + "InstancePort", StringUtils.FromInt(publicRequestlistValue.InstancePort));
                        }
                        if(publicRequestlistValue.IsSetInstanceProtocol())
                        {
                            request.Parameters.Add("Listeners" + "." + "member" + "." + publicRequestlistValueIndex + "." + "InstanceProtocol", StringUtils.FromString(publicRequestlistValue.InstanceProtocol));
                        }
                        if(publicRequestlistValue.IsSetLoadBalancerPort())
                        {
                            request.Parameters.Add("Listeners" + "." + "member" + "." + publicRequestlistValueIndex + "." + "LoadBalancerPort", StringUtils.FromInt(publicRequestlistValue.LoadBalancerPort));
                        }
                        if(publicRequestlistValue.IsSetProtocol())
                        {
                            request.Parameters.Add("Listeners" + "." + "member" + "." + publicRequestlistValueIndex + "." + "Protocol", StringUtils.FromString(publicRequestlistValue.Protocol));
                        }
                        if(publicRequestlistValue.IsSetSSLCertificateId())
                        {
                            request.Parameters.Add("Listeners" + "." + "member" + "." + publicRequestlistValueIndex + "." + "SSLCertificateId", StringUtils.FromString(publicRequestlistValue.SSLCertificateId));
                        }
                        publicRequestlistValueIndex++;
                    }
                }
                if(publicRequest.IsSetLoadBalancerName())
                {
                    request.Parameters.Add("LoadBalancerName", StringUtils.FromString(publicRequest.LoadBalancerName));
                }
            }
            return request;
        }
    }
}