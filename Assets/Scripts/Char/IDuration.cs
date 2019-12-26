using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDuration 
{
    int Duration { get; }
    void TickDown();
    void IncreaseDuration(int toIncrease);
}
