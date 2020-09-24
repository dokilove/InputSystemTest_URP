using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpdateableObject
{
    void OnUpdate(float dt);
}

public interface IFixedUpdateableObject
{
    void OnFixedUpdate(float dt);
}

public interface ILateUpdateableObject
{
    void OnLateUpdate(float dt);
}


public class GameLogic : SingletonAsComponent<GameLogic>
{
    public static GameLogic Instance {
        get { return ((GameLogic)_Instance); }
        set { _Instance = value; }
    }

    List<IUpdateableObject> _updateableObjects = new List<IUpdateableObject>();
    List<IFixedUpdateableObject> _fixedUpdateableObjects = new List<IFixedUpdateableObject>();
    List<ILateUpdateableObject> _lateUpdateableObjects = new List<ILateUpdateableObject>();

    #region Update
    public void RegisterUpdateableObject(IUpdateableObject obj)
    {
        if (!_updateableObjects.Contains(obj))
        {
            _updateableObjects.Add(obj);
        }
    }

    public void DeregisterUpdateableObject(IUpdateableObject obj)
    {
        if (_updateableObjects.Contains(obj))
        {
            _updateableObjects.Remove(obj);
        }
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        for (int i =0; i < _updateableObjects.Count; ++i)
        {
            _updateableObjects[i].OnUpdate(dt);
        }
    }
    #endregion

    #region FixedUpdate
    public void RegisterFixedUpdateableObject(IFixedUpdateableObject obj)
    {
        if (!_fixedUpdateableObjects.Contains(obj))
        {
            _fixedUpdateableObjects.Add(obj);
        }
    }

    public void DeregisterFixedUpdateableObject(IFixedUpdateableObject obj)
    {
        if (_fixedUpdateableObjects.Contains(obj))
        {
            _fixedUpdateableObjects.Remove(obj);
        }
    }

    private void FixedUpdate()
    {
        float dt = Time.deltaTime;
        for (int i = 0; i < _fixedUpdateableObjects.Count; ++i)
        {
            _fixedUpdateableObjects[i].OnFixedUpdate(dt);
        }
    }
    #endregion

    #region LateUpdate
    public void RegisterLateUpdateableObject(ILateUpdateableObject obj)
    {
        if (!_lateUpdateableObjects.Contains(obj))
        {
            _lateUpdateableObjects.Add(obj);
        }
    }

    public void DeregisterLateUpdateableObject(ILateUpdateableObject obj)
    {
        if (_lateUpdateableObjects.Contains(obj))
        {
            _lateUpdateableObjects.Remove(obj);
        }
    }

    private void LateUpdate()
    {     
        float dt = Time.deltaTime;
        for (int i = 0; i < _lateUpdateableObjects.Count; ++i)
        {
            _lateUpdateableObjects[i].OnLateUpdate(dt);
        }
    }
    #endregion
}
