using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using android_service_stack.ServiceModel.response;

namespace android_service_stack.ServiceModel.message
{
    [Route("/storage/out", "POST")]
    public class StorageOut : IReturn<NullResponse>
    {
        public String storageOutRequest { get; set; }
    }
}
