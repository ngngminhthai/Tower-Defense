﻿using Assets.Scripts.Gameplay.Units;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


public class UnitSpawner : MonoBehaviour
{
    public const float WIDTH_MAP = 6;
    public const float HEIGHT_MAP = 6;
    public const float DELTA_TIME_PER_WAVE = 5;

    [SerializeField]
    GameObject prefabBanshee;
    [SerializeField]
    GameObject prefabHarpy;
    [SerializeField]
    GameObject prefabMinotaur;
    [SerializeField]
    GameObject prefabOrge;
    [SerializeField]
    TextMeshProUGUI countWave;
    public int StrengthPerWave { get; set; }

    public int CountWave { get; set; }

    public float AccumStrength { get; set; }
    public int TypeUnit { get; set; }

    public float max_x = 28;
    public float min_x = -28;
    public float min_y = -14;
    public float max_y = 14;
    GameObject[] attackers;
    private float StrengthPerUnit;
    private GameObject Instance;
    private System.Random rand = new System.Random();
    private bool isEndWave = false;
    private int level = 1;
    Timer timer;
    Unit unit;

    void Start()
    {
        CountWave = 1;
        countWave.text = "Wave: " +CountWave;
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = DELTA_TIME_PER_WAVE;
        StrengthPerWave = 80;
        SpawnNewWave();
    }

    // Update is called once per frame
    // Khi kết thúc một wave, tăng số wave, tăng tổng sức mạnh->sinh Unit mỗi delta giây 
    void Update()
    {
        if (IsFinishWave())
        {
            StartCoroutine(StartNewWave(5f));
        }
    }

    bool IsFinishWave()
    {
        attackers = GameObject.FindGameObjectsWithTag("attackers");
        if (attackers.Length > 0) return false;
        else
        {
            return true;
        }
    }

    void SpawnNewWave()
    {
        AccumStrength = 0;
        while (true)
        {
            TypeUnit = UnityEngine.Random.Range(1, 5);
            switch (TypeUnit)
            {
                case 1:
                    GameObject Instance = GenerateUnit(prefabHarpy);
                    Harpy harpy = Instance.GetComponent<Harpy>();
                    harpy.level = level;
                    harpy.Start();
                    StrengthPerUnit = (harpy.Damage + harpy.HitPoints / 2) + (1 + harpy.Speed / 3);
                    break;
                case 2:
                    GameObject Instance1 = GenerateUnit(prefabBanshee);
                    Banshee banshee = Instance1.GetComponent<Banshee>();
                    banshee.level = level;
                    banshee.Start();
                    StrengthPerUnit = (banshee.Damage + banshee.HitPoints / 2) + (1 + banshee.Speed / 3);
                    break;
                case 3:
                    GameObject Instance2 = GenerateUnit(prefabMinotaur);
                    Monitor minotaur = Instance2.GetComponent<Monitor>();
                    minotaur.level = level;
                    minotaur.Start();
                    StrengthPerUnit = (minotaur.Damage + minotaur.HitPoints / 2) + (1 + minotaur.Speed / 3);
                    break;
                case 4:
                    GameObject Instance3 = GenerateUnit(prefabOrge);
                    Ogre ogre = Instance3.GetComponent<Ogre>();
                    ogre.level = level;
                    ogre.Start();
                    StrengthPerUnit = (ogre.Damage + ogre.HitPoints / 2) + (1 + ogre.Speed / 3) + 5;
                    //Debug.Log(ogre.Damage);
                    break;
                default: break;
            }
            AccumStrength += StrengthPerUnit;
            //break;
            if (AccumStrength > StrengthPerWave) break;
        }
    }

    public void IncreaseStrength()
    {
        Harpy harpy = Instance.GetComponent<Harpy>();
        harpy.LevelUp();
        Banshee banshee = prefabBanshee.GetComponent<Banshee>();
        banshee.LevelUp();
        Ogre orge = prefabOrge.GetComponent<Ogre>();
        orge.LevelUp();
        Monitor mage = prefabMinotaur.GetComponent<Monitor>();
        mage.LevelUp();
    }
    public GameObject GenerateUnit(GameObject prefab)
    {
        float randomX, randomY;
        randomX = (float)(rand.NextDouble() * 52f - 26f);
        if (randomX > 26f || randomX < -26f)
        {
            randomY = (float)(rand.NextDouble() * 24f - 12f);
        }
        else
        {
            if (rand.Next(2) == 0)
            {
                randomY = (float)(rand.NextDouble() * (14f - 12f) + 12f);
            }
            else
            {
                randomY = (float)(rand.NextDouble() * (-12f + 14f) - 12f);
            }
        }
        Vector2 position = new Vector2(randomX, randomY);
        return Instantiate<GameObject>(prefab, position, Quaternion.identity);
    }
    IEnumerator StartNewWave(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Do something to end the game
        if (IsFinishWave())
        {
            SpawnNewWave();
            level += 1;
            CountWave += 1;
            countWave.text = "Wave: " + CountWave;
            StrengthPerWave = StrengthPerWave + 10 * (CountWave + 1);
            //             StrengthPerWave = StrengthPerWave + 10 * (CountWave + 2);
        }
        isEndWave = true;
    }
}