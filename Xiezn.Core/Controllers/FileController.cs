using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Xiezn.Core.Business.Services;
using Xiezn.Core.Common.Helpers;

namespace Xiezn.Core.Controllers
{
    /// <summary>
    /// 文件接口
    /// </summary>
    [Route("[controller]/[action]")]
    // [Authorize(Roles = "Admin,Client")]
    public class FileController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _savePath;
        private readonly ConfigService _configBLL;

        public FileController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _savePath = _hostingEnvironment.WebRootPath + Path.DirectorySeparatorChar + ConfigHelper.GetConfig("SchemaName") + Path.DirectorySeparatorChar + "upload" + Path.DirectorySeparatorChar;
            _configBLL = new ConfigService();
        }

        /// <summary>
        /// 文件上传（单个文件）
        /// </summary>
        /// <param name="file"></param>
        /// <param name="type">当type=1时，表示后端上传接口(保存文件名到config表)</param>
        /// <returns></returns>
        [DisableRequestSizeLimit]
        [HttpPost]
        public JsonResult Upload(List<IFormFile> file, string type = "0")
        {
            try
            {
                //IFormFileCollection files = Request.Form.Files; // 获取上传的文件
                if (file == null || file.Count == 0)
                {
                    return Json(new { Code = -1, Msg = "没有上传文件！", File = "" });
                }

                FuncHelper.DicCreate(_savePath);
                string newFileName = "";

                foreach (IFormFile formFile in file)
                {
                    if (formFile.Length > 0)
                    {
                        var timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                        string fileExtension = formFile.FileName.Substring(formFile.FileName.LastIndexOf(".") + 1); // 获取文件名称后缀
                        //string newFileName = System.Guid.NewGuid().ToString() + "." + fileExtension; // 随机生成新的文件名
                        newFileName = timestamp + "." + fileExtension;
                        if(type.Contains("_template")) {
                            newFileName = type + "." + fileExtension;
                        }
                        // 保存文件
                        var stream = formFile.OpenReadStream();
                        // 把 Stream 转换成 byte[] 
                        byte[] bytes = new byte[stream.Length];
                        stream.Read(bytes, 0, bytes.Length);
                        // 设置当前流的位置为流的开始 
                        stream.Seek(0, SeekOrigin.Begin);
                        // 把 byte[] 写入文件 
                        FileStream fs = new FileStream(_savePath + newFileName, FileMode.Create);
                        BinaryWriter bw = new BinaryWriter(fs);
                        bw.Write(bytes);
                        bw.Close();
                        fs.Close();
                    }

                    break; // 限制多文件上传
                }

                if (type == "1")
                {
                    _configBLL.UpdateByName("faceFile", newFileName);
                }

                return Json(new { Code = 0, Msg = "上传成功！", File = newFileName });
            }
            catch (Exception ex)
            {
                return Json(new { Code = -2, Msg = "上传失败！", Data = ex.Message });
            }
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Download(string fileName)
        {
            try
            {
                string url = _savePath + fileName;
                var stream = System.IO.File.OpenRead(url);
                //var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); // 推荐此方法
                string fileExt = Path.GetExtension(url);
                // 获取文件的ContentType
                var provider = new FileExtensionContentTypeProvider();
                var memi = provider.Mappings[fileExt];
                return File(stream, memi, Path.GetFileName(url));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
