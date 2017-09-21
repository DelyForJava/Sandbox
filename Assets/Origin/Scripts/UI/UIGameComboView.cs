using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIGameComboView : MonoBehaviour
{
	public Transform _panel;
	public Button _btnChow;
	public Button _btnPong;
	public Button _btnKong;
	public Button _btnBaoTing;
	public Button _btnWin;
	public Button _btnPass;

	void Awake()
	{
		_panel = transform.Find ("Panel");
		_btnChow = _panel.Find("Combo/Button_Chow").GetComponent<Button>();
		_btnPong = _panel.Find("Combo/Button_Pong").GetComponent<Button>();
		_btnKong = _panel.Find("Combo/Button_Kong").GetComponent<Button>();
		_btnBaoTing = _panel.Find("Combo/Button_BAO_TING").GetComponent<Button>();
		_btnWin = _panel.Find("Combo/Button_Win").GetComponent<Button>();
		_btnPass = _panel.Find("Combo/Button_Pass").GetComponent<Button>();

		// add click audio
		//_btnChow.gameObject.AddComponent<ClickAudio>();
		//_btnPong.gameObject.AddComponent<ClickAudio>();
		//_btnKong.gameObject.AddComponent<ClickAudio>();
		//_btnBaoTing.gameObject.AddComponent<ClickAudio>();
		//_btnWin.gameObject.AddComponent<ClickAudio>();
		//_btnPass.gameObject.AddComponent<ClickAudio>();
	}

	void Start()
	{
	}
}
