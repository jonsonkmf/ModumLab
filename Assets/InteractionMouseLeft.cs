using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMouseLeft : MonoBehaviour
{
    [SerializeField] Camera _playerCamera;
    [SerializeField] private float _maxDistance = 2f;   //Максимальная дистанция до вентиля
    [SerializeField] private float _minRotate = 0f;     //Минимальный угол поворота вентиля
    [SerializeField] private float _maxRotate = 180f;   //Максимальный угол поворота вентиля
    [SerializeField] private float _angleVelocity = 100f;

    private Transform _target;
    private float startangle;
    private float preangle = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // повесил триггеры на вентили, но можно было сделать еще через тэги и прочее. Просто на сцене другие объекты стоят без коллайдеров.
            // Хотя по тэгу тоже не ок, так как можно записать неверное название (ошибиться легко, проект скомпилится и не подавится)))
            if (Physics.Raycast(ray, out hit, _maxDistance))
            {
                _target = hit.transform;
                var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(_target.transform.position);
                startangle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // начальное значение угла поворота вентиля
                Debug.Log($"Стартовое значение - {startangle}");
            }
			else
			{
                _target = null;
            }
        }

		if (_target!=null)
		{
			//Первый способ
			var direction = Input.mousePosition - Camera.main.WorldToScreenPoint(_target.transform.position); // Нахождение катетов для расчёта тангенса, а в последствии и количества градусов угла. 
			var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Нахождение тангенса угла и перевод его в градусы.
			_target.transform.rotation = Quaternion.AngleAxis(startangle - angle, Vector3.up); // Вращение объекта на полученное количество градусов.
			Debug.Log(-angle);

			//Второй способ вращения через Quaternion.Lerp
			//Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(_target.transform.position);
			//float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			//Quaternion needRotation = Quaternion.Euler(0f, startangle - angle, 0f);
			//_target.transform.rotation = Quaternion.Lerp(_target.transform.rotation, needRotation, _angleVelocity * Time.deltaTime);

		}
	}
}
