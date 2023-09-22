using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.Text;


namespace Assets.Scripts.Application.APIController
{       
    public class DefaultController : IDefaultController 
    {   
      
        public async Task<string> GetRequest(string url) 
        {
            using (var request = UnityWebRequest.Get(url))
            {
                request.SetRequestHeader("Content-Type", "application/json");

                var response = await ProcessRequest(request);
                return response;
            };
            
        }

        public async Task<string> PostRequest(string url, string body) 
        {

            using (var request = UnityWebRequest.Post(url, body))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
                request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
                var response = await ProcessRequest(request);
                request.uploadHandler.Dispose();
                return response;
            };
            
        }

        public async Task<string> DeleteRequest(string url, string body)
        {

            using (var request = UnityWebRequest.Delete(url))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                byte[] bodyRaw = Encoding.UTF8.GetBytes(body);

                request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);

                var response = await ProcessRequest(request);
                request.uploadHandler.Dispose();
                return response;
            };
            
            
        }

        public async Task<string> PutRequest(string url, string body)
        {
            using (var request = UnityWebRequest.Put(url, body))
            {
                request.SetRequestHeader("Content-Type", "application/json");
                byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
                request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
                
                var response = await ProcessRequest(request);
                request.uploadHandler.Dispose();
                return response;
            };
            
        }
        
        private async Task<string> ProcessRequest(UnityWebRequest request) 
        {
            using (request.downloadHandler = new DownloadHandlerBuffer())
            {
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield();

                }

                if (request.result != UnityWebRequest.Result.Success)
                {

                    Debug.LogError("Failed Request." + request.error);
                }
                string responseText = request.downloadHandler.text;
                request.Dispose();
                return responseText;

            };
           
        }
    } 
}



