using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class CMSDefaultCar : CMSEntity
{
    public CMSDefaultCar()
    {
        Define<TagDefaultCar>().paternPath = $"{PaternCreator.folder}/BasePatern.json";
    }
}

public class TagDefaultCar : CMSComponent
{
    public string paternPath;

    public List<Vector2Int> UnpackPatern()
    {
        PaternCreator.SerializeList temp = JsonUtility.FromJson<PaternCreator.SerializeList>(File.ReadAllText(paternPath));

        return temp.Unpack();
    }
}
