using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Sequence> sequences = new List<Sequence>();
    public List<GameObject> Cars = new List<GameObject>();

    [Space]
    public TrunUI turnUI;
    public Switcher cameraControllSwitcher;

    private float cellPoolOffset;
    public Transform cellPool;

    public bool playerEndMove = false;
    private void Awake()
    {
        Game.GameManager = this;

        CMS.Init();

        cameraControllSwitcher = Camera.main.GetComponent<CameraControll>().MSwitcher.CreateNewSwitcher();
        cameraControllSwitcher.Switch = false;
    }

    private IEnumerator Start()
    {
        yield return null;
        cellPoolOffset = cellPool.transform.position.x - Game.player.transform.position.x;

        while (true) // ������� ����
        {
            yield return PlayerMovePhase();
            yield return GamePhase();
            yield return FinishMove();
        }
    }

    private IEnumerator PlayerMovePhase()
    {
        yield return Game.TurnUI.TurnOnUI(); // �������� UI
        cameraControllSwitcher.Switch = true; // �������� ���������� �������

        foreach (var car in Cars)
        {
            car.GetComponent<Car>().DefineAcceleration(); // ���������� ��������� ��� ���� ������� � ����
        }

        Game.player.VisualizePatern(); // ���������� ������ ������

        yield return new WaitUntil(() => playerEndMove); // ���� ����� ����� �������� ���

        cameraControllSwitcher.Switch = false;

        Game.player.HidePatern();

        yield return Game.TurnUI.TurnOffUI();

        Game.TurnUI.ConfirmSpeed();
        SetDirection();

        yield return new WaitForSeconds(0.3f);

        yield break;
    }

    private IEnumerator GamePhase()
    {
        do
        {
            MakeRoutePhase();
            yield return ApplyMovePhase();
        }while (!CheckFinishCondition());
    }

    private void MakeRoutePhase()
    {
        foreach (GameObject go in Cars)
        {
            Car car = go.GetComponent<Car>();
            if (car.CellSelected) // ���� ������� ������� ������
            {
                IntPos ip = go.GetComponent<IntPos>();

                ip.CreateChangePosContext(ip.pos, car.SelectedCell); // ������ �������
            }
        }
    }

    private IEnumerator ApplyMovePhase()
    {
        foreach(GameObject go in Cars)
        {
            go.GetComponent<EntityRenderer>().ChangePosition(); // ���������� ������ � ��������� �������
        }

        yield return new WaitUntil(GetAllSequanceState);

        KillSequances();

        foreach (GameObject go in Cars)
        {
            go.GetComponent<IntPos>().ApplyMove();
        }

        // ��������� ����, �������� ������� � ���������� �������
        yield break;
    }

    public bool GetAllSequanceState()
    {
        bool complated = true;
        foreach (var item in sequences)
        {
            complated = complated && item.IsComplete();
        }

        return complated;
    }

    public void KillSequances()
    {
        foreach (var item in sequences)
        {
            item.Kill();
        }

        sequences.Clear();
    }

    private bool CheckFinishCondition()
    {
        // ��������� ������� ���������� ����
        return true;
    }

    private void SetDirection()
    {
        foreach (var car in Cars)
        {
            Car a = car.GetComponent<Car>();
            IntPos b = a.GetComponent<IntPos>();

            if (a.SelectedCell != b.pos)
            {
                a.Direction = ((Vector2)(a.SelectedCell - b.pos)).normalized;
            }
        }
    }

    private IEnumerator FinishMove()
    {
        foreach (GameObject go in Cars)
        {
            Car car = go.GetComponent<Car>();
            car.CellSelected = false;
        }

        yield return Camera.main.GetComponent<CameraControll>().CenterCameraOnPlayer();

        playerEndMove = false;

        yield return new WaitForSeconds(0.2f);
        yield break;
    }

    private void AlignPlaceToPlayer()
    {
        cellPool.transform.position = new Vector3(Game.player.transform.position.x - Mathf.Abs(cellPoolOffset), cellPool.transform.position.y, cellPool.transform.position.z);
    }

}
