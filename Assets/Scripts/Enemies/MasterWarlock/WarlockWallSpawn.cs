using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class WarlockWallSpawn : BaseState
{
    private WarlockSM _sm;
    private Stopwatch sw;
    private int[][] spawnPatterns = new int [][] {
        new int [] { 0, 0, 0 },
        new int [] { 0, 1, 2 },
        new int [] { 2, 0, 2, 0 },
        new int [] { 1, 0, 1 }
    };
    private int [] selectedPattern;
    private int spawnCounter;

    public WarlockWallSpawn(WarlockSM stateMachine) : base("WarlockWallSpawn", stateMachine) {
        _sm = stateMachine;
        sw = new Stopwatch();
    }

    public override void Enter()
    {
        base.Enter();
        sw.Restart();
        selectedPattern = spawnPatterns[Random.Range(0, spawnPatterns.Length - 1)];
        spawnCounter = 0;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if( spawnCounter < selectedPattern.Length) {
            if(sw.ElapsedMilliseconds > 800 ) {
                _sm.SpawnWall(selectedPattern[spawnCounter]);
                spawnCounter++;
                sw.Restart();
            }
        } else {
            if(sw.ElapsedMilliseconds > 800 ) {
                stateMachine.ChangeState(_sm.retreatingState);
            }
        }
    }
}
