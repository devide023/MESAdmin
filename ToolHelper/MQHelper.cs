using NPOI.SS.Formula.Functions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ZDToolHelper
{
    public enum WorkMode
    {
        /// <summary>
        /// 平均分发模式
        /// </summary>
        direct,
        /// <summary>
        /// 一对多工作模式
        /// </summary>
        fanout,
        /// <summary>
        /// 自由匹配模式
        /// </summary>
        topic,
        /// <summary>
        /// 暂不使用
        /// </summary>
        [Obsolete]
        headers,
    }

    /// <summary>
    /// RabbitMQ生产者
    /// </summary>
    public class RabbitMQCreater : IDisposable
    {
        #region 构造函数
        public RabbitMQCreater()
        {
            var username = ConfigurationManager.AppSettings["mqusername"]?.ToString();
            var psword = ConfigurationManager.AppSettings["mquserpwd"]?.ToString();
            var ip = ConfigurationManager.AppSettings["mqip"]?.ToString();
            var port = ConfigurationManager.AppSettings["mqport"]?.ToString();
            var factory = new ConnectionFactory()
            {
                HostName = ip,//主机名，Rabbit会拿这个IP生成一个endpoint，这个很熟悉吧，就是socket绑定的那个终结点。
                Port = Convert.ToInt32(port),
                UserName = username,//默认用户名,用户可以在服务端自定义创建，有相关命令行
                Password = psword,//默认密码
                ClientProvidedName = "ZD_JJMES_Creater",//建议给定一个名称
            };
            //连接服务器，即正在创建终结点。
            //创建一个通道，这个就是Rabbit自己定义的规则了，如果自己写消息队列，这个就可以开脑洞设计了
            //这里Rabbit的玩法就是一个通道channel下包含多个队列Queue
            conn = factory.CreateConnection();
            chan = conn.CreateModel();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="as_createrName">生产者名称，可以在RabbitMQ管理页面中查询到的名称</param>
        /// <param name="as_id">账户</param>
        /// <param name="as_psword">密码</param>
        /// <param name="as_ip">MQ服务器地址</param>
        /// <param name="as_port">MQ服务器端口号</param>
        public RabbitMQCreater(string as_createrName, string as_id, string as_psword, string as_ip, int as_port = 5672)
        {
            createrName = as_createrName;
            id = as_id;
            psword = as_psword;
            ip = as_ip;
            port = as_port;
            var factory = new ConnectionFactory()
            {
                HostName = as_ip,//主机名，Rabbit会拿这个IP生成一个endpoint，这个很熟悉吧，就是socket绑定的那个终结点。
                Port = as_port,
                UserName = as_id,//默认用户名,用户可以在服务端自定义创建，有相关命令行
                Password = as_psword,//默认密码
                ClientProvidedName = as_createrName,//,建议给定一个名称
            };
            //连接服务器，即正在创建终结点。
            //创建一个通道，这个就是Rabbit自己定义的规则了，如果自己写消息队列，这个就可以开脑洞设计了
            //这里Rabbit的玩法就是一个通道channel下包含多个队列Queue
            conn = factory.CreateConnection();
            chan = conn.CreateModel();
        }

        #endregion

        #region 字段

        string createrName, id, psword, ip;
        int port;
        IConnection conn;
        IModel chan;
        List<string> qnames = new List<string>();
        List<string> exnames = new List<string>();

        #endregion

        #region 属性

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        #endregion

        /// <summary>
        /// 创建队列或交换器
        /// </summary>
        /// <param name="qnames"></param>
        /// <param name="extype"></param>
        /// <returns></returns>
        public bool CreateQueue(string qname, WorkMode extype, out string err)
        {
            switch (extype)
            {
                case WorkMode.direct:
                    //参数含义：
                    //queue队列名称、
                    //durable队列是否持久化【服务器重启后还能保持】、
                    //exclusive队列排他【如果true，则队列只能针对这个ConnectionFactory可见，其他连接或客户端程序均不可见】
                    //AutoDelete自动删除，创建该连接后，任意一个消费者连接到队列后，再等到没有任何一个消费者连接时，就自动删除此队列
                    try
                    {
                        //var re_temp =
                        chan.QueueDeclare(qname, false, false, false, null);//创建一个名称为kibaqueue的消息队列
                    }
                    catch (Exception exc)
                    {
                        err = exc.Message;
                        return false;
                    }
                    err = "";
                    qnames.Add(qname);
                    return true;
                case WorkMode.fanout:
                    try
                    {
                        chan.ExchangeDeclare(qname, ExchangeType.Fanout);//声明fanout交换器
                        //chan.QueueDeclare(qname, false, false, false, null);
                        //chan.QueueBind(qname, qname, "", null);
                    }
                    catch (Exception exc)
                    {
                        err = exc.Message;
                        return false;
                    }
                    err = "";
                    exnames.Add(qname);
                    return true;
                default:
                    err = "不支持的工作模式";
                    return false;
            }
        }

        /// <summary>
        /// 创建队列或交换器
        /// </summary>
        /// <param name="qnames"></param>
        /// <param name="extype"></param>
        /// <returns></returns>
        public bool CreateQueue(List<string> qnames, WorkMode extype, out List<string> err)
        {
            err = qnames.ConvertAll(qw =>
            {
                CreateQueue(qw, extype, out string err_temp);
                return err_temp;
            });
            return err.TrueForAll(qw => qw == "");
        }

        /// <summary>
        /// 向一个队列或交换器发送数据
        /// </summary>
        /// <param name="qname"></param>
        /// <param name="msg"></param>
        /// <param name="err"></param>
        /// <param name="persistent"></param>
        /// <returns></returns>
        public bool Send(string qname, string msg, out string err, bool persistent = false)
        {
            IBasicProperties properties = null;
            if (persistent)
            {
                properties = chan.CreateBasicProperties();
                properties.DeliveryMode = 2;
            }
            try
            {
                if (qnames.Contains(qname))
                {
                    chan.BasicPublish("", qname, properties, Encoding.GetBytes(msg));
                }
                else
                {
                    chan.BasicPublish(qname, "", properties, Encoding.GetBytes(msg));
                }
            }
            catch (Exception exc)
            {
                err = exc.Message;
                return false;
            }
            err = "";
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            try
            {
                chan.Dispose();
                conn.Dispose();
            }
            catch
            {
            }
        }
    }

    /// <summary>
    /// RabbitMQ消息消费者
    /// </summary>
    public class RabbitMQConsumer : IDisposable
    {
        #region 构造函数
        public RabbitMQConsumer()
        {
            var username = ConfigurationManager.AppSettings["mqusername"]?.ToString();
            var psword = ConfigurationManager.AppSettings["mquserpwd"]?.ToString();
            var ip = ConfigurationManager.AppSettings["mqip"]?.ToString();
            var port = ConfigurationManager.AppSettings["mqport"]?.ToString();
            var factory = new ConnectionFactory()
            {
                HostName = ip,//主机名，Rabbit会拿这个IP生成一个endpoint，这个很熟悉吧，就是socket绑定的那个终结点。
                Port = Convert.ToInt32(port),
                UserName = username,//默认用户名,用户可以在服务端自定义创建，有相关命令行
                Password = psword,//默认密码
                ClientProvidedName = "ZD_JJMES_Consumer",//,建议给定一个名称
            };
            //连接服务器，即正在创建终结点。
            //创建一个通道，这个就是Rabbit自己定义的规则了，如果自己写消息队列，这个就可以开脑洞设计了
            //这里Rabbit的玩法就是一个通道channel下包含多个队列Queue
            conn = factory.CreateConnection();
            chan = conn.CreateModel();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="as_consumerName">生产者名称，可以在RabbitMQ管理页面中查询到的名称</param>
        /// <param name="as_id">账户</param>
        /// <param name="as_psword">密码</param>
        /// <param name="as_ip">MQ服务器地址</param>
        /// <param name="as_port">MQ服务器端口号</param>
        public RabbitMQConsumer(string as_consumerName, string as_id, string as_psword, string as_ip, int as_port = 5672)
        {
            consumerName = as_consumerName;
            id = as_id;
            psword = as_psword;
            ip = as_ip;
            port = as_port;
            var factory = new ConnectionFactory()
            {
                HostName = as_ip,//主机名，Rabbit会拿这个IP生成一个endpoint，这个很熟悉吧，就是socket绑定的那个终结点。
                Port = as_port,
                UserName = as_id,//默认用户名,用户可以在服务端自定义创建，有相关命令行
                Password = as_psword,//默认密码
                ClientProvidedName = as_consumerName,//,建议给定一个名称
            };
            //连接服务器，即正在创建终结点。
            //创建一个通道，这个就是Rabbit自己定义的规则了，如果自己写消息队列，这个就可以开脑洞设计了
            //这里Rabbit的玩法就是一个通道channel下包含多个队列Queue
            conn = factory.CreateConnection();
            chan = conn.CreateModel();
        }

        #endregion

        #region 字段

        string consumerName, id, psword, ip;
        int port;
        IConnection conn;
        IModel chan;

        #endregion

        #region 属性

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public delegate void ReceMsg_Dele(string qname, string msg, string cName);

        public event ReceMsg_Dele ReceMsg;

        #endregion

        /// <summary>
        /// 从队列订阅数据
        /// </summary>
        /// <param name="qnames"></param>
        /// <returns></returns>
        public void SubscribeMsgFromQueue(string qname)
        {
            var consumer = new EventingBasicConsumer(chan);//消费者
            chan.BasicConsume(qname, true, consumer);//消费消息
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var msg = Encoding.GetString(body);
                ReceMsg?.Invoke(qname, msg, consumerName);
            };
        }

        /// <summary>
        /// 从交换器上订阅数据
        /// </summary>
        /// <param name="qnames"></param>
        /// <returns></returns>
        public void SubscribeMsgFromExChange(string exname, string qname = "")
        {
            chan.ExchangeDeclare(exname, ExchangeType.Fanout);//声明fanout交换器
            if (qname == "")
            {
                //声明一个队列，这个队列的名称随机
                var queueName = chan.QueueDeclare().QueueName;
            }
            else
            {
                chan.QueueDeclare(qname, false, false, false, null);
            }
            //将这个队列绑定（bind）到交换机上面
            chan.QueueBind(qname, exname, "");
            //声明一个consumer
            var consumer = new EventingBasicConsumer(chan);
            //一个委托，只要这个程序不被杀死，这段代码便一直监听rabbitmq，有消息就实时收到
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var msg = Encoding.GetString(body);
                ReceMsg?.Invoke(ea.RoutingKey, msg, consumerName);
            };
            chan.BasicConsume(qname, true, consumer);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            try
            {
                chan.Dispose();
                conn.Dispose();
            }
            catch
            {
            }
        }
    }
}
