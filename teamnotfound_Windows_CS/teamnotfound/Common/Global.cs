using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace teamnotfound.Common
{
   
    static  class Global
    {
        static private ApplicationDataCompositeValue repository = new ApplicationDataCompositeValue();
        public static Object GetRepositoryValue(String key)
        {
            if (repository != null)
            {
                return repository[key];
            }
            else
                return null;
        }
       public  static void SetRepositoryValue(String key,Object value)
        {
            if(repository != null)
            {
                repository[key] = value;

            }
        }
        static string username { set; get; }

    }
   

}
