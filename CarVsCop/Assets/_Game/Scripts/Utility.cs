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

        public static int GetDefense(int levelNumber)
        {
            int rand1 = 20;
            int rand2 = 25;
            double[] mulArray2 = new double[]
            {
            1,
            1.05,
            1.07,
            1.09,
            1.11,
            1.13,
            1.15,
            1.19,
            1.21,
            1.25,
            1,
            1.02,
            1.01,
            1.03,
            1.04
            };
            double calc5 = ((levelNumber * new System.Random().Next(rand1, rand2)) * mulArray2[new System.Random().Next(0, 15)]) + new System.Random().Next(-6, levelNumber);
            double[] mulArray = new double[]
            {
            0.23,
            0.25,
            0.27,
            0.29,
            0.32,
            0.37,
            0.41,
            0.46,
            0.51,
            0.55,
            0.57,
            0.62,
            0.68,
            0.69,
            0.76
            };
            double calc3Mul = new System.Random().Next(1, 1) * mulArray[new System.Random().Next(0, 15)];
            double calcEnd3 = Math.Round((levelNumber * calc3Mul) + new System.Random().Next(2, 10));
            double calcEnd4 = Math.Round(calc5 / levelNumber);
            int def5 = (int)Math.Round(calcEnd3 + calcEnd4);
            return def5;
        }

        public static int GetDefense(int levelNumber, int maxLevel)
        {
            int rand1 = 20;
            int rand2 = 25;
            double[] mulArray2 = new double[]
            {
            1,
            1.05,
            1.07,
            1.09,
            1.11,
            1.13,
            1.15,
            1.19,
            1.21,
            1.25,
            1,
            1.02,
            1.01,
            1.03,
            1.04
            };
            double calc5 = ((levelNumber * new System.Random().Next(rand1, rand2)) * mulArray2[new System.Random().Next(0, 15)]) + new System.Random().Next(-6, levelNumber);
            double[] mulArray = new double[]
            {
            0.19,
            0.21,
            0.23,
            0.25,
            0.28,
            0.31,
            0.34,
            0.37,
            0.41,
            0.46,
            0.49,
            0.51,
            0.52,
            0.53,
            0.59
            };
            double calc3Mul = new System.Random().Next(1, 1) * mulArray[new System.Random().Next(0, 15)];
            double calcEnd3 = Math.Round((levelNumber * calc3Mul) + new System.Random().Next(2, 10));
            double calcEnd4;
            if (levelNumber < 15 && maxLevel < 15)
            {
                calcEnd4 = Math.Round((calc5 / levelNumber) - 3);
            }
            else
            {
                calcEnd4 = Math.Round((calc5 / levelNumber) + new System.Random().Next(1, (int)Math.Ceiling(levelNumber / 2.0)));
            }
            int def5 = (int)Math.Round(calcEnd3 + calcEnd4);
            return def5;
        }
    }
}