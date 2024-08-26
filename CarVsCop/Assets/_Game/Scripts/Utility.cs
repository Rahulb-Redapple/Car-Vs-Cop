using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace RacerVsCops
{
    internal static class Utility
	{
		private static Dictionary<float, WaitForSeconds> _waitForSecDict = new Dictionary<float, WaitForSeconds>(100);
		private static Camera _cameraRef = null;

		public static string RemoveAndReplaceSpecialChars(string inputString, string replacementString = "")
		{
			string trimmedString;
            trimmedString = Regex.Replace(inputString, @"[^0-9a-zA-Z]+", replacementString);
			return trimmedString;
		}

		internal static string GetRandomPlayerDisplayName()
		{
			DateTime currentDateTime = DateTime.Now;
			return "User" + currentDateTime.Minute + currentDateTime.Second /*+ currentDateTime.Millisecond*/;
		}

		public static WaitForSeconds GetWaitForSeconds(float time)
		{
			if (_waitForSecDict.TryGetValue(time, out var _time))
			{
				return _waitForSecDict[time];
			}
			_waitForSecDict[time] = new WaitForSeconds(time);
			return _waitForSecDict[time];
		}

		internal static Vector2 GetCameraEdgeMidPosition(Direction direction)
        {
            return GetCameraEdgeMidPosition(direction, Camera.main);
		}

		internal static Vector2 GetCameraEdgeMidPosition(Direction direction, Camera camera)
        {
            switch(direction)
            {
                case Direction.UP:
                    return camera.ViewportToWorldPoint(new Vector2(0.5f, 1f));
                case Direction.DOWN:
                    return camera.ViewportToWorldPoint(new Vector2(0.5f, 0f));
				case Direction.LEFT:
					return camera.ViewportToWorldPoint(new Vector2(0f, 0.5f));
				case Direction.RIGHT:
					return camera.ViewportToWorldPoint(new Vector2(1f, 0.5f));
                default:
                    return Vector2.zero;
			}
		}

		internal static Vector2 GetTransformPositionInViewport(Vector2 worldPosition)
		{
			return GetTransformPositionInViewport(worldPosition, Camera.main);
		}

		internal static Vector2 GetTransformPositionInViewport(Vector2 worldPosition, Camera camera)
        {
            return camera.WorldToViewportPoint(worldPosition);
		}

		internal static Vector2 GetTransformPositionInWorld(Vector2 viewportPosition)
		{
			return GetTransformPositionInWorld(viewportPosition, Camera.main);
		}

		internal static Vector2 GetTransformPositionInWorld(Vector2 viewportPosition, Camera camera)
		{
			return camera.ViewportToWorldPoint(new Vector3(viewportPosition.x, viewportPosition.y, camera.nearClipPlane));
		}

		internal static void UpdateCameraOrthographicSize(float newSize)
        {
            UpdateCameraOrthographicSize(newSize, Camera.main);
        }

		internal static void UpdateCameraOrthographicSize(float newSize, Camera camera)
        {
            camera.orthographicSize = newSize;
        }

        internal static Vector2 GetRandomPointInUnitSphere()
        {
            return UnityEngine.Random.insideUnitCircle.normalized;
        }

		internal static Vector3 GetRandomPosOffScreen()
		{
			return GetRandomPosOffScreen(Camera.main);
		}

		internal static Vector3 GetRandomPosOffScreen(Camera camera)
		{
			float x = UnityEngine.Random.Range(-0.2f, 0.2f);
			float y = UnityEngine.Random.Range(-0.2f, 0.2f);
			if (x >= 0) x += 1;
			if (y >= 0) y += 1;
			Vector3 randomPoint = new Vector2(x, y);

			randomPoint.z = camera.nearClipPlane;
			Vector3 worldPoint = Camera.main.ViewportToWorldPoint(randomPoint);

			return worldPoint;
		}

		internal static float GetScreenWidth()
		{
			return GetScreenHeight() * Camera.main.aspect;
		}

		internal static float GetScreenHeight()
		{
			return Camera.main.orthographicSize * 2f;
		}

        internal static Camera GetCameraReference()
        {
            if(Equals(_cameraRef, null))
			{
				_cameraRef = Camera.main;
			}
			return _cameraRef;
        }

		internal static void Cleanup()
		{
			_cameraRef = null;
            _waitForSecDict.Clear();

        }
    }
}