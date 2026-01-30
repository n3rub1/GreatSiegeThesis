using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOptionManager : MonoBehaviour
{
    public static StartOptionManager Instance;

    //public bool isStartIDN;

    private bool isPreTextPostIDNPost = false;
    private bool isPreIDNPostTextPost = false;
    private bool isTextPostIDNPost = false;
    private bool isIDNPostTextPost = false;

    private bool isPreTextPostIDNPost_MidPostReady = false;
    private bool isPreIDNPostTextPost_MidPostReady = false;
    private bool isTextPostIDNPost_MidPostReady = false;
    private bool isIDNPostTextPost_MidPostReady = false;

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

    //public void setIsStartIDN(bool setIsIDN)
    //{
    //    isStartIDN = setIsIDN;
    //}

    //public bool getIsStartIDN()
    //{
    //    return isStartIDN;
    //}


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

    public void SetisPreTextPostIDNPost_MidPostReady(bool setValue)
    {
        isPreTextPostIDNPost_MidPostReady = setValue;
    }

    public bool GetisPreTextPostIDNPost_MidPostReady()
    {
        return isPreTextPostIDNPost_MidPostReady;
    }

    public void SetisPreIDNPostTextPost_MidPostReady(bool setValue)
    {
        isPreIDNPostTextPost_MidPostReady = setValue;
    }

    public bool GetisPreIDNPostTextPost_MidPostReady()
    {
        return isPreIDNPostTextPost_MidPostReady;
    }

    public void SetisTextPostIDNPost_MidPostReady(bool setValue)
    {
        isTextPostIDNPost_MidPostReady = setValue;
    }

    public bool GetisTextPostIDNPost_MidPostReady()
    {
        return isTextPostIDNPost_MidPostReady;
    }

    public void SetisIDNPostTextPost_MidPostReady(bool setValue)
    {
        isIDNPostTextPost_MidPostReady = setValue;
    }

    public bool GetisIDNPostTextPost_MidPostReady()
    {
        return isIDNPostTextPost_MidPostReady;
    }







}
