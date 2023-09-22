using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using Assets.Scripts.Application.APIController;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Assets.Scripts.Model;
using Assets.Scripts.Application.Controllers;


namespace Assets.Scripts.Application.APIController.MarmotaController
{
   public static class MarmotaReqController
   {
      public async static Task<List<Marmota>> GetAllMarmotas() 
      { 
         var controller = new DefaultController();
         var converter = new JSONConverter<List<Marmota>>();
         var res = await controller.GetRequest("http://localhost:7071/api/GetAllMarmotas");
         var convertedJSON = converter.ConvertFromJSON(res);
         return convertedJSON;
        
      }

      public async static Task<Marmota> CreateMarmotaAsync(Marmota body)
      {
            var controller = new DefaultController();
            var converter = new JSONConverter<Marmota>();
            var convertToJson = converter.ConvertToJSON(body);
            var res = await controller.PostRequest("http://localhost:7071/api/CreateMarmota", convertToJson);
            var convertedJSON = converter.ConvertFromJSON(res);
            return convertedJSON;
      }

      public async static Task<string> DeleteMarmotaAsync(Marmota body)
      {
            var controller = new DefaultController();
            var converter = new JSONConverter<Marmota>();
            var convertToJson = converter.ConvertToJSON(body);
            var res = await controller.DeleteRequest("http://localhost:7071/api/DeleteMarmota", convertToJson);
            return res;
      }

      public async static Task<Marmota> UpdateMarmotaAsync(Marmota body)
      {
            var controller = new DefaultController();
            var converter = new JSONConverter<Marmota>();
            var convertToJson = converter.ConvertToJSON(body);
            var res = await controller.PutRequest("http://localhost:7071/api/UpdateMarmota", convertToJson);
            var convertedJSON = converter.ConvertFromJSON(res);
            return convertedJSON;
        }
   }
}
