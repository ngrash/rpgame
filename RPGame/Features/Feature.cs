using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGame.Features
{
    abstract class Feature : IFeature
    {
        public virtual Entity Entity
        {
            get;
            set;
        }

        public abstract void Update(float timeElapsed);
    }
}
