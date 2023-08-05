using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    //Get dialogue script
    public Dialogue dialogue;

    //Booleans to tell the game if they can spawn
    private bool canSpawnFruitNPC = true;
    private bool canSpawnDairyNPC = true;
    private bool canSpawnGummyBearNPC = true;

    //NPCs
    public GameObject fruitNPC;
    public GameObject dairyNPC;
    public GameObject gummyBearNPC;

    //Spawners
    public GameObject fruitNPCSpawner;
    public GameObject dairyNPCSpawner;
    public GameObject gummyBearNPCSpawner;


    // Update is called once per frame
    void Update()
    {
        SpawnFruitNPC();
        SpawnDairyNPC();
        SpawnGummyBearNPC();
    }

    private void SpawnFruitNPC()
    {
        if (dialogue.fruitBossDefeated && canSpawnFruitNPC)
        {
            Instantiate(fruitNPC, fruitNPCSpawner.transform.position, fruitNPCSpawner.transform.rotation);
            canSpawnFruitNPC = false;
        }
    }

    private void SpawnDairyNPC()
    {
        if (dialogue.dairyBossDefeated && canSpawnDairyNPC)
        {
            Instantiate(dairyNPC, dairyNPCSpawner.transform.position, dairyNPCSpawner.transform.rotation);
            canSpawnDairyNPC = false;
        }
    }

    private void SpawnGummyBearNPC()
    {
        if (dialogue.fruitBossDefeated && dialogue.dairyBossDefeated && canSpawnGummyBearNPC)
        {
            Instantiate(gummyBearNPC, gummyBearNPCSpawner.transform.position, gummyBearNPCSpawner.transform.rotation);
            canSpawnGummyBearNPC = false;
        }
    }
}
