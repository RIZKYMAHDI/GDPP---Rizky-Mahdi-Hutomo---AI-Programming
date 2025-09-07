using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Action OnPowerupStart;
    public Action OnPowerupStop;
    [SerializeField] private float _speed;
    [SerializeField] private Camera _camera;
    [SerializeField]
    private float _powerupDuration;
    [SerializeField]
    private int _health;
    [SerializeField]
    private TMP_Text _healthText;
    [SerializeField]
    private Transform _respawnPoint;

    private Rigidbody _rigidbody;
    private Coroutine _powerupCoroutine;
    private bool _isPowerUpActive = false;

    public void Dead()
    {
        _health -= 0;
        UpdateUI();
        SceneManager.LoadScene("LoseScreen");
    }
    public void PickPowerUp()
    {
        if (_powerupCoroutine != null)
        {
            StopCoroutine(_powerupCoroutine);
        }
        _powerupCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        _isPowerUpActive = true;
        if (OnPowerupStart != null)
        {
            OnPowerupStart();
        }
        yield return new WaitForSeconds(_powerupDuration);
        _isPowerUpActive = false;
        if (OnPowerupStop != null)
        {
            OnPowerupStop();
        }
    }

    private void Awake()
    {
        UpdateUI();
        _rigidbody = GetComponent<Rigidbody>();
        HideAndLockCursor();
    }

    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal = a or left (-) & d or right (+)
        float horizontal = Input.GetAxis("Horizontal");
        // Vertical = s or down (-) & w or up (+)
        float vertical = Input.GetAxis("Vertical");

        Vector3 horizontalDirection = horizontal * _camera.transform.right;
        Vector3 verticalDirection = vertical * _camera.transform.forward;
        horizontalDirection.y = 0;
        verticalDirection.y = 0;

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        _rigidbody.linearVelocity = movementDirection * _speed; Debug.Log("Vertical: " + vertical);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_isPowerUpActive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }
    private void UpdateUI()
    {
        _healthText.text = "Health: " + _health;
    }
}