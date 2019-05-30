using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

	[SerializeField]
	RectTransform thrusterFuelFill;

	[SerializeField]
	RectTransform healthBarFill;

	[SerializeField]
	Text ammoText;

	[SerializeField]
	GameObject pauseMenu;

	[SerializeField]
	GameObject scoreboard;

	private Player player;
	private PlayerController controller;
	private WeaponManager weaponManager;

	public void SetPlayer(Player _player)
	{
		player = _player;
		controller = player.GetComponent<PlayerController>();
		weaponManager = player.GetComponent<WeaponManager>();
	}

	private void Start()
	{
		PauseMenu.IsOn = false;
	}

	private void Update()
	{
		SetFuelAmount(controller.GetThrusterFuelAmount());
		SetHealthAmount(player.GetHealthPct());
		SetAmmoAmount(weaponManager.GetCurrentWeapon().bullets, weaponManager.GetCurrentWeapon().maxBullets);

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TogglePauseMenu();
		}

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			scoreboard.SetActive(true);
		}
		else if (Input.GetKeyUp(KeyCode.Tab))
		{
			scoreboard.SetActive(false);
		}
	}

	public void TogglePauseMenu()
	{
		pauseMenu.SetActive(!pauseMenu.activeSelf);
		PauseMenu.IsOn = pauseMenu.activeSelf;
	}

	void SetFuelAmount(float _amount)
	{
		thrusterFuelFill.localScale = new Vector3(1f, _amount, 1f);
	}

	void SetHealthAmount(float _amount)
	{
		healthBarFill.localScale = new Vector3(1f, _amount, 1f);
	}

	void SetAmmoAmount(int _amount, int _maxAmmo)
	{
		ammoText.text = _amount.ToString() + " / " + _maxAmmo.ToString();
	}

	public void SetLookSensitivity(float _sensitivity)
	{
		if (controller == null)
			return;
		else
			controller.lookSensitivity = _sensitivity;
	}
}
