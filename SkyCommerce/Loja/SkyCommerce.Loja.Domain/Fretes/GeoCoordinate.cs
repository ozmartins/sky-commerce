using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SkyCommerce.Loja.Domain.Fretes
{
    internal class GeoCoordinate : IEquatable<GeoCoordinate>
    {
        public static readonly GeoCoordinate Unknown = new GeoCoordinate();

        private double _course;
        private double _horizontalAccuracy;
        private double _verticalAccuracy;
        private double _latitude;
        private double _longitude;        
        private double _speed;
        
        public GeoCoordinate(double latitude, double longitude, double altitude, double course, double horizontalAccuracy, double verticalAccuracy, double speed)
        {
            _course = course;
            _horizontalAccuracy = horizontalAccuracy;
            _verticalAccuracy = verticalAccuracy;
            _latitude = latitude;
            _longitude = longitude;
            _speed = speed;
            Altitude = altitude;
        }

        public GeoCoordinate(double latitude, double longitude, double altitude) : this(latitude, longitude, altitude, double.NaN, double.NaN, double.NaN, double.NaN) { }
        public GeoCoordinate(double latitude, double longitude) : this(latitude, longitude, double.NaN, double.NaN, double.NaN, double.NaN, double.NaN) { }
        public GeoCoordinate() : this(double.NaN, double.NaN) { }

        public double Latitude { 
            get => _latitude;
            set 
            {
                if (value < -90.0 || value > 90.0)
                    throw new ArgumentOutOfRangeException("Latitude", "Argument must be in range of -90 e 90");
                _latitude = value;
            }
        }
        public double Longitude
        {
            get => _longitude;
            set
            {
                if (value < -90.0 || value > 90.0)
                    throw new ArgumentOutOfRangeException("Longitude", "Argument must be in range of -90 e 90");
                _longitude = value;
            }
        }

        public double Altitude { get; set; }       

        public double HorizontalAccuracy
        {
            get => _horizontalAccuracy;
            set
            {
                if (value < 0.0)
                    throw new ArgumentOutOfRangeException("HorizontalAccuracy", "Argument must be non negative");
                _horizontalAccuracy = value;
            }
        }

        public double VerticalAccuracy
        {
            get => _verticalAccuracy;
            set
            {
                if (value < 0.0)
                    throw new ArgumentOutOfRangeException("VerticalAccuracy", "Argument must be non negative");
                _verticalAccuracy = value;
            }
        }

        public double Speed
        {
            get => _speed;
            set
            {
                if (value < 0.0)
                    throw new ArgumentOutOfRangeException("Speed", "Argument must be non negative");
                _speed = value;
            }
        }

        public double Course
        {
            get => _course;
            set
            {
                if (value < 0.0 || value > 360.0)
                    throw new ArgumentOutOfRangeException("Course", "Argument must be in range of 0 e 360");
                _course = value;
            }
        }        

        public double GetDistanceTo(GeoCoordinate other, DistanceType distanceType = DistanceType.Meters)
        {
            if (double.IsNaN(Latitude) || double.IsNaN(Longitude) || double.IsNaN(other.Latitude) || double.IsNaN(other.Longitude))
                throw new ArgumentException("Argumento latitude or longitude is not a number");

            var thisLatitudeInRadians = Latitude * (Math.PI / 180);
            var thisLongitudeInRadians = Longitude * (Math.PI / 180);

            var otherLatitudeInRadians = other.Latitude * (Math.PI / 180);
            var otherLongitudeInRadians = other.Longitude * (Math.PI / 180);

            var deltaLatitude = otherLatitudeInRadians - thisLatitudeInRadians;
            var deltaLongitude = otherLongitudeInRadians - thisLongitudeInRadians;

            //eu não sei o significa do calculo abaixo e por isso eu nomeie a variável de "x", pois ela é uma incognita pra mim.
            var x = Math.Pow(Math.Sin(deltaLatitude / 2.0), 2.0) +
                    Math.Cos(thisLatitudeInRadians) * 
                    Math.Cos(otherLatitudeInRadians) * 
                    Math.Pow(Math.Sin(deltaLongitude / 2.0), 2.0);

            var distanceInMeters = 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(x), Math.Sqrt(1.0 - x)));

            return distanceType == DistanceType.Meters ? distanceInMeters : distanceInMeters / 1000;        
        }

        public bool IsUnknown() => Equals(Unknown);        
        public bool Equals(GeoCoordinate? other) => other is not null && Latitude == other.Latitude && Longitude == other.Longitude;
        public override bool Equals(object? obj) => Equals(obj as GeoCoordinate);
        public static bool operator ==(GeoCoordinate left, GeoCoordinate right) => left?.Equals(right) ?? right is null;        
        public static bool operator !=(GeoCoordinate left, GeoCoordinate right) => !(left == right);        
        public override int GetHashCode() => Latitude.GetHashCode() ^ Longitude.GetHashCode();
        public override string ToString() => IsUnknown() ? "Unknown" : $"{Latitude.ToString("G", CultureInfo.InvariantCulture)}, {Longitude.ToString("G", CultureInfo.InvariantCulture)}";
    }    
}
