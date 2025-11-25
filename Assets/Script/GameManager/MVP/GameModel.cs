using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel
{
    public int CurrentTime { get; private set; }

    public void SetTime(int time)
    {
        CurrentTime = time;
    }

}
