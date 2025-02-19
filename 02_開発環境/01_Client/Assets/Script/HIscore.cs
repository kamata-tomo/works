using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIscore
{
    public long Rank;
    public int stage_id;
    public int Time;

    //public Ranking(long rank, int user_id, int time)
    //{
    //    User_id = user_id;
    //    Time = time;
    //    Rank = rank;
    //}
    public HIscore(int time)
    {
        stage_id = GameManager.StageID;
        Time = time;
    }
    public HIscore() { }

    public string GetRankText()
    {
        return $"{this.Time / 10000}:{(this.Time % 10000) / 100}:{this.Time % 100}";
    }
}
