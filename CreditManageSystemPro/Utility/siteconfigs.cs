using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CreditManageSystemPro.Admin.Models;

namespace CreditManageSystemPro.Admin.Utility
{
    public class siteconfigs
    {
        private static object lockHelper = new object();
        /// <summary>
        ///  读取配置文件
        /// </summary>
        public siteconfig loadConfig()
        {
            siteconfig model = CacheHelper.Get<siteconfig>(DTKeys.CACHE_SITE_CONFIG);
            if (model == null)
            {
                CacheHelper.Insert(DTKeys.CACHE_SITE_CONFIG, DALloadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING)),
                    Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
                model = CacheHelper.Get<siteconfig>(DTKeys.CACHE_SITE_CONFIG);
            }
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public siteconfig saveConifg(siteconfig model)
        {
            return DALsaveConifg(model, Utils.GetXmlMapPath(DTKeys.FILE_SITE_XML_CONFING));
        }

        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public siteconfig DALloadConfig(string configFilePath)
        {
            return (siteconfig)SerializationHelper.Load(typeof(siteconfig), configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public siteconfig DALsaveConifg(siteconfig model, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(model, configFilePath);
            }
            return model;
        }
    }
}