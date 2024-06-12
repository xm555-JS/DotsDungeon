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

    private float duration = 3f;
    private float time = 0f;

    private bool isFinish = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        TargetCamera();
    }

    void TargetCamera()
    {
        if (!target)
            return;

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
        // ������ ���۵� �� target�� null -> player�� �Ǹ鼭 ����� ī�޶� ������ ����.
        if (!preTarget)
        {
            FocusOnPlayer();
            return;
        }

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
        posX = Mathf.Clamp(posX, -5.7f, 5.7f);
        posY = Mathf.Clamp(posY, -0.6f, 0f);

        this.transform.position = new Vector3(posX, posY, -10f);
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
    //    // �ӽ� ���� ���ӸŴ��� Ŭ�������� �ش� ���������� �� ���� �˷��ִ� ������ �������
    //    // �� ���� awake���� �׻� 0���� �ʱ�ȭ ���ٰ�
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
    //    // �÷��̾� Ŭ�������� ī�޶� �����ϰ� �ִµ� ���࿡ �ٸ� Ŭ�������� ī�޶� Ÿ���� �ٲ۴ٸ�
    //    preTarget = target;
    //}
}
