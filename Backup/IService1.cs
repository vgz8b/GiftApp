﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
namespace WebCamApplicationTheLast
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void DoWork();
        [OperationContract]
        [WebInvoke(Method="GET",ResponseFormat=WebMessageFormat.Json,UriTemplate="?profile={username}")]
        Person getData(string username);
    }
    public class Person
    {
        public string Username { get; set; }
    }
}
