using UnityEngine;

// from https://youtu.be/AoRBZh6HvIk?si=MuGyEkDGKNrEZuWy
namespace TheLastLand._Project.Scripts.Background
{
    public class ParallaxEffect : MonoBehaviour
    {
        private float _currentStartPos, _length;
        private GameObject _camera;
        [SerializeField] private float parallaxMultiplier;

        private void Awake()
        {
            _camera = GameObject.FindWithTag("MainCamera");
            _currentStartPos = transform.position.x;
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void FixedUpdate()
        {
            // 0 = with cam, 1 = static, 0.5 = half speed
            var distance = _camera.transform.position.x * parallaxMultiplier;
            var movement = _camera.transform.position.x * (1 - parallaxMultiplier);
            
            transform.position = new Vector3(
                _currentStartPos + distance,
                transform.position.y,
                transform.position.z
            );
            
            if (movement > _currentStartPos + _length)
            {
                _currentStartPos += _length;
            }
            else if (movement < _currentStartPos - _length)
            {
                _currentStartPos -= _length;
            }
        }
    }
}