using UnityEngine;

namespace Extensions
{
	public static class TransformExtension
	{
		public static UnityEngine.Vector2 Pos2(this Transform transform) => 
			new(transform.position.x, transform.position.y);

		public static float Sqr2DDistance(this Transform tr, Transform other)
		{
			var vector = tr.position - other.position;
			return UnityEngine.Vector2.SqrMagnitude(vector);
		}

		public static void DestroyAll(this Transform transform)
		{
			int childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				GameObject.Destroy(transform.GetChild(i).gameObject);
			}
		}

	}
}