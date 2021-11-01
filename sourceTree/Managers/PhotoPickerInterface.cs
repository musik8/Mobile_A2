using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace sourceTree
{
    public interface PhotoPickerInterface
    {
        Task<Stream> GetImageStreamAsync();
    }
}
