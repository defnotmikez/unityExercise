using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.Application.APIController.MarmotaController;
using Assets.Scripts.Model;
using System.Linq;
using System;
using System.ComponentModel;


public class CreateMarmota : MonoBehaviour
{
  
    [SerializeField]private TextMeshProUGUI  inputNameText;
    [SerializeField]private TextMeshProUGUI  inputAgeText;
    [SerializeField]private TextMeshProUGUI  inputHeightText;
    [SerializeField]private TextMeshProUGUI  inputWeightText;
    [SerializeField]private MarmotasTable marmotasTable;

    public async void CreateMarmotaButton()
    {     
         var name = inputNameText.text.Trim((char)8203);
         var age = inputAgeText.text.Trim((char)8203);
         var height = inputHeightText.text.Trim((char)8203);
         var weight = inputWeightText.text.Trim((char)8203);
         var marmota = new Marmota
         {   

             Name = name,
             Age = Convert.ToInt32(age),
             Height = Convert.ToDouble(height),
             Weight =  Convert.ToDouble(weight)
         };

         await MarmotaReqController.CreateMarmotaAsync(marmota);
         marmotasTable.RemoveAllEntries();
         marmotasTable.GetMarmotas();
    }
}
