using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaternCellPool : MonoBehaviour
{
    public int objectCount = 200;
    private GameObject prefab;

    private Vector3 poolPos = new Vector3(200, 200);

    private List<GameObject> objectPool = new List<GameObject>();

    private void Awake()
    {
        prefab = "Prefabs/PaternCell".load<GameObject>();
        for (int i = 0; i < objectCount; i++)
        {
            objectPool.Add(Instantiate(prefab, poolPos, Quaternion.identity, transform));

            objectPool[objectPool.Count - 1].SetActive(false);
        }

        Game.PaternCellPool = this;
    }


    public GameObject SetCell(Vector2Int pos)
    {
        GameObject obj = objectPool.Find((x) => !x.activeSelf);
        obj.transform.localPosition = GamePlace.instance.cellSize * (Vector2)pos;
        obj.GetComponent<PaternCell>().pos = pos;
        obj.SetActive(true);
        return obj;
    }

    public bool RemoveCell(GameObject PaternCell)
    {
        if (objectPool.Contains(PaternCell))
        {
            PaternCell.GetComponent<MouseControll>().ResetControll();
            PaternCell.GetComponent<PaternCell>().ResetControll();

            PaternCell.SetActive(false);
            PaternCell.transform.position = poolPos;

            return true;
        }
        else
        {
            return false;
        }
    }
}
