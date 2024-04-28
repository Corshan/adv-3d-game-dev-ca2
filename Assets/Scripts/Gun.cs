using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField][Range(10f, 100f)] private float _bulletForce;
    [SerializeField] private Transform _guntip;
    [SerializeField][Range(1, 10)] private float _bulletTimer = 3;
    [SerializeField][Range(1, 100)] private int _maxAmmo = 10;
    [SerializeField][Range(1, 10)] private int _ammoAmount = 5;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [SerializeField] private bool _infiniteAmmo = false;
    private int _counter;
    private int _currentAmmo;
    public int CurrentAmmo => _currentAmmo;
    public int MaxAmmo => _maxAmmo;

    private void Start()
    {
        _currentAmmo = _maxAmmo;
    }

    private void Update()
    {
        if(_ammoText != null) _ammoText.text = $"Ammo => {_currentAmmo}";
    }

    public void Fire()
    {
        if (_currentAmmo <= 0 && !_infiniteAmmo) return;

        GameObject go = Instantiate(_bulletPrefab, _guntip.position, _guntip.rotation);


        go.name = $"Bullet {_counter}";
        _counter++;

        Rigidbody rb = go.GetComponent<Rigidbody>();

        rb.AddForce(_guntip.forward * _bulletForce, ForceMode.Impulse);

        Destroy(go, _bulletTimer);
        _currentAmmo--;
    }

    public bool HasAmmo() => _currentAmmo > 0;

    private void OnTriggerEnter(Collider other)
    {
        // if (other.CompareTag("Ammo") && _currentAmmo < _maxAmmo)
        // {
        //     _currentAmmo = ((_currentAmmo + _ammoAmount) < _maxAmmo) ? _currentAmmo + _ammoAmount : _maxAmmo;

        //     Destroy(other.gameObject);
        // }

    }

}
