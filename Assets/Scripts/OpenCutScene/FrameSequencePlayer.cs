using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FrameSequencePlayer : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Image targetImage;

    [Header("Frames")]
    [SerializeField] private Sprite[] frames;

    [Header("Playback")]
    [SerializeField] private float fps = 8f;              // 8~12 比较像动画；想更快就调高
    [SerializeField] private bool playOnStart = true;
    [SerializeField] private bool loop = false;
    [SerializeField] private bool useUnscaledTime = true; // UI/暂停时也能播

    [Header("Skip / Finish")]
    [SerializeField] private bool allowSkip = true;
    [SerializeField] private KeyCode skipKey = KeyCode.Space;

    [Tooltip("播完后要去的场景名；留空 = 不切场景")]
    [SerializeField] private string nextSceneName = "";

    private Coroutine _co;

    private void Start()
    {
        if (playOnStart)
            Play();
    }

    private void Update()
    {
        if (!allowSkip) return;

        // 你也可以改成 Input.anyKeyDown 或 鼠标点击
        if (Input.GetKeyDown(skipKey) || Input.GetMouseButtonDown(0))
        {
            Finish();
        }
    }

    public void Play()
    {
        if (_co != null) StopCoroutine(_co);
        _co = StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        if (targetImage == null || frames == null || frames.Length == 0)
            yield break;

        float frameTime = 1f / Mathf.Max(1f, fps);

        do
        {
            for (int i = 0; i < frames.Length; i++)
            {
                targetImage.sprite = frames[i];

                if (useUnscaledTime)
                    yield return new WaitForSecondsRealtime(frameTime);
                else
                    yield return new WaitForSeconds(frameTime);
            }
        } while (loop);

        Finish();
    }

    private void Finish()
    {
        if (_co != null)
        {
            StopCoroutine(_co);
            _co = null;
        }

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
