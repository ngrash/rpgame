using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGame.Features
{
    public abstract class Feature : IFeature
    {
        public virtual Entity Entity
        {
            get;
            set;
        }

        public virtual void Update(float timeElapsed) { }
        public virtual void Initialize() { }
    }
}
