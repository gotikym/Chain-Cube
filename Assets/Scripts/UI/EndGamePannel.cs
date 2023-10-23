using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private int _stoppedTimeScale = 0;
    private int _runningTimeScale = 1;

    private void OnEnable()
    {
        Cube.Losed += OnLose;
    }

    private void OnDisable()
    {
        Cube.Losed -= OnLose;
    }

    public void OnRestartButtonClick()
    {
        _panel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = _runningTimeScale;
    }

    protected void OnLose()
    {
        _panel.SetActive(true);
        Time.timeScale = _stoppedTimeScale;
    }

    public void Exit()
    {
        Application.Quit();
    }
}