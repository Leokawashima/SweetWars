using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// イイ感じに非同期でロードする処理と演出
/// </summary>

public class LoadingScript : MonoBehaviour
{
    [Header("変化させるスライダー")]
    [SerializeField] Slider slider;
    /// <param>長すぎず短すぎない程度にする</param>
    [Header("ロードにかかる最低時間")]
    [SerializeField] float misTime = 5f;

    void Start()
    {
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        AsyncOperation _loadNextScene = SceneManager.LoadSceneAsync(Name.SceneName_Int(Name.Scene.Game));
        _loadNextScene.allowSceneActivation = false;
        bool _isLoaded = false;
        float _elapsedTime = 0;

        while(!_isLoaded)
        {
            float _perTime = Mathf.Clamp(Mathf.Pow(_elapsedTime / misTime, 3), 0, 0.9f);
            slider.value =  _perTime > _loadNextScene.progress ? _loadNextScene.progress : _perTime;
            _elapsedTime += Time.deltaTime;

            if(_elapsedTime > misTime)
            {
                if(!_loadNextScene.isDone)
                {
                    _loadNextScene.allowSceneActivation = true;
                    _isLoaded = true;
                }
            }

            yield return null;
        }
    }
}
