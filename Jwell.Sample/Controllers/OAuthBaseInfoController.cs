using Jwell.Application.Constant;
using Jwell.Framework.Utilities;
using Jwell.Modules.Configure;
using Jwell.Sample.Common;
using Jwell.Sample.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jwell.Sample.Controllers
{
    /// <summary>
    /// 基础系统回调方法
    /// </summary>
    public class OAuthBaseInfoController : Controller
    {
        /// <summary>
        /// 回调
        /// </summary>
        /// <param name="code">此处code为employeeID</param>
        /// <param name="state"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult AuthorizationCallBack(string code, string state, string returnUrl)
        {
            string cookieKey = JwellConfig.GetAppSetting("accessToken");


            if (JwellConfig.GetAppSetting("scope") == ApplicationConstant.BASEINFO)
            {
                var param = new NameValueCollection
                {
                    ["clientId"] = JwellConfig.GetAppSetting("clientId"),
                    ["clientSecret"] = JwellConfig.GetAppSetting("clientSecret"),
                    ["code"] = code,
                    ["grantType"] = "authorizationCode"
                };
                //根据Code申请Token
                var token = GetToken(cookieKey, param);
                if (token != null)
                {
                    int expireTime = 0;
                    int.TryParse(JwellConfig.GetAppSetting("codeExpire"), out expireTime);
                    var cookie = new HttpCookie(cookieKey, token.Value<string>())
                    {
                        //cookie过期时间固定设置为12小时，与token过期时间一致
                        Expires = DateTime.Now.AddHours(expireTime)
                    };

                    var userInfo = GetUserInfo(token.Value<string>(), cookieKey);
                    Request.RequestContext.HttpContext.Session["userContext"] = userInfo;

                    System.Web.HttpContext.Current.Response.AppendCookie(cookie);
                    if (!string.IsNullOrWhiteSpace(returnUrl))
                    {
                        if (returnUrl.Contains("~")) //解决前端URl存在#的问题
                        {
                            returnUrl = returnUrl.Replace("~", "#");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(returnUrl);
                }
            }
            return Redirect(OAuthHelper.GenerateLoginUrl(this.Url.Action("AuthorizationCallBack", "OAuth", null, Request.Url.Scheme),
                $"http://{HttpContext.Request.Url.Authority}/Home/index"));
        }

        private UserInfo GetUserInfo(string token, string cookieKey)
        {
            string jsonInfo = OAuthHelper.GetUserInfo(token, cookieKey);
            UserInfo userInfo = Serializer.FromJson<UserInfo>(jsonInfo);
            return userInfo;
        }

        private JToken GetToken(string cookieKey, NameValueCollection param)
        {
            var result = OAuthHelper.PostRequest(JwellConfig.GetAppSetting("baseInfoForTokenUrl"), param);
            var value = JObject.Parse(result);
            //取得Token存到Cookie里面
            return value[cookieKey];
        }
    }
}