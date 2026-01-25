using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOptionManager : MonoBehaviour
{
    public static StartOptionManager Instance;

    public bool isStartIDN;

    public bool isPreTextPostIDNPost;
    public bool isPreIDNPostTextPost;
    public bool isTextPostIDNPost;
    public bool isIDNPostTextPost;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void setIsStartIDN(bool setIsIDN)
    {
        isStartIDN = setIsIDN;
    }

    public bool getIsStartIDN()
    {
        return isStartIDN;
    }


    public void SetIsPreTextPostIDNPost(bool setValue)
    {
        isPreTextPostIDNPost = setValue;
    }

    public bool GetIsPreTextPostIDNPost()
    {
        return isPreTextPostIDNPost;
    }

    public void SetIsPreIDNPostTextPost(bool setValue)
    {
        isPreIDNPostTextPost = setValue;
    }

    public bool GetIsPreIDNPostTextPost()
    {
        return isPreIDNPostTextPost;
    }

    public void SetIsTextPostIDNPost(bool setValue)
    {
        isTextPostIDNPost = setValue;
    }

    public bool GetIsTextPostIDNPost()
    {
        return isTextPostIDNPost;
    }

    public void SetIsIDNPostTextPost(bool setValue)
    {
        isIDNPostTextPost = setValue;
    }

    public bool GetIsIDNPostTextPost()
    {
        return isIDNPostTextPost;
    }

}
