using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class PaternCreator : MonoBehaviour
{
    public List<Transform> cells = new List<Transform>();
    [SerializeField, HideInInspector] private List<IntPare> Ints = new List<IntPare>();
    [SerializeField, HideInInspector] private SerializeList list;

    [ContextMenu("Create json")]
    public void CreatePatern()
    {
        Ints = cells.Select<Transform, IntPare>((x) => new IntPare(Mathf.FloorToInt(x.localPosition.x), Mathf.FloorToInt(x.localPosition.y))).ToList();

        list = new SerializeList(Ints);
        string jsonInts = JsonUtility.ToJson(list, true);

        File.WriteAllText($"Paterns/{gameObject.name}_pat.json", jsonInts);

        Ints.Clear();
    }

    [System.Serializable]
    public class IntPare
    {
        [SerializeField] public int x;
        [SerializeField] public int y;

        public IntPare(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    [System.Serializable]
    public class SerializeList
    {
        [SerializeField] public List<IntPare> Ints;

        public SerializeList(List<IntPare> intPares)
        {
            Ints = intPares;
        }

        public List<Vector2Int> Unpack()
        {
            return Ints.Select<IntPare, Vector2Int>((x) => new Vector2Int(x.x, x.y)).ToList();
        }
    }

}
