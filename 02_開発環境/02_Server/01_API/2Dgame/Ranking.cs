using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;


internal class Ranking
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
    public Ranking(long rank, int time)
    {
        Time = time;
        Rank = rank;
    }
    public Ranking() { }

    //public string GetStatus()
    //{
    //    return $"プレイヤーID:{User_id},スコア:{Time}";

    //}
}

