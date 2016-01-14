// <copyright file="IGeofenceStore.cs" company="TreeWatch">
// Copyright © 2015 TreeWatch
// </copyright>
#region Copyright
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
#endregion
namespace TreeWatch
{
    using System.Collections.Generic;

    /// <summary>
    /// I geofence store.
    /// </summary>
    public interface IGeofenceStore
    {
        /// <summary>
        /// Gets all geofence regions in store
        /// </summary>
        /// <returns>A dictionary with all regions.</returns>
        Dictionary<string, GeofenceCircularRegion> GetAll();

        /// <summary>
        /// Gets an specific geofence from store
        /// </summary>
        /// <param name="id">Region id.</param>
        /// <returns>Region of id.</returns>
        GeofenceCircularRegion Get(string id);

        /// <summary>
        /// Save geofence region in store
        /// </summary>
        /// <param name="region">New region.</param>
        void Save(GeofenceCircularRegion region);

        /// <summary>
        /// Remove all geofences regions from store
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// Remove specific geofence region from store
        /// </summary>
        /// <param name="id">Region id.</param>
        void Remove(string id);
    }
}