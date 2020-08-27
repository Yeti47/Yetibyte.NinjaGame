using System;
using System.Collections.Generic;
using System.Text;

namespace Yetibyte.NinjaGame.SceneManagement
{
    public class SceneDelegateException : Exception
    {
        public SceneDelegateException(string message, Exception innerException) : base(message, innerException)
        {

        }    

    }
}
