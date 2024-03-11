using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Module : MonoBehaviour
{
    [field:SerializeField] public bool IsBlockedModule { get; set; }
    
    [Header("----- Modules -----")]
    [SerializeField] protected Module _topModule;
    [SerializeField] protected Module _leftModule;
    [SerializeField] protected Module _rightModule;
    [SerializeField] protected Module _downModule;
    
    [Header("----- Sprites -----")]
    [SerializeField] protected Image _imgMod;
    [SerializeField] protected Sprite[] _spritesMod;

    public bool IsDeathModule { get; set; }

    private List<Module> _modules = new List<Module>();
    private List<Wall> _walls = new List<Wall>();

    public bool IsTreated { get; set; }

    private ModuleFeature _moduleFeature;

    private void Awake()
    {
        _modules.Add(_topModule);
        _modules.Add(_leftModule);
        _modules.Add(_rightModule);
        _modules.Add(_downModule);
  
        for (int i = 0; i < 4; i++)
        {
            _walls.Add(null);
        }
    }

    private void Start()
    {
        if(GetComponent<ModuleFeature>() != null)
            _moduleFeature = GetComponent<ModuleFeature>();
        
        int randomNum = Random.Range(0, _spritesMod.Length);
        if(_imgMod != null)
            _imgMod.sprite = _spritesMod[randomNum];
    }

    public Module GetModuleNeighbor(Directions dir)
    {
        if (dir == Directions.Right && _modules[2] != null && _walls[2] == null)
            return _modules[2];
        if (dir == Directions.Left && _modules[1] != null && _walls[1] == null)
            return _modules[1];
        if (dir == Directions.Top && _modules[0] != null && _walls[0] == null)
            return _modules[0];
        if (dir == Directions.Down && _modules[3] != null && _walls[3] == null)
            return _modules[3];

        return null;
    }

    public void AddWall(Wall[] walls)
    {
        for (int i = 0; i < _walls.Count; i++)
        {
            if (walls[i] != null)
                _walls[i] = walls[i];
        }
    }

    public void ResetWall()
    {
        for (int i = 0; i < _walls.Count; i++)
        {
            _walls[i] = null;
        }
    }

    public void Magnet(Vector2 imgSize)
    {
        for (int i = 0; i < _modules.Count; i++)
        {
            if (_modules[i] != null && !_modules[i].IsTreated)
            {
                _modules[i].IsTreated = true;
                var pos = gameObject.transform.localPosition;
                var newPos = Vector3.zero;
                
                switch (i)
                {
                    case 0:
                        newPos = new Vector3(pos.x, pos.y + imgSize.y, 0);
                        break;
                    case 1:
                        newPos = new Vector3(pos.x - imgSize.x, pos.y, 0);
                        break;
                    case 2:
                        newPos = new Vector3(pos.x + imgSize.x, pos.y, 0);
                        break;
                    case 3:
                        newPos = new Vector3(pos.x, pos.y - imgSize.y, 0);
                        break;
                }

                _modules[i].gameObject.transform.localPosition = newPos;
                _modules[i].Magnet(imgSize);
            }
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