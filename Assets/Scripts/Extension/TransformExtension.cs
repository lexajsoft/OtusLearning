using UnityEngine;

namespace Code.Extensions
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

		public static void DestroyChildren(this Transform transform)
		{
			if (transform == null)
				return;

			int childCount = transform.childCount;

			for (int i = 0; i < childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (child != null)
				{
					Object.Destroy(child.gameObject);
				}
			}
		}
	}
}