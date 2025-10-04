using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrunUI : MonoBehaviour
{
    public RectTransform UpperPoint;
    public RectTransform DownPoint;
    [HideInInspector]public RectTransform _transform;

    public bool UIDownOnStart = false;

    public bool SpeedUped = false;
    public bool SlowDowned = false;

    public int CurrentSpeed;

    [Space]
    public Button confirmButton;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI acceleration;
    private void Awake()
    {
        Game.TurnUI = this;

        _transform = GetComponent<RectTransform>();

        if (UIDownOnStart)
        {
            _transform.position = new Vector3(_transform.position.x, DownPoint.position.y);
        }
        else
        {
            _transform.position = new Vector3(_transform.position.x, UpperPoint.position.y);
        }

    }

    public void OnSpeedUpClick()
    {
        DisableSelection();
        if (SpeedUped)
        {
            Game.player.car.Speed = CurrentSpeed;
            SpeedUped = false;

            Game.player.UpdatePatern();
        }
        else
        {
            if (SlowDowned)
            {
                Game.player.car.Speed = CurrentSpeed;
                SpeedUp();
                SlowDowned = false;
                SpeedUped = true;

                Game.player.UpdatePatern();
            }
            else
            {
                SpeedUp();
                SpeedUped = true;

                Game.player.UpdatePatern();
            }

            if (Game.player.car.Speed == CurrentSpeed)
            {
                SlowDowned = false;
                SpeedUped = false;
            }
        }
        
    }

    public void OnSlowDownClick()
    {
        
        DisableSelection();
        if (SlowDowned)
        {
            Game.player.car.Speed = CurrentSpeed;
            SlowDowned = false;

            Game.player.UpdatePatern();
        }
        else
        {
            if (SpeedUped)
            {
                Game.player.car.Speed = CurrentSpeed;
                SlowDown();
                SlowDowned = true;
                SpeedUped = false;

                Game.player.UpdatePatern();
            }
            else
            {
                SlowDown();
                SlowDowned = true;

                Game.player.UpdatePatern();
            }

            if (Game.player.car.Speed == CurrentSpeed)
            {
                SlowDowned = false;
                SpeedUped = false;
            }
        }
    }

    private void SpeedUp()
    {
        Game.player.car.SpeedUp();
    }

    private void SlowDown()
    {
        Game.player.car.SlowDown();
    }

    private void DisableSelection()
    {
        Game.player.car.CellSelected = false;
    }

    public IEnumerator TurnOnUI()
    {
        _transform.position = new Vector3(_transform.position.x, UpperPoint.position.y);

        yield break;
    }
    
    public IEnumerator TurnOffUI()
    {
        _transform.position = new Vector3(_transform.position.x, DownPoint.position.y);

        yield break;
    }

    public void OnConfirmClick()
    {
        Game.GameManager.playerEndMove = true;
    }

    public void ConfirmSpeed()
    {
        SpeedUped = false;
        SlowDowned = false;

        CurrentSpeed = Game.player.car.Speed;
    }

    private void Update()
    {
        confirmButton.interactable = Game.player.car.CellSelected;
        acceleration.SetText(Game.player.car.Acceleration.ToString());
        speed.SetText(Game.player.car.Speed.ToString());
    }
}
