using UnityEngine;
using UnityEngine.SceneManagement;

namespace LiquidTetris
{
	[CreateAssetMenu(fileName = "Scenes", menuName = "Data Containers/Scenes")]
	public sealed class Scenes : ScriptableObject
	{
		[SerializeField] private string _mainMenuSceneName;
		[SerializeField] private string _onlineMatchSceneName;
		[SerializeField] private string _offlineMatchSceneName;

		public string MainMenuSceneName => _mainMenuSceneName;
		public string OnlineMatchSceneName => _onlineMatchSceneName;
		public string OfflineMatchSceneName => _offlineMatchSceneName;

		public void DownloadMainMenuScene()
		{
			SceneManager.LoadScene(_mainMenuSceneName);
		}

		public void DownloadOnlineMatchScene()
		{
			SceneManager.LoadScene(_onlineMatchSceneName);
		}

		public void DownloadOfflineMatchScene()
		{
			SceneManager.LoadScene(_offlineMatchSceneName);
		}
	}
}
