using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{

    public List<GameObject> childrenAvatars;
    public List<GameObject> parentAvatars;
    public GameObject currentChild;
    public GameObject currentParent;
    public Button buttonStart;
    public GameObject backPack;
    public GameObject obstacles;
    
    



    private void Awake()
    {
    
    }

    // Start is called before the first frame update
    void Start()
    {
        GetParent(MenuManager.menuManager.parentName);
        GetChildren(MenuManager.menuManager.childName);
        buttonStart.onClick.AddListener(OnClickPlay);
        ActiveObjects();
            
    }


    private void ActiveObjects()
    {
        if ((GameManager.gameManager.activitySelect.nameActivity == "Swing") || (GameManager.gameManager.activitySelect.nameActivity == "Swing Bonus"))
        {
            currentChild.GetComponent<PlayerController>().ActiveBackPack();
            currentParent.GetComponent<ParentController>().ActiveBackPack();
        }
        if (GameManager.gameManager.activitySelect.nameActivity == "Jump Forward")
        {
            obstacles.SetActive(true);

        }


    }

    public void OnClickPlay()
    {
        currentChild.GetComponent<PlayerController>().OnPlayAnim();//Start ANimations in Players
        buttonStart.interactable = false;
    }


    public void GetChildren(string name)
    {
        foreach (GameObject child in childrenAvatars)
        {
            if(child.name == name)
            {
                child.SetActive(true);
                currentChild = child;
                return;
               
            }
        }
        
    }
    public void GetParent(string name)
    {
        foreach (GameObject parent in parentAvatars)
        {
            if (parent.name == name)
            {
                parent.SetActive(true);
                currentParent = parent;
                return;
            }
        }

    }

     
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
