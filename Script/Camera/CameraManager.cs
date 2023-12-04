using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public static CameraManager instance;

    [SerializeField] private CinemachineVirtualCamera[] listVirtualCamera;

    [SerializeField] private float fallPanAmount = .25f;
    [SerializeField] private float fallPanTime = .35f;

    private float normalYPanAmount;
    public float fallSpeedDampingThreshold = -15.0f;

    public bool IsLerpingYDamping {  get; set; }

    public bool LerpedFromPlayerFalling { get; set; }

    private Coroutine lerpYPanCoroutine;

    private CinemachineFramingTransposer framingTransposer;
    private CinemachineVirtualCamera currentCamera;


    private void Awake()
    {
        instance = this;

        for (int i = 0; i < listVirtualCamera.Length; i++)
        {
            if (listVirtualCamera[i].enabled)
            {
                currentCamera = listVirtualCamera[i];
                framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }

        normalYPanAmount = framingTransposer.m_YDamping;
    }

    private IEnumerator LerpYAction(bool isFalling)
    {
        IsLerpingYDamping = true;

        float startDampAmount = framingTransposer.m_YDamping;
        float endDampAmount = 0f;

        if (isFalling)
        {
            endDampAmount = fallPanAmount;
            LerpedFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = normalYPanAmount;
        }

        float elapsedTime = 0f;
        while (elapsedTime < fallPanTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime / fallPanTime));
            framingTransposer.m_YDamping = lerpedPanAmount;

            yield return null;

        }

        IsLerpingYDamping = false;
    }

    public void LerpYDamping(bool isFalling)
    {
        lerpYPanCoroutine = StartCoroutine(LerpYAction(isFalling));

    }

}
