using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    [Header("----- Modules -----")]
    [SerializeField] private Module _topModule;
    [SerializeField] private Module _leftModule;
    [SerializeField] private Module _rightModule;
    [SerializeField] private Module _downModule;

    private Wall _topWall { get; set; }
    private Wall _leftWall { get; set; }
    private Wall _rightWall { get; set; }
    private Wall _downWall { get; set; }

    public bool IsDeathModule { get; set; }

    
    private void Start()
    {
        // PlayerManager.Instance.PlayerHasSwipe += CheckDeathModule;
    }

    public Module GetModuleNeighbor(Directions dir)
    {
        if (dir == Directions.Right && _rightModule != null && _rightWall == null)
            return _rightModule;
        if (dir == Directions.Left && _leftModule != null && _leftWall == null)
            return _leftModule;
        if (dir == Directions.Top && _topModule != null && _topWall == null)
            return _topModule;
        if (dir == Directions.Down && _downModule != null && _downWall == null)
            return _downModule;

        return null;
    }

    public void AddWall(Wall top, Wall left, Wall right, Wall down)
    {
        _topWall = top;
        _leftWall = left;
        _rightWall = right;
        _downWall = down;
    }

    // private void CheckDeathModule()
    // {
    //     
    // }

    private void OnDisable()
    {
        // PlayerManager.Instance.PlayerHasSwipe -= CheckDeathModule;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var pos = gameObject.transform.position;
        if (_topModule != null)
            Gizmos.DrawLine(pos, _topModule.gameObject.transform.position);
        Gizmos.color = Color.yellow;
        if (_leftModule != null)
            Gizmos.DrawLine(pos, _leftModule.gameObject.transform.position);
        Gizmos.color = Color.green;
        if (_downModule != null)
            Gizmos.DrawLine(pos, _downModule.gameObject.transform.position);
        Gizmos.color = Color.blue;
        if (_rightModule != null)
            Gizmos.DrawLine(pos, _rightModule.gameObject.transform.position);
    }
}