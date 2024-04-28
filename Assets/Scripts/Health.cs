using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private Image image;
    private int _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        if (_currentHealth <= 0) Destroy(gameObject);

        image.fillAmount = (float)_currentHealth / (float)_maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            _currentHealth -= 10;
            Destroy(other.gameObject);
        }

    }
}
