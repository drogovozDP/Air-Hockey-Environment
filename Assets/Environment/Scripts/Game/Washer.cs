using UnityEngine;


public class Washer : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GetComponentInParent<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        string playerTag = collision.gameObject.tag;
        if ((playerTag == _gameManager.playerTagBlue) || (playerTag == _gameManager.playerTagRed))
            _gameManager.HitWasher(playerTag);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_gameManager.gateTagBlue) || other.CompareTag(_gameManager.gateTagRed))
            _gameManager.Goal(other.tag);
    }
}
