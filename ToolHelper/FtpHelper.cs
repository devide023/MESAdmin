using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace ZDToolHelper
{
    public class FtpHelper
    {
        private static FtpWebRequest GetRequest(string URI, string username, string password)
        {
            //根据服务器信息FtpWebRequest创建类的对象
            FtpWebRequest result = (FtpWebRequest)FtpWebRequest.Create(URI);
            if (!string.IsNullOrEmpty(username))
            {
                //提供身份验证信息
                result.Credentials = new System.Net.NetworkCredential(username, password);
            }
            //设置请求完成之后是否保持到FTP服务器的控制连接，默认值为true
            result.KeepAlive = true;
            return result;
        }

        private bool ftpIsExistsFile(string dirName, string ftpHostIP, string username, string password)
        {
            bool flag = true;
            try
            {
                string uri = "ftp://" + ftpHostIP + "/" + dirName;
                System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
                ftp.Method = WebRequestMethods.Ftp.ListDirectory;

                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dirName">创建的目录名称</param>
        /// <param name="ftpHostIP">ftp地址</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public void delDir(string dirName, string ftpHostIP, string username, string password)
        {
            try
            {
                string uri = "ftp://" + ftpHostIP + "/" + dirName;
                System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
                ftp.Method = WebRequestMethods.Ftp.RemoveDirectory;
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
            catch (Exception)
            {
            }
        }

        ///<summary>
        /// 在ftp服务器上创建目录
        /// </summary>
        /// <param name="dirName">创建的目录名称</param>
        /// <param name="ftpHostIP">ftp地址</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public void MakeDir(string dirName, string ftpHostIP, string username, string password)
        {
            try
            {
                string uri = "ftp://" + ftpHostIP + "/" + dirName;
                System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
                ftp.Method = WebRequestMethods.Ftp.MakeDirectory;

                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
            catch (Exception)
            {
            }
        }

        public void UploadFile(FileInfo fileinfo, string targetDir, string hostname, string username, string password)
        {
            string target;
            if (targetDir.Trim() == "")
            {
                return;
            }
            target = Guid.NewGuid().ToString() + fileinfo.Extension;  //使用临时文件名
            string URI = "FTP://" + hostname + "/" + targetDir + "/" + target;
            FtpWebRequest ftp = GetRequest(URI, username, password);
            ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            //指定文件传输的数据类型
            ftp.UseBinary = true;
            ftp.UsePassive = true;
            //告诉ftp文件大小
            ftp.ContentLength = fileinfo.Length;
            //缓冲大小设置为2KB
            const int BufferSize = 40960;
            byte[] content = new byte[BufferSize];
            int dataRead;
            //打开一个文件流 (System.IO.FileStream) 去读上传的文件
            using (FileStream fs = fileinfo.OpenRead())
            {
                try
                {
                    //把上传的文件写入流
                    using (Stream rs = ftp.GetRequestStream())
                    {
                        do
                        {
                            //每次读文件流的2KB
                            dataRead = fs.Read(content, 0, BufferSize);
                            rs.Write(content, 0, dataRead);
                        } while (!(dataRead < BufferSize));
                        rs.Close();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    fs.Close();
                }
            }
            ftp = null;
            //设置FTP命令
            ftp = GetRequest(URI, username, password);
            ftp.Method = System.Net.WebRequestMethods.Ftp.Rename; //改名
            ftp.RenameTo = fileinfo.Name;
            try
            {
                ftp.GetResponse();
            }
            catch (Exception ex)
            {
                ftp = GetRequest(URI, username, password);
                ftp.Method = System.Net.WebRequestMethods.Ftp.DeleteFile; //删除
                ftp.GetResponse();
                throw ex;
            }
        }

        public void UploadFile(Stream fs, string hostname, string filename, string username, string password)
        {
            string URI = "FTP://" + hostname +"/"+ filename;
            FtpWebRequest ftp = GetRequest(URI, username, password);
            ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            //指定文件传输的数据类型
            ftp.UseBinary = true;
            ftp.UsePassive = true;
            //告诉ftp文件大小
            ftp.ContentLength = fs.Length;
            //缓冲大小设置为2KB
            const int BufferSize = 40960;
            byte[] content = new byte[BufferSize];
            int dataRead;
            try
            {
                //把上传的文件写入流
                using (Stream rs = ftp.GetRequestStream())
                {
                    do
                    {
                        //每次读文件流的40KB
                        dataRead = fs.Read(content, 0, BufferSize);
                        rs.Write(content, 0, dataRead);
                    } while (!(dataRead < BufferSize));
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="FtpDir">ftp目标文件路径</param>
        /// <param name="FtpFile">从ftp要下载的文件名</param>
        /// <param name="hostname">ftp地址即IP</param>
        /// <param name="username">ftp用户名</param>
        /// <param name="password">ftp密码</param>
        public bool DownloadFile( string hostname, string FtpDir, string FtpFile, string username, string password)
        {
            try
            {
                string URI = "FTP://" + hostname + "/" + FtpDir + "/" + FtpFile;
                FtpWebRequest ftp = GetRequest(URI, username, password);
                ftp.Method = WebRequestMethods.Ftp.DownloadFile;
                ftp.UseBinary = true;
                using (FtpWebResponse ftpWebResponse = (FtpWebResponse)ftp.GetResponse())
                {
                    string downpath = HttpContext.Current.Server.MapPath("~/DownLoad/");
                    string localfile = downpath + FtpFile;
                    var ftpStream = ftpWebResponse.GetResponseStream();
                    using (FileStream fs = new FileStream(localfile, FileMode.OpenOrCreate))
                    {
                        try
                        {
                            byte[] buffer = new byte[20480];
                            int read = 0;
                            do
                            {
                                read = ftpStream.Read(buffer, 0, buffer.Length);
                                fs.Write(buffer, 0, read);
                            } while (!(read == 0));
                            ftpStream.Close();
                            fs.Flush();
                            fs.Close();
                            ftpWebResponse.Close();
                            return true;
                        }
                        catch (Exception)
                        {
                            fs.Close();
                            File.Delete(localfile);
                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public byte[] GetFtpFile(string url, string username, string password)
        {
            try
            {
                FtpWebRequest ftp = GetRequest(url, username, password);
                ftp.Method = WebRequestMethods.Ftp.DownloadFile;
                ftp.UseBinary = true;
                using (FtpWebResponse ftpWebResponse = (FtpWebResponse)ftp.GetResponse())
                {
                    var ftpStream = ftpWebResponse.GetResponseStream();
                    byte[] bytes = null;
                    using (StreamReader sr = new StreamReader(ftpStream))
                    {
                        string fileContent = sr.ReadToEnd();
                        bytes = Encoding.UTF8.GetBytes(fileContent);
                    }
                    return bytes;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 文件重命名
        /// </summary>
        /// <param name="currentFilename">当前目录名称</param>
        /// <param name="newFilename">重命名目录名称</param>
        /// <param name="ftpServerIP">ftp地址</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public void Rename(string currentFilename, string newFilename, string ftpServerIP, string username, string password)
        {
            try
            {

                FileInfo fileInf = new FileInfo(currentFilename);
                string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
                System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
                ftp.Method = WebRequestMethods.Ftp.Rename;
                ftp.RenameTo = newFilename;
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
         /// 删除文件
         /// </summary>
         /// <param name="fileName"></param>
         public void Delete(string currentFilename, string ftpServerIP, string username, string password)
         {
             try
             {
                FileInfo fileInf = new FileInfo(currentFilename);
                string uri = "ftp://" + ftpServerIP + fileInf.Name;
                FtpWebRequest ftp = GetRequest(uri, username, password);
                ftp.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
    }
}