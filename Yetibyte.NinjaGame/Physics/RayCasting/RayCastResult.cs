using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Yetibyte.NinjaGame.Physics.RayCasting
{
    public class RayCastResult
    {

        public IEnumerable<Collider> Colliders { get; private set; }

        public Vector2 Normal { get; private set; }
        public float Fraction { get; private set; }

        public Vector2 Origin { get; private set; }
        public Vector2 Destination { get; private set; }


        public RayCastResult(IEnumerable<Collider> colliders, Vector2 normal, float fraction, Vector2 origin, Vector2 destination)
        {
            Colliders = colliders?.ToArray() ?? Array.Empty<Collider>();
            Normal = normal;
            Fraction = fraction;
            Origin = origin;
            Destination = destination;
        }

        public RayCastResult(Collider collider, Vector2 normal, float fraction, Vector2 origin, Vector2 destination)
            : this(collider != null ? new[] { collider } : Array.Empty<Collider>(), normal, fraction, origin, destination)
        {
        }

        public Vector2 HitPoint => Vector2.Lerp(Origin, Destination, Fraction);

        public Collider Collider => Colliders.FirstOrDefault();

        public bool HasHit => Colliders.Any();

        public int HitCount => Colliders.Count();

    }
}
