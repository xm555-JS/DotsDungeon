using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cCamControl : MonoBehaviour
{
    public GameObject player;
    private float posX;
    private float posY;

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
        posX = player.transform.position.x;
        posY = player.transform.position.y;

        posX = Mathf.Clamp(posX, -5.7f, 5.7f);
        posY = Mathf.Clamp(posY, -0.6f, 0f);

        transform.position = new Vector3(posX, posY, -10f);
    }
}
