using System;
using UnityEngine;
using System.Collections.Generic;

public class FunctionUpdater
{
    private class MonoBehaviourHook : MonoBehaviour
    {

        public Action OnUpdate;

        private void Update()
        {
            if (OnUpdate != null) OnUpdate();
        }

    }

    private static List<FunctionUpdater> updaterList;
    private static GameObject initGameObject;

    private static void InitIfNeeded()
    {
        if (initGameObject == null)
        {
            initGameObject = new GameObject("FunctionUpdater_Global");
            updaterList = new List<FunctionUpdater>();
        }
    }


    public static FunctionUpdater Create(Func<bool> updateFunc, string functionName)
    {
        return Create(updateFunc, functionName, true, false);
    }

    public static FunctionUpdater Create(Func<bool> updateFunc, string functionName, bool active, bool stopAllWithSameName)
    {
        InitIfNeeded();

        if (stopAllWithSameName)
        {
            StopAllUpdatersWithName(functionName);
        }

        GameObject gameObject = new GameObject("FunctionUpdater Object " + functionName, typeof(MonoBehaviourHook));
        FunctionUpdater functionUpdater = new FunctionUpdater(gameObject, updateFunc, functionName, active);
        gameObject.GetComponent<MonoBehaviourHook>().OnUpdate = functionUpdater.Update;

        updaterList.Add(functionUpdater);
        return functionUpdater;
    }

    private static void RemoveUpdater(FunctionUpdater funcUpdater)
    {
        InitIfNeeded();
        updaterList.Remove(funcUpdater);
    }

    public static void StopAllUpdatersWithName(string functionName)
    {
        InitIfNeeded();
        for (int i = 0; i < updaterList.Count; i++)
        {
            if (updaterList[i].functionName == functionName)
            {
                updaterList[i].DestroySelf();
                i--;
            }
        }
    }

    private GameObject gameObject;
    private string functionName;
    private bool active;
    private Func<bool> updateFunc;

    public FunctionUpdater(GameObject gameObject, Func<bool> updateFunc, string functionName, bool active)
    {
        this.gameObject = gameObject;
        this.updateFunc = updateFunc;
        this.functionName = functionName;
        this.active = active;
    }

    private void Update()
    {
        if (!active) return;
        if (updateFunc())
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        RemoveUpdater(this);
        if (gameObject != null)
        {
            UnityEngine.Object.Destroy(gameObject);
        }
    }

}