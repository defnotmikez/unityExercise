using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
  
  public interface IDefaultController
  {
    Task<string> GetRequest(string url);
    Task<string> PostRequest(string url, string body);
    Task<string> PutRequest(string url, string body);
    Task<string> DeleteRequest(string url, string body);
  }

  


  


