﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorus.Model
{

    [Serializable]
    public class Collider
    {
        public GameObject parent;
        public Collider(GameObject parentObject)
        {
            parent = parentObject;
        }
        virtual public bool isColliderTouching(Collider otherCollider)
        {
            return false; 
        }
    }
}
