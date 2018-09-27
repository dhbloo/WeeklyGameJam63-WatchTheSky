using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierGenerator : MonoBehaviour
{
    public GameObject Soldier;
    public GameObject Player;

    public int MaxCount = 30;
    public float GenerateCycleTime = 3;
    public float MaxRadius;
    public float NoGenInPlayerRadius;

    private float timeGen = 0.1f;


    private void Update()
    {
        if (timeGen == 0)
        {
            if (transform.childCount < MaxCount)
            {
                float rndAngle, rndRadius;
                Vector3 spawnPos = new Vector3();
                Vector3 playerpos = Player.transform.position;
                playerpos.y = 0;
                for (int i = 0; i < 10; i++)
                {
                    rndAngle = Random.value * Mathf.PI * 2;
                    rndRadius = Random.value * MaxRadius;
                    spawnPos = new Vector3(Mathf.Cos(rndAngle) * rndRadius, 0, Mathf.Sin(rndAngle) * rndRadius);

                    if (Vector3.Distance(spawnPos, playerpos) > NoGenInPlayerRadius)
                        break;
                }
                GameObject newSoldier = Instantiate(Soldier, transform);
                newSoldier.transform.localPosition = spawnPos;
                Vector3 forward = (playerpos - spawnPos).normalized;
                newSoldier.transform.forward = -forward;

                timeGen += Time.deltaTime;
            }
        }
        else
        {
            timeGen += Time.deltaTime;
            if (timeGen > GenerateCycleTime)
                timeGen = 0;
        }
    }
}
