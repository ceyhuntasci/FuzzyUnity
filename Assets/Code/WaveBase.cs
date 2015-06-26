﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveBase
{
    public const int LANE_COUNT = 5;
    public enum WaveType
    {
        Normal,
        OaA,
        Oa2A
    }

    private WaveType m_Type;

    public WaveType Type
    {
        get { return m_Type; }
        set { m_Type = value; }
    }

    private Vector3 m_vSpeed;

    public Vector3 Speed
    {
        get { return m_vSpeed; }
        set { m_vSpeed = value; }
    }

    private List<Lane> m_cLanes;

    private List<GameObject> m_Prefabs;

    public List<GameObject> Prefabs
    {
        get { return m_Prefabs; }
        set { m_Prefabs = value; }
    }

    private Color m_cBlockColor;

    public Color MaterialColor
    {
        get { return m_cBlockColor; }
        set { m_cBlockColor = value; }
    }

    private List<int> m_DesiredPrefabIndices;

    public List<int> DesiredPrefabIndices
    {
        get { return m_DesiredPrefabIndices; }
        set { m_DesiredPrefabIndices = value; }
    }

    private Vector3 m_vSpawnPos;

    public Vector3 SpawnPosition
    {
        get { return m_vSpawnPos; }
        set { m_vSpawnPos = value; }
    }


    public List<Lane> Lanes
    {
        get { return m_cLanes; }
        set { m_cLanes = value; }
    }

    private string m_sName;

    public string Name
    {
        get { return m_sName; }
        set { m_sName = value; }
    }
    private int m_iSpawnRate;

    public int SpawnRate
    {
        get { return m_iSpawnRate; }
        set { m_iSpawnRate = value; }
    }

    private int m_iForcedSpawnTime;

    public int ForcedSpawnTime
    {
        get { return m_iForcedSpawnTime; }
        set { m_iForcedSpawnTime = value; }
    }

    public WaveBase()
    {
        m_vSpawnPos = Vector3.zero;
        m_vSpeed = Vector3.zero;
        Lanes = new List<Lane>();
        m_cLanes = new List<Lane>();
        m_Type = WaveType.Normal;
        m_Prefabs = new List<GameObject>();
        m_sName = "Wave Unknown";
        SpawnPosition = new Vector3(15.16f, 2, -2);

        for (int i = 0; i < LANE_COUNT; i++)
            m_Prefabs.Add(null);

        m_DesiredPrefabIndices = new List<int>();
        m_DesiredPrefabIndices = InitializeListEmpty(m_DesiredPrefabIndices);

    }
    public List<int> InitializeListEmpty(List<int> l)
    {
        for (int i = 0; i < LANE_COUNT; i++)
            l.Add(0);
        return l;
    }
    public virtual void Initialize()
    {

    }
    public virtual void Update()
    {

    }
}
