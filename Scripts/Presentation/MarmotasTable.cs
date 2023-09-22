using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.Application.APIController.MarmotaController;
using Assets.Scripts.Model;
using System.Linq;
using System;


public class MarmotasTable : MonoBehaviour
{
   [SerializeField]
   private Transform entryContainer;
   [SerializeField]
   private Transform entryTemplate;
   private List<Transform> marmotasEntryTransformList;
   private Button editButton;
   private Button deleteButton;
   private Button saveButton;
   private Button cancelButton;
   private TMPro.TMP_InputField editNameInputField;
   private TMPro.TMP_InputField editAgeInputField;
   private TMPro.TMP_InputField editHeightInputField;
   private TMPro.TMP_InputField editWeightInputField;
   private Transform currentlyEditingRow = null;

    private void Start()
   {
        GetMarmotas();
   }

    public async void  GetMarmotas()
    {
        
        marmotasEntryTransformList = new List<Transform>();
        entryTemplate.gameObject.SetActive(false);

        var marmotas = await MarmotaReqController.GetAllMarmotas();
        
        foreach (Marmota marmota in marmotas)
        {
            CreateMarmotasEntryTransform(marmota);
          
        }
    }

    public void RemoveAllEntries()
    {
        
        foreach (Transform entry in marmotasEntryTransformList)
        {
            Destroy(entry.gameObject);
        }

        
        marmotasEntryTransformList.Clear();
    }

    private void CreateMarmotasEntryTransform(Marmota marmota)
    {
        float templateHeight = 50f;

        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
       

        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * marmotasEntryTransformList.Count);
        
        entryTransform.gameObject.SetActive(true);

       

        entryTransform.Find("marmotaNameTableText").GetComponent<TextMeshProUGUI>().text = marmota.Name;
        entryTransform.Find("marmotaAgeTableText").GetComponent<TextMeshProUGUI>().text = marmota.Age.ToString();
        entryTransform.Find("marmotaHeightTableText").GetComponent<TextMeshProUGUI>().text = marmota.Height.ToString();
        entryTransform.Find("marmotaWeightTableText").GetComponent<TextMeshProUGUI>().text = marmota.Weight.ToString();

        editNameInputField = entryTransform.Find("editName").GetComponent<TMPro.TMP_InputField>();
        editAgeInputField = entryTransform.Find("editAge").GetComponent<TMPro.TMP_InputField>();
        editHeightInputField = entryTransform.Find("editHeight").GetComponent<TMPro.TMP_InputField>();
        editWeightInputField = entryTransform.Find("editWeight").GetComponent<TMPro.TMP_InputField>();

        editNameInputField.gameObject.SetActive(false);
        editAgeInputField.gameObject.SetActive(false);
        editHeightInputField.gameObject.SetActive(false);
        editWeightInputField.gameObject.SetActive(false);
        
        
        

        saveButton = entryTransform.Find("saveButton").GetComponent<Button>();
        cancelButton = entryTransform.Find("cancelButton").GetComponent<Button>();
        saveButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);

        editButton = entryTransform.Find("editButton").GetComponent<Button>();
        deleteButton = entryTransform.Find("deleteButton").GetComponent<Button>();
        editButton.gameObject.SetActive(true);
        deleteButton.gameObject.SetActive(true);



        editButton.onClick.AddListener(() =>EditMarmota(marmota, entryTransform));
        deleteButton.onClick.AddListener(() => DeleteMarmota(marmota));

        marmotasEntryTransformList.Add(entryTransform);

       
    }

    private void EditMarmota(Marmota marmota, Transform entryTransform)
    {

        editNameInputField = entryTransform.Find("editName").GetComponent<TMPro.TMP_InputField>();
        editAgeInputField = entryTransform.Find("editAge").GetComponent<TMPro.TMP_InputField>();
        editHeightInputField = entryTransform.Find("editHeight").GetComponent<TMPro.TMP_InputField>();
        editWeightInputField = entryTransform.Find("editWeight").GetComponent<TMPro.TMP_InputField>();

        editNameInputField.gameObject.SetActive(true);
        editAgeInputField.gameObject.SetActive(true);
        editHeightInputField.gameObject.SetActive(true);
        editWeightInputField.gameObject.SetActive(true);

        editNameInputField.text = marmota.Name;
        editAgeInputField.text = marmota.Age.ToString();
        editHeightInputField.text = marmota.Height.ToString();
        editWeightInputField.text = marmota.Weight.ToString();


        saveButton = entryTransform.Find("saveButton").GetComponent<Button>();
        cancelButton = entryTransform.Find("cancelButton").GetComponent<Button>();
        editButton = entryTransform.Find("editButton").GetComponent<Button>();
        deleteButton = entryTransform.Find("deleteButton").GetComponent<Button>();
        saveButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
        editButton.gameObject.SetActive(false);
        deleteButton.gameObject.SetActive(false);

        if(currentlyEditingRow != null)
        {
            CancelEdit(currentlyEditingRow);
        }

        currentlyEditingRow = entryTransform;
        cancelButton.onClick.AddListener(() => CancelEdit(entryTransform));
        saveButton.onClick.AddListener(() => SaveEdit(entryTransform, marmota.Id));

    }

    private void CancelEdit(Transform entryTransform)
    {
        
        editNameInputField = entryTransform.Find("editName").GetComponent<TMPro.TMP_InputField>();
        editAgeInputField = entryTransform.Find("editAge").GetComponent<TMPro.TMP_InputField>();
        editHeightInputField = entryTransform.Find("editHeight").GetComponent<TMPro.TMP_InputField>();
        editWeightInputField = entryTransform.Find("editWeight").GetComponent<TMPro.TMP_InputField>();

        editNameInputField.gameObject.SetActive(false);
        editAgeInputField.gameObject.SetActive(false);
        editHeightInputField.gameObject.SetActive(false);
        editWeightInputField.gameObject.SetActive(false);

    
        saveButton = entryTransform.Find("saveButton").GetComponent<Button>();
        cancelButton = entryTransform.Find("cancelButton").GetComponent<Button>();
        editButton = entryTransform.Find("editButton").GetComponent<Button>();
        deleteButton = entryTransform.Find("deleteButton").GetComponent<Button>();
        saveButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        editButton.gameObject.SetActive(true);
        deleteButton.gameObject.SetActive(true);
        currentlyEditingRow = null;
    }

    private async void SaveEdit(Transform entryTransform, string marmotaId)
    {
        editNameInputField = entryTransform.Find("editName").GetComponent<TMPro.TMP_InputField>();
        editAgeInputField = entryTransform.Find("editAge").GetComponent<TMPro.TMP_InputField>();
        editHeightInputField = entryTransform.Find("editHeight").GetComponent<TMPro.TMP_InputField>();
        editWeightInputField = entryTransform.Find("editWeight").GetComponent<TMPro.TMP_InputField>();

        var name = editNameInputField.text.Trim((char)8203);
        var age = editAgeInputField.text.Trim((char)8203);
        var height = editHeightInputField.text.Trim((char)8203);
        var weight = editWeightInputField.text.Trim((char)8203);

        var updateMarmota = new Marmota
        {
            Id = marmotaId.ToString(),
            Name = name,
            Age = Convert.ToInt32(age),
            Height = Convert.ToDouble(height),
            Weight = Convert.ToDouble(weight)
        };
       
        await MarmotaReqController.UpdateMarmotaAsync(updateMarmota);
        RemoveAllEntries();
        GetMarmotas();

    }

    private async void DeleteMarmota(Marmota marmota)
    {

        
        var res = await MarmotaReqController.DeleteMarmotaAsync(marmota);
        if(res == "Entity has been deleted")
        {
            RemoveAllEntries();
            GetMarmotas();
        }

    }
}
