using GEI797Labo.Models.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEI797Labo.Models.Collections
{
    internal class ShapeList : IDisposable
    {
        private readonly List<Shape> oShapes;
        private bool disposed = false;

        public ShapeList() 
        {
            oShapes = new List<Shape>();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                oShapes.Clear();
                disposed = true;
            }
        }

        public Shape GetItem(int index)
        {
            if (index < 0 || index >= Count) return null;
            return oShapes[index];
        }

        public void Clear()
        {
            oShapes.Clear();
        }

        public void Add(Shape shape)
        {
            oShapes.Add(shape);
        }

        public int Count
        {
            get { return oShapes.Count; }
        }
    }
}
