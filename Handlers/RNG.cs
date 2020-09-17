using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChanceResult {
    CRIT_FAIL, FAIL, SUCCESS, CRIT_SUCCESS
}

public class RNG : Handler
{
    public static RNG I;

    protected List<int> currentResultList = new List<int>();
    protected int previousResult = 0;
    protected int successes;
    protected int failures;

    public override void Awake()
    {
        base.Awake();
        if (!EnforceSingleton(RNG.I))
            return;
        RNG.I = this;
        NewResultList();
    }

    public ChanceResult ChanceCheck(int difficulty) {
        HandleCurrentResultListRefresh();
        ChanceResult result = GetChanceResult(currentResultList[0], difficulty);
        currentResultList.RemoveAt(0);
        if (result == ChanceResult.CRIT_FAIL || result == ChanceResult.FAIL)
            failures++;
        if (result == ChanceResult.CRIT_SUCCESS || result == ChanceResult.SUCCESS)
            successes++;
        return result;
    }

    protected void HandleCurrentResultListRefresh() {
        if (currentResultList.Count == 0)
            NewResultList();
    }

    public ChanceResult ChanceCheckSimple(int difficulty)
    {
        ChanceResult result = ChanceCheck(difficulty);
        if (result == ChanceResult.CRIT_FAIL)
            return ChanceResult.FAIL;
        if (result == ChanceResult.CRIT_SUCCESS)
            return ChanceResult.SUCCESS;
        return result;
    }

    protected ChanceResult GetChanceResult(int dieRoll, int difficulty) {
        previousResult = dieRoll;
        if (dieRoll > difficulty)
        {
            if (dieRoll >= 95)
                return ChanceResult.CRIT_FAIL;
            return ChanceResult.FAIL;
        }
        if (dieRoll <= 5)
            return ChanceResult.CRIT_SUCCESS;
        return ChanceResult.SUCCESS;
    }

    public int GetPreviousResult() {
        return previousResult;
    }

    public void NewResultList() {
        int nextListSize = Random.Range(10,100);
        float increment = 100f / (float)nextListSize;
        int mod = GetNextResultListMod();
        for (int i = 0; i < nextListSize; i++)
        {
            currentResultList.Add(Mathf.Clamp(Mathf.RoundToInt((float)i* (float)increment)+mod, 0, 100));
        }
        currentResultList.Sort((x, y) => Random.Range(0, 3).CompareTo(1));
    }

    protected int GetNextResultListMod() {
        int mod = (-successes + failures);
        failures = 0;
        successes = 0;
        return mod;
    }

}
