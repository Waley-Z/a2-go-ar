namespace Mapbox.Unity.Location
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.IO;
	using Mapbox.Unity.Utilities;
	using Mapbox.Utils;
	using UnityEngine;

	/// <summary>
	/// <para>
	/// The EditorLocationProvider is responsible for providing mock location data via log file collected with the 'LocationProvider' scene
	/// </para>
	/// </summary>
	public class EditorLocationProviderLocationLog : AbstractEditorLocationProvider
	{


		/// <summary>
		/// The mock "latitude, longitude" location, respresented with a string.
		/// You can search for a place using the embedded "Search" button in the inspector.
		/// This value can be changed at runtime in the inspector.
		/// </summary>
		[SerializeField]
		private TextAsset _locationLogFile;


		private LocationLogReader _logReader;
		private IEnumerator<Location> _locationEnumerator;


#if UNITY_EDITOR
		protected override void Awake()
		{
			base.Awake();
			_logReader = new LocationLogReader(_locationLogFile.bytes);
			_locationEnumerator = _logReader.GetLocations();
            _currentLocation.LatitudeLongitude = new Vector2d(42.279594, -83.732124);
        }

        private void Update()
        {
			Vector2d deltaVec = Vector2d.zero;
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
			{
				deltaVec = new Vector2d(0, -1);
			}
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                deltaVec = new Vector2d(0, 1);
            }
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                deltaVec = new Vector2d(1, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                deltaVec = new Vector2d(-1, 0);
            }
			_currentLocation.LatitudeLongitude += deltaVec * 0.001 * Time.deltaTime;
			//print("Current location: " + _currentLocation.LatitudeLongitude);
		}
#endif


        private void OnDestroy()
		{
			if (null != _locationEnumerator)
			{
				_locationEnumerator.Dispose();
				_locationEnumerator = null;
			}
			if (null != _logReader)
			{
				_logReader.Dispose();
				_logReader = null;
			}
		}


		protected override void SetLocation()
		{
#if !UNITY_EDITOR
            if (null == _locationEnumerator) { return; }

			// no need to check if 'MoveNext()' returns false as LocationLogReader loops through log file
			_locationEnumerator.MoveNext();
			_currentLocation = _locationEnumerator.Current;
#endif
		}
	}
}
