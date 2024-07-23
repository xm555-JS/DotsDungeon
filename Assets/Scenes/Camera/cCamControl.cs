using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCamControl : MonoBehaviour
{
    public GameObject player;

    private GameObject target;
    private GameObject preTarget;

    private float posX;
    private float posY;
    static float defaultPosZ = -10f;

    private float duration = 3f;
    private float time = 0f;

    private bool isFinish = false;

    void LateUpdate()
    {
        TargetCamera();
    }

    void TargetCamera()
    {
        if (!target)
        {
            Debug.Log("cCamControl - target이 없습니다.");
            return;
        }

        // 이전 타겟과 현재 타겟이 다르면 엑션 카메라 실행
        if (preTarget != target)
            ActionCamera();
        else
        {
            posX = target.transform.position.x;
            posY = target.transform.position.y;

            CameraMoving(posX, posY);
        }
    }

    void ActionCamera()
    {
        // 타겟이 없을 땐 기본적으로 플레이어 카메라로 설정
        if (!preTarget)
        {
            FocusOnPlayer();
            return;
        }

        // duration만큼 target을 향해 카메라 이동
        if (time <= duration)
        {
            float posX = Mathf.Lerp(preTarget.transform.position.x, target.transform.position.x, time / duration);
            float posY = Mathf.Lerp(preTarget.transform.position.y, target.transform.position.y, time / duration);
            time += Time.deltaTime;

            CameraMoving(posX, posY);
        }
        else
        {
            isFinish = true;
            preTarget = target;
            time = 0f;
        }
    }

    void CameraMoving(float posX, float posY)
    {
        posX = Mathf.Clamp(posX, -6.9f, 6.65f);
        posY = Mathf.Clamp(posY, -0.6f, 0f);

        this.transform.position = new Vector3(posX, posY, defaultPosZ);
    }

    public void TargetSetting(GameObject changeTarget)
    {
        preTarget = target;
        target = changeTarget;
    }

    public void FocusOnPlayer()
    {
        preTarget = target;
        target = player;
    }




    //void TargetSetting()
    //{
    //    // 임시 변수 게임매니저 클래스에서 해당 스테이지가 몇 인지 알려주는 변수를 만들거임
    //    // 이 변수 awake에서 항상 0으로 초기화 해줄것
    //    int bossStageIndex = 0;

    //    bool checkBossStage = false;
    //    if (bossStageIndex != 0)
    //        checkBossStage = true;

    //    if (checkBossStage)
    //    {
    //        TargetCamera(targets[bossStageIndex - 1]);
    //    }
    //}

    //public void TargetCameraTest(GameObject target)
    //{
    //    if (target != preTarget)
    //    Debug.Log(target);
    //    // 플레이어 클래스에서 카메라를 지정하고 있는데 만약에 다른 클래스에서 카메라 타겟을 바꾼다면
    //    preTarget = target;
    //}
}
