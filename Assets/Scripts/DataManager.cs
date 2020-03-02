﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{   
    public static DataManager Instance { get; private set; }
    public List<List<List<int>>> infoMono = new List<List<List<int>>>();  
    public int[] size = new int[2];
    public void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
