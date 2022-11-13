using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public CinemachineVirtualCamera defaultCamera;
    public CinemachineVirtualCamera opponentFollowCamera;
    public CinemachineVirtualCamera playerFollowCamera;
    public CinemachineVirtualCamera contactPointCamera;

    public List<CinemachineVirtualCamera> randomChangingCameras = new List<CinemachineVirtualCamera>();
    public float changeCameraEvery;
    public float changingDuration;
    public float startsAfter;

    private void Awake()
    {
        StartCoroutine(ChangeCameraCoroutine());
    }

    public void SetDefaultCamera()
    {
        ChangeCamera(defaultCamera);
    }

    public void SetOpponentFollowCamera()
    {
        ChangeCamera(opponentFollowCamera);
    }

    public void SetPlayerFollowCamera()
    {
        ChangeCamera(playerFollowCamera);
    }

    public void SetContactPointCamera()
    {
        ChangeCamera(contactPointCamera);
    }

    private void ChangeCamera(CinemachineVirtualCamera camera)
    {
        ResetPriorities();

        camera.Priority = 1;
    }

    public void SetOpponentToFollow(GameObject obj)
    {
        opponentFollowCamera.Follow = obj.transform;
    }

    private void ResetPriorities()
    {
        defaultCamera.Priority = 0;
        opponentFollowCamera.Priority = 0;
        playerFollowCamera.Priority = 0;
        contactPointCamera.Priority = 0;
    }

    IEnumerator ChangeCameraCoroutine()
    {
        yield return new WaitForSeconds(startsAfter);

        while (true)
        {
            yield return new WaitForSeconds(changeCameraEvery);

            var random = new System.Random();

            CinemachineVirtualCamera nextCamera = randomChangingCameras
                .OrderBy(x => random.Next())
                .First();

            ChangeCamera(nextCamera);

            yield return new WaitForSeconds(changingDuration);

            SetDefaultCamera();
        }
    }
}
