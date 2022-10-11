using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    public string PlayerName;
    public NetworkType NetType;
    [SerializeField] UnityEvent<GameState> OnGameStateChanged; // invoke method can not used though scenes
    public string ipaddress;
    public int port;
    void Awake()
    {
        CheckInstance();
        DontDestroyOnLoad(gameObject);
    }
    void CheckInstance()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        UpdateGameState(GameState.SelectGamemode);
    }
    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.SelectGamemode:
                break;
            case GameState.WaitingPlayer:
                break;
            case GameState.Prepare:
                break;
            case GameState.Playing:
                break;
            case GameState.Result:
                break;
            case GameState.Connecting:
                break;
            default:
                break;
        }
        OnGameStateChanged?.Invoke (newState);
    }
    public void SetNetworkType(NetworkType _NetType)
    {
        NetType = _NetType;
    }
    public NetworkType GetNetworkType()
    {
        return NetType;
    }
}
public enum GameState
{
    SelectGamemode,
    WaitingPlayer,
    Prepare,
    Playing,
    Result,
    Connecting
}
public enum NetworkType
{
    Host,
    Server,
    Client
}