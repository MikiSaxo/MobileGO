using System.Collections;
using UnityEngine;

public class Level : MonoBehaviour
{
    [field:SerializeField] public Module StartModule { get; set; }

    [SerializeField] private WallPos[] _wallsPos;
    [SerializeField] private Wall[] _walls;

    private void Start()
    {
        StartCoroutine(GoMagnet());
    }

    IEnumerator GoMagnet()
    {
        yield return new WaitForSeconds(0.1f);
        StartModule.gameObject.GetComponent<StartPoint>().GoMagnetModules();

        foreach (var wallPos in _wallsPos)
        {
            wallPos.GoMagnet();
        }
        
        foreach (var wall in _walls)
        {
            wall.GoWaitToReplace();
        }
    }
}
