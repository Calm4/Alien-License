using App.Scripts.GameScene.Interfaces;
using UnityEngine;

namespace App.Scripts.GameScene.GameItems
{
    public class DefaultMovableObject : MonoBehaviour, IMovable
    {
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        public BoxCollider GetBoxCollider()
        {
            return _boxCollider;
        }
    }
}
