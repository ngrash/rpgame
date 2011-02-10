using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGame.Features
{
    public interface IFeature
    {
        Entity Entity { get; set; }
        void Update(float timeElapsed);
    }
}
