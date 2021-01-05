using UnityEngine;

public class ParentController : MonoBehaviour
{

    public Animator parentAnim;
    public GameObject backPack;
    public GameObject obstacle;


    private void Start()
    {

    }

    private void Update()
    {
        var activityParent = GameManager.gameManager.activitySelect;


        if (activityParent.isFinish)
        {
            parentAnim.SetBool("isFinish", true);
            return;
        }

        if (activityParent.isPlaying)
        {
            parentAnim.SetBool(activityParent.nameAnim, true);

        }
        if (!activityParent.isPlaying)
        {
            parentAnim.SetBool(activityParent.nameAnim, false);

        }




    }

    public void ActiveBackPack()
    {
        if (!backPack.activeInHierarchy)
        {
            backPack.SetActive(true);
        }
    }

    public void ActiveObstacle()
    {
        if (!obstacle.activeInHierarchy)
        {
            obstacle.SetActive(true);
        }
    }


    public void OnParentPlayAnim()
    {
        return;

    }
    public void IncreseParentAnim()
    {

    }





    public void CheckParentTime()
    {

    }
}


