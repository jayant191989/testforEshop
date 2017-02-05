using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR_Management.Web.Areas.Products.Controllers
{
    public class ImageProcessController : Controller
    {
        // GET: ImageProcess
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Thumbnail(string path,string imagename,int width, int height)
        {
            // TODO: the filename could be passed as argument of course
            var imageFile = Path.Combine(Server.MapPath(path), imagename);
            using (var srcImage = Image.FromFile(imageFile))
            using (var newImage = new Bitmap(width, height))
            using (var graphics = Graphics.FromImage(newImage))
            using (var stream = new MemoryStream())
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(srcImage, new Rectangle(0, 0, width, height));
                newImage.Save(stream, ImageFormat.Png);
                return File(stream.ToArray(), "image/png");
            }
        }
    }
}