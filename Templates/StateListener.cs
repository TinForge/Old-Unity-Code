using UnityEngine;

///Base class listens for game state changes
public class StateListener : MonoBehaviour
{
    protected virtual void Awake()
    {
        State.OnGameStart += OnGameStart;
        State.OnGameEnd += OnGameEnd;
    }

    #region Callback

    //Game Starts, activate Objects
    protected void OnGameStart()
    {
        Activate();
    }

    //Game Ends, deactivate Objects
    protected void OnGameEnd()
    {
        Deactivate();
    }

    #endregion

    #region Override


    //Game Starts, activate Objects
    protected virtual void Activate()
    {

    }

    //Game Ends, deactivate Objects
    protected virtual void Deactivate()
    {

    }

    #endregion


}
