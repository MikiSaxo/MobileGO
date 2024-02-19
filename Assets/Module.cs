using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    [SerializeField] private Module _topModule;
    [SerializeField] private Module _leftModule;
    [SerializeField] private Module _rightModule;
    [SerializeField] private Module _downModule;


    public Module GetModuleNeighbor(Directions dir)
    {
        if (dir == Directions.Right && _rightModule != null)
            return _rightModule;
        if (dir == Directions.Left && _leftModule != null)
            return _leftModule;
        if (dir == Directions.Top && _topModule != null)
            return _topModule;
        if (dir == Directions.Down && _downModule != null)
            return _downModule;

        return null;
    }


    private void OnDrawGizmos()
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