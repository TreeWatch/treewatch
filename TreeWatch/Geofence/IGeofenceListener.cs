using System;

namespace TreeWatch
{
    public interface IGeofenceListener
    {
        void OnMonitoringStarted(string identifier);
        void OnMonitoringStopped();
        void OnMonitoringStopped(string identifier);
        void OnRegionStateChanged(GeofenceResult result);
        void OnError(String error);
    }
}

