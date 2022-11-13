using Cinemachine;
using UnityEditor;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public CinemachineVirtualCamera defaultCamera;
    public CinemachineVirtualCamera opponentFollowCamera;
    public CinemachineVirtualCamera playerFollowCamera;
    public CinemachineVirtualCamera contactPointCamera;


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
}
