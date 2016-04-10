using UnityEngine;
using System.Collections;

public class ChipStacks : MonoBehaviour {

    public GameObject AllChips;

    public GameObject chip_1;
    public GameObject chip_10;
    public GameObject chip_100;
    public GameObject chip_1000;

    public GameObject playerChipsSpawnPoint;
    public GameObject computerChipsSpawnPoint;
    public GameObject thePot;

    public void SetTableChips(int playerChips, int computerChips,int thepotcount)
    {
        foreach (Transform child in AllChips.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        PlaceChips(playerChipsSpawnPoint, playerChips);
        PlaceChips(computerChipsSpawnPoint, computerChips);
        PlaceChips(thePot, thepotcount);
    }

    Vector3 shiftSpawnPositionX(Vector3 pos)
    {
        return new Vector3(pos.x + 0.13f, pos.y,pos.z);
    }
	
    void PlaceChips(GameObject spawnPoint, int amount)
    {
        int multiple;

        multiple = amount / 1000;
        amount -= multiple * 1000;

        Vector3 spawnPosition = spawnPoint.transform.position;
        GameObject temp;

        if (multiple > 0)
        {
            for (int i = 0; i < multiple; i++)
            {
                temp = Instantiate(chip_1000, spawnPosition, Quaternion.identity) as GameObject;
                temp.transform.SetParent(AllChips.transform);

                if (i % 10 == 0 && i != 0)
                {
                    spawnPosition = shiftSpawnPositionX(spawnPosition);
                }
            }
        }

        spawnPosition = shiftSpawnPositionX(spawnPosition);

        multiple = amount / 100;
        amount -= multiple * 100;

        if (multiple > 0)
        {
            for (int i = 0; i < multiple; i++)
            {
                temp = Instantiate(chip_100, spawnPosition, Quaternion.identity) as GameObject;
                temp.transform.SetParent(AllChips.transform);
            }
        }

        spawnPosition = shiftSpawnPositionX(spawnPosition);

        multiple = amount / 10;
        amount -= multiple * 10;

        if (multiple > 0)
        {
            for (int i = 0; i < multiple; i++)
            {
                temp = Instantiate(chip_10, spawnPosition, Quaternion.identity) as GameObject;
                temp.transform.SetParent(AllChips.transform);
            }
        }

        spawnPosition = shiftSpawnPositionX(spawnPosition);

        multiple = amount / 1;

        if (multiple > 0)
        {
            for (int i = 0; i < multiple; i++)
            {
                temp = Instantiate(chip_1, spawnPosition, Quaternion.identity) as GameObject;
                temp.transform.SetParent(AllChips.transform);
            }
        }
    }
}
