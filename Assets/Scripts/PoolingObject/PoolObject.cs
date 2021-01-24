using UnityEngine;

[AddComponentMenu("Pool/PoolObject")]
public class PoolObject : MonoBehaviour
{

	#region Interface
	public virtual void ReturnToPool()
	{
		gameObject.transform.SetParent(GameObject.Find("Pool").transform);
		gameObject.SetActive(false);
	}
	#endregion
}