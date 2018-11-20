using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class InfoHighScore
{
    public int beatz;
    public int miss;
    public int hit;
    public int good;
    public int perfect;
    public float percent;

    public InfoHighScore(int beatz, int miss,int hit,int good,int perfect, float percent) 
    {
        this.beatz = beatz;
        this.miss = miss;
        this.good = good;
        this.hit = hit;
        this.perfect = perfect;
        this.percent = percent;
    }
}

