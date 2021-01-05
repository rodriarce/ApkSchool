using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class MainMenuManager : MonoBehaviour
{

    public GameObject menuParents;
    public GameObject menuChildren;
    public Toggle toggleSound;
    public Toggle toggleSoundTwo;
    public Dropdown days;
    public Dropdown daysChild;
    public Dropdown months;
    public Dropdown monthsChild;
    public Dropdown years;
    public Dropdown yearsChild;
    public TextMeshProUGUI textMath;
    public TMP_InputField inputMath;
    public TextMeshProUGUI textInfo;
    private int numOne;
    private int numTwo;
    private int result;
    public GameObject panelParent;
    
    


    public void SelectScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }

    private void Start()
    {
        SetDropDown();
        SetRandom();
        
    }


    private void SetDropDown()
    {

        List<Dropdown.OptionData> daysOptions = new List<Dropdown.OptionData>();
        for (int i = 1; i <= 30; i++)
        {
            Dropdown.OptionData newDay = new Dropdown.OptionData();
            newDay.text = i.ToString();
            daysOptions.Add(newDay);
        }
        days.AddOptions(daysOptions);
        daysChild.AddOptions(daysOptions);
        List<Dropdown.OptionData> monthOptions = new List<Dropdown.OptionData>();
        for (int i = 1; i <= 12; i++)
        {
            Dropdown.OptionData newMonth = new Dropdown.OptionData();
            newMonth.text = i.ToString();
            monthOptions.Add(newMonth);
        }
        months.AddOptions(monthOptions);
        monthsChild.AddOptions(monthOptions);

        List<Dropdown.OptionData> yearsOptions = new List<Dropdown.OptionData>();
        for (int i = 1940; i <= 2015; i++)
        {
            Dropdown.OptionData yearDate = new Dropdown.OptionData();
            yearDate.text = i.ToString();
            yearsOptions.Add(yearDate);
        }
        years.AddOptions(yearsOptions);
        yearsChild.AddOptions(yearsOptions);








        //days.

    }
    public void SelectChild(string childName)
    {
        MenuManager.menuManager.childName = childName;

    }
    public void SelectParent(string parentName)
    {
        MenuManager.menuManager.parentName = parentName;
        
    }


    public void SetRandom()
    {
         numOne = UnityEngine.Random.Range(10, 100);
         numTwo = UnityEngine.Random.Range(10, 100);
        result = numOne + numTwo;
        textMath.text = numOne.ToString() + " + " + numTwo.ToString() +" = ?";
        
        

    }

    public void MakeOperation()
    {

        int inputValue = Int32.Parse(inputMath.text);
        if (result == inputValue)
        {
            Application.OpenURL("https://excel-r-8.co.uk/active-parents/");
            panelParent.SetActive(false);
            //LoadWeb(true);
        }
        else
        {
            textInfo.text = "Wrong Answer";
            
        }
        
    }

   



}
