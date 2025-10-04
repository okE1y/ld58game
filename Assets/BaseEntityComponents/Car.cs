using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

[RequireComponent(typeof(IntPos))]
public class Car : MonoBehaviour
{
    private IntPos intPos;

    [TypeSelect(typeof(CMSEntity))] public EntityType CurrentCMSEntity = new EntityType();
    private TagDefaultCar tagDefaultCar;

    public Vector2Int SelectedCell;
    public bool CellSelected = false;

    public List<Vector2Int> CellPatern = new List<Vector2Int>();

    private Vector2 Direction = Vector2.right;
    public int Speed; // –ассто€ние селл патерна от машинки по направлению Direction

    public int maxSpeed;
    public int Acceleration = 0;

    private void Start()
    {
        intPos = GetComponent<IntPos>();
        Game.GameManager.Cars.Add(gameObject);

        tagDefaultCar = CMS.Get(CurrentCMSEntity.Get()).Define<TagDefaultCar>();

        CellPatern = tagDefaultCar.UnpackPatern();
        maxSpeed = tagDefaultCar.MaxSpeed;
    }

    public int DefineAcceleration()
    {
        Acceleration = UnityEngine.Random.Range(1, 3);
        return Acceleration;
    }

    public void SpeedUp()
    {
        Speed += Acceleration;
        Speed = Mathf.Clamp(Speed, 0, maxSpeed);
    }

    public void SlowDown()
    {
        Speed -= Acceleration;
        Speed = Mathf.Clamp(Speed, 0, maxSpeed);
    }

    public List<Vector2Int> GetCellPaternProjectionOnGamePlace()
    {
        List<Vector2Int> temp = new List<Vector2Int>();
        temp.AddRange(CellPatern);

        for (int i = 0; i < temp.Count; i++)
        {
            temp[i] += intPos.pos + new Vector2Int(Mathf.FloorToInt(Direction.x * Speed), Mathf.FloorToInt(Direction.y * Speed));
        }

        
        return temp.FindAll((x) => 0 < x.x + 1 && x.x + 1 < GamePlace.instance.Place.GetLength(0) &&
        0 < x.y + 1 && x.y + 1 < GamePlace.instance.Place.GetLength(1));

    }

    public void SelectOrDeselectCell(Vector2Int cell, bool selected)
    {
        SelectedCell = cell;
        CellSelected = selected;
    }
}

[System.Serializable]
public class EntityType
{
    public Type entityType;
    [SerializeField] public string typeName;

    public EntityType()
    {
        this.entityType = ReflectionUtil.GetAllSubclasses<CMSEntity>()[0];
        typeName = this.entityType.Name;
    }

    public Type Get()
    {
        entityType = ReflectionUtil.GetAllSubclasses<CMSEntity>().ToList().Find(x => x.Name == typeName);
        return entityType;
    }
}