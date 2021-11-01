using System;
using System.Collections.Generic;
using System.Text;

namespace sourceTree
{
    public class ImageModel
    { 

    public string image { set; get; }
    public string type { set; get; }

    public ImageModel(string img, string ty = "base64")
    {
        image = img;
        type = ty;   
    }
}
}
