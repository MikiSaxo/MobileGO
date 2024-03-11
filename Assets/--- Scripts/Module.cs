using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    [field:SerializeField] public bool IsBlockedModule { get; set; }
    
    [Header("----- Modules -----")]
    [SerializeField] protected Module _topModule;
    [SerializeField] protected Module _leftModule;
    [SerializeField] protected Module _rightModule;
    [SerializeField] protected Module _downModule;

    private Wall _topWall { get; set; }
    private Wall _leftWall { get; set; }
    private Wall _rightWall { get; set; }
    private Wall _downWall { get; set; }

    public bool IsDeathModule { get; set; }

    
    public bool IsTreated { get; set; }

    private ModuleFeature _moduleFeature;

    private void Start()
    {
        if(GetComponent<ModuleFeature>() != null)
            _moduleFeature = GetComponent<ModuleFeature>();
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
        if (top != null)
            _topWall = top;
        if (left != null)
            _leftWall = left;
        if (right != null)
            _rightWall = right;
        if (down != null)
            _downWall = down;
    }

    public void ResetWall()
    {
        _topWall = null;
        _leftWall = null;
        _rightWall = null;
        _downWall = null;
    }

    public void Magnet(Vector2 imgSize)
    {
        if (_topModule != null && !_topModule.IsTreated)
        {
            _topModule.IsTreated = true;
            var pos = gameObject.transform.localPosition;
            _topModule.gameObject.transform.localPosition = new Vector3(pos.x, pos.y + imgSize.y, 0);
            _topModule.Magnet(imgSize);
        }
        
        if (_downModule != null && !_downModule.IsTreated)
        {
            _downModule.IsTreated = true;
            var pos = gameObject.transform.localPosition;
            _downModule.gameObject.transform.localPosition = new Vector3(pos.x, pos.y - imgSize.y, 0);
            _downModule.Magnet(imgSize);
        }
        
        if (_rightModule != null && !_rightModule.IsTreated)
        {
            _rightModule.IsTreated = true;
            var pos = gameObject.transform.localPosition;
            _rightModule.gameObject.transform.localPosition = new Vector3(pos.x + imgSize.x, pos.y, 0);
            _rightModule.Magnet(imgSize);
        }
        
        if (_leftModule != null && !_leftModule.IsTreated)
        {
            _leftModule.IsTreated = true;
            var pos = gameObject.transform.localPosition;
            _leftModule.gameObject.transform.localPosition = new Vector3(pos.x - imgSize.x, pos.y, 0);
            _leftModule.Magnet(imgSize);
        }
    }


    public virtual void OnPlayerEnter()
    {
        if(_moduleFeature != null)
            _moduleFeature.OnPlayerEnter();
    }
    public virtual void OnPlayerLeave()
    {
        if(_moduleFeature != null)
            _moduleFeature.OnPlayerLeave();
    }

    public virtual bool CanMove()
    {
        if (IsBlockedModule)
            return false;
        
        return true;
    }

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