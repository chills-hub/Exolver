using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
	//offset from the viewport center to fix damping
	public float m_DampTime = 0.1f;
	public Transform m_Target;
	public float m_XOffset = 0;
	public float m_YOffset = 0;

	private float margin = 0.1f;

	void Start()
	{
		if (m_Target == null)
		{
			m_Target = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}

	void Update()
	{
		if (m_Target)
		{
			float targetX = m_Target.position.x + m_XOffset;
			float targetY = m_Target.position.y + m_YOffset;

			if (Mathf.Abs(transform.position.x - targetX) > margin)
				targetX = Mathf.Lerp(transform.position.x, targetX, 1 / m_DampTime * Time.deltaTime);

			if (Mathf.Abs(transform.position.y - targetY) > margin)
				targetY = Mathf.Lerp(transform.position.y, targetY, 1 / m_DampTime * Time.deltaTime);

			transform.position = new Vector3(targetX, targetY, transform.position.z);
		}
	}

	static FollowCam followCamInstance;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);

		if (followCamInstance == null)
		{
			//First run, set the instance
			followCamInstance = this;

		}
		else if (followCamInstance != this)
		{
			//Instance is not the same as the one we have, destroy old one, and reset to newest one
			Destroy(followCamInstance.gameObject);
			followCamInstance = this;
		}
	}
}
