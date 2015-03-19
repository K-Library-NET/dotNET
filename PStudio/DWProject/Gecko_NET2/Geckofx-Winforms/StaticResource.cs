using System;
using System.Collections.Generic;
using System.Text;

namespace GeckoFxWinForm
{
    public class StaticResource
    {
        public static byte[] Gecko18
        {
            get
            {
                object obj = Gecko.Properties.Resources.ResourceManager.GetObject("Gecko18", Gecko.Properties.Resources.Culture);
                return ((byte[])(obj));
            }
        }
    }
}
