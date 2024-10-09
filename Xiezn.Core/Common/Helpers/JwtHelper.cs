using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xiezn.Core.Common.Helpers;
using Xiezn.Core.Models;
using Xiezn.Core.Models.ConfModel;

namespace Xiezn.Core.Common.Helpers
{
    public class JwtHelper
    {
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJWT(TokenModel tokenModel)
        {
            var dateTime = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Uid.ToString()),//用户Id
                new Claim("Uname", tokenModel.Uname),
                new Claim("Role", tokenModel.Role),//身份
                new Claim("Project", tokenModel.Project),
                new Claim("UNickname", tokenModel.UNickname),
                new Claim("Tablename", tokenModel.Tablename),
                new Claim(JwtRegisteredClaimNames.Iat,dateTime.ToString(), ClaimValueTypes.Integer64)
            };
            //秘钥
            JwtAuthConfig jwtAuthConfig = ConfigHelper.GetConfig<JwtAuthConfig>("JwtAuth");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuthConfig.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //过期时间
            double exp = 0;
            switch (tokenModel.TokenType)
            {
                case "Web":
                    exp = jwtAuthConfig.WebExp;
                    break;
                case "App":
                    exp = jwtAuthConfig.AppExp;
                    break;
                case "MiniProgram":
                    exp = jwtAuthConfig.MiniProgramExp;
                    break;
                case "Other":
                    exp = jwtAuthConfig.OtherExp;
                    break;
            }
            var jwt = new JwtSecurityToken(
                issuer: jwtAuthConfig.Issuer,
                claims: claims, //声明集合
                expires: dateTime.AddHours(exp),
                signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            return "Bearer " + encodedJwt;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static TokenModel SerializeJWT(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            try
            {
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
                object uname = new object();
                object role = new object();
                object project = new object();
                object uNickname = new object();
                object tablename = new object();
                jwtToken.Payload.TryGetValue("Uname", out uname);
                jwtToken.Payload.TryGetValue("Role", out role);
                jwtToken.Payload.TryGetValue("Project", out project);
                jwtToken.Payload.TryGetValue("UNickname", out uNickname);
                jwtToken.Payload.TryGetValue("Tablename", out tablename);

                var tm = new TokenModel
                {
                    Uid = long.Parse(jwtToken.Id),
                    Uname = uname.ToString(),
                    Role = role.ToString(),
                    Project = project.ToString(),
                    UNickname = uNickname.ToString(),
                    Tablename = tablename.ToString(),
                };
                return tm;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
