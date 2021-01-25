using UnityEngine;

[AddComponentMenu("Pool/PoolObject")]
public class PoolObject : MonoBehaviour
{
	public virtual void ReturnToPool()
	{
		gameObject.transform.SetParent(GameObject.Find("Pool").transform);
		gameObject.SetActive(false);
	}
}