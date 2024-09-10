using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    Biome biome;
    public GameObject ball;
    public GameObject platform;
    float distance;
    Vector3 lastPosition;

    List<GameObject> platformList = new List<GameObject>();

    void Start()
    {
        biome = GetComponent<Biome>();

        distance = 30f;   

        ResetVariables();
    }

    void Update()
    {
        CheckPlatforms();

        if(Vector3.Distance(ball.transform.position, lastPosition) < distance)
        {
            SpawnPlatform();
        }
    }

    Vector3 CreatePlatformPosition()
    {
        if (platformList.Count == 0) return new Vector3(0, 0, 0);
        if (platformList.Count == 1) return new Vector3(1, 0, 0);
        if (platformList.Count == 2) return new Vector3(2, 0, 0);
        if (platformList.Count == 3) return new Vector3(3, 0, 0);
        if (platformList.Count == 4) return new Vector3(4, 0, 0);

        int sayi = Random.Range(0, 2);

        if (sayi == 0) return new Vector3(lastPosition.x + 1, lastPosition.y, lastPosition.z);
        else return new Vector3(lastPosition.x, lastPosition.y, lastPosition.z + 1);
    }

    void SpawnPlatform()
    {
        Vector3 pos = CreatePlatformPosition();

        GameObject myObject = Instantiate(platform, pos, Quaternion.identity);

        myObject.GetComponent<MeshRenderer>().material.color = biome.GetCurrentBiomeColor();

        if(platformList.Count >= 4)
        {
            int number = Random.Range(0, 40);

            if (number > 35)
            {
                myObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (number == 35)
            {
                myObject.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        

        lastPosition = pos;
        platformList.Add(myObject);
    }

    void CheckPlatforms()
    {
        if (platformList.Count <= 0) return;

        if (Vector3.Distance(platformList[0].transform.position, ball.transform.position) > 2)
        {
            GameObject oldPlatform = platformList[0];
            platformList.RemoveAt(0);

            oldPlatform.AddComponent<Rigidbody>();
            Destroy(oldPlatform, 1f);
        }
    }

    void ClearPlatformList()
    {
        for (int i = 0; i < platformList.Count; i++)
        {
            Destroy(platformList[i]);
        }

        platformList.Clear();
    }

    void SpawnFirstPlatforms()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnPlatform();
        }
    }

    public void ChangeColorOfAllPlatforms(Color color)
    {
        for (int i = 0; i < platformList.Count; i++)
        {
            platformList[i].GetComponent<MeshRenderer>().material.color = color;
        }
    }

    public void ResetVariables()
    {       
        ball.transform.position = new Vector3(0, 0.75f, 0);
        ball.SetActive(true);

        lastPosition = new Vector3(2, 0, 0);

        ClearPlatformList();
        SpawnFirstPlatforms();
    }

    
}
