using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using Unity.Mathematics;
using UnityEngine.Events;

public class MultipleSwitcher
{
    private List<Switcher> switchers = new List<Switcher>();

    private bool switcherState = true;

    private Action SwitcherSetsTrue;
    private Action SwitcherSetsFalse;

    public MultipleSwitcher(Action SwitcherSetsTrue = null, Action SwitcherSetsFalse = null)
    {
        this.SwitcherSetsTrue = SwitcherSetsTrue;
        this.SwitcherSetsFalse = SwitcherSetsFalse;
    }

    public Switcher CreateNewSwitcher()
    {
        switchers.Add(new Switcher(this, OnSwitcherTrue, OnSwitcherFalse));
        return switchers[switchers.Count - 1];
    }

    public void RemoveSwitch(Switcher switcher)
    {
        switchers.Remove(switcher);
    }

    public bool GetSwitchState()
    {
        bool result = true;

        foreach (Switcher switcher in switchers)
        {
            if (!switcher.Switch)
            {
                result = false;
                break;
            }
        }

        return result;
    }

    private void OnSwitcherFalse()
    {
        bool result = false;

        foreach (Switcher switcher in switchers)
        {
            if (!switcher.Switch)
            {
                result = true;
                break;
            }
        }

        if (result && switcherState)
        {
            switcherState = false;
            if (SwitcherSetsFalse != null)
                SwitcherSetsFalse();
        }
        else
        {
            switcherState = !result;
        }
    }

    private void OnSwitcherTrue()
    {
        bool result = false;

        foreach (Switcher switcher in switchers)
        {
            if (!switcher.Switch)
            {
                result = true;
                break;
            }
        }

        if (!result && !switcherState)
        {
            switcherState = true;
            if (SwitcherSetsTrue != null)
                SwitcherSetsTrue();
        }
        else
        {
            switcherState = !result;
        }
    }
}

public class Switcher
{
    private MultipleSwitcher multipleSwitcher;

    private UnityAction OnSetTrue;
    private UnityAction OnSetFalse;

    public bool _switch = true;
    public bool Switch
    {
        get => _switch;
        set
        {

            _switch = value;

            if (value)
                OnSetTrue();
            else
                OnSetFalse();
        }
    }

    public Switcher(MultipleSwitcher multipleSwitcher, UnityAction onSetTrue, UnityAction onSetFalse)
    {
        this.multipleSwitcher = multipleSwitcher;
        
        OnSetTrue = onSetTrue;
        OnSetFalse = onSetFalse;
    }

    public void RemoveSwitcherFromBase()
    {
        multipleSwitcher.RemoveSwitch(this);
    }

}
