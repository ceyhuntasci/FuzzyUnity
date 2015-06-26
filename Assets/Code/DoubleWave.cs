﻿using UnityEngine;
using System.Collections;

public class DoubleWave : WaveBase
{
    public DoubleWave()
    {
        int laneObjCount = Random.Range(2, 4);
        Lane l = new Lane();
        l.Binary = Lane.GenerateRandomLane(laneObjCount);

        Block lElement = new Block();
        lElement.Prefab = "oneMesh";
        MaterialColor = Color.red;
        l.SetDefaultBlock(lElement);

        Speed = new Vector3(-7, 0.0f, 0.0f);
        SpawnPosition = new Vector3(15.16f, 2.86f, -2);
        l.Wave = this;

        laneObjCount = Random.Range(2, 4);
        Lane d = new Lane();
        d.Binary = Lane.GenerateRandomLane(laneObjCount);
        d.SetDefaultBlock(lElement);
        d.Wave = this;

        Name = "Double Wave";
        Type = WaveType.OaA;
        SpawnRate = 25;
        ForcedSpawnTime = 20;

        Lanes.Add(l);
        Lanes.Add(d);
    }
    public override void Initialize()
    {
        for (int i = 0; i < Lanes.Count; i++)
            Lanes[i].Initialize();
    }
    public override void Update()
    {
        for (int j = 0; j < Lanes.Count; j++)
        {
            Lane l = Lanes[j];

            l.Update();
        }
    }
}
