using Cinemachine;
using System.Collections;
using UnityEngine;

public class IntroManager : Singleton<IntroManager>
{
    private bool isSpace;
    private bool isLoading = true;

    public CinemachineVirtualCamera vanCamera;
    public CinemachineVirtualCamera shutterCamera;

    void Awake()
    {
        InitUI();
    }

    void FixedUpdate()
    {
        if (!isSpace && !isLoading && Input.GetKeyDown(KeyCode.Space))
        {           
            StartSpace();
        }
    }

    public void InitUI()
    {
        UIManager.Instance.shutter.SetActive(false);
        UIManager.Instance.spaceToStart.SetActive(true);

        UIManager.Instance.ingamePlayerUI.SetActive(false);

        UIManager.Instance.buttonUI.SetActive(false);
        UIManager.Instance.settingUI.SetActive(false);

        UIManager.Instance.underBarStop.SetActive(false);
        UIManager.Instance.underBarCancle.SetActive(false);

//        UIManager.Instance.loadingUI.SetActive(true);

        StartCoroutine("IntroSetting");
    }

    public void StartSpace()
    {
        isSpace = true;
        SoundManager.Instance.StartPlay();

        UIManager.Instance.shutterAnim.SetTrigger("ShutterOn");

        shutterCamera.Priority = 9;

        StartCoroutine("ShutterOut");
        
        UIManager.Instance.spaceToStart.SetActive(false);

        UIManager.Instance.ingamePlayerUI.SetActive(true);

        UIManager.Instance.buttonUI.SetActive(true);
        // UIManager.Instance.UnderBarStop.SetActive(true);

        UIManager.Instance.loadingUI.SetActive(false);

    }

    IEnumerator ShutterOut()
    {
        yield return new WaitForSeconds(2f);

        UIManager.Instance.shutter.SetActive(false);
    }

    IEnumerator IntroSetting()
    {
        yield return new WaitForSeconds(18f);

        UIManager.Instance.shutter.SetActive(true);  
        isLoading = false;
    }
}