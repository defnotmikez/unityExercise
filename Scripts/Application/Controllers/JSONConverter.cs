using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using Newtonsoft.Json;

namespace Assets.Scripts.Application.Controllers
{
    public class JSONConverter<T>
    {

    public T ConvertFromJSON(string json) 
    {
           
        var result = JsonConvert.DeserializeObject<T>(json);
        return result;
       
    }

    public string ConvertToJSON(T obj)
    {
            
        var result = JsonConvert.SerializeObject(obj);
        return result;
       
    }

    }
}
