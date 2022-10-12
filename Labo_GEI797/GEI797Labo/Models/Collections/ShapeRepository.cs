using GEI797Labo.Models.Definitions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEI797Labo.Models.Collections
{
    internal class ShapeRepository : IRepository<Shape>
    {
        private readonly List<Shape> oShapes;
        private bool disposed = false;

        public ShapeRepository() 
        {

        }

        public List<Shape> FindById(params object[] values)
        {
            for i in 
            return oShapes[id];
        }

        public IEnumerable FindAll()
        {
            return null;
        }

        public void Insert(Shape entity)
        {
            oShapes.Add(entity);
        }

        public void Update(Shape entity)
        {
            
        }

        public void Delete(Shape entity)
        {
            oShapes.Remove(entity);
        }

    }
}
