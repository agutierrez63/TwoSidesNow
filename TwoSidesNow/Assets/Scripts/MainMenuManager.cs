using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    //#region References
    //[Header("Main Menu Objects")]
    //[SerializeField] private GameObject _loadingBarObject;
    //[SerializeField] private Image _loadingBar;
    //[SerializeField] private GameObject[] _objectsToHide;

    //[Header("Scenes to Load")]
    //// [SerializeField] private string _presistentGameplay;
    //[SerializeField] private SceneAsset _levelScene;

    //private List<AsyncOperation> _scenesToLoad = new List<AsyncOperation>();
    //#endregion

    private void Awake()
    {
        // _loadingBarObject.SetActive(false);
    }

    public void StartGame()
    {
        //HideMenu();

        //_loadingBarObject.SetActive(true);

        SceneManager.LoadScene("Demo");
        // _scenesToLoad.Add(SceneManager.LoadSceneAsync(_levelScene.GetInstanceID(), LoadSceneMode.Additive));

        //StartCoroutine(ProgessLoadingBar());
    }

    //private void HideMenu()
    //{
    //    for(int i= 0; i < _objectsToHide.Length; i++)
    //    {
    //        _objectsToHide[i].SetActive(false);
    //    }
    //}

    //private IEnumerator ProgessLoadingBar()
    //{
    //    float loadProgress = 0f;
    //    for (int i = 0; i < _objectsToHide.Length; i++)
    //    {
    //        while (!_scenesToLoad[i].isDone)
    //        {
    //            loadProgress += _scenesToLoad[i].progress;
    //            _loadingBar.fillAmount = loadProgress / _scenesToLoad.Count;
    //            yield return null;
    //        }
    //    }
    //}
}
