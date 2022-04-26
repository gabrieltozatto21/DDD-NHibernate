using DDD.NHibernate.Dominio.UsuariosAcesso.Entidades;
using DDD.NHibernate.Dominio.UsuariosAcesso.Repositorios;
using DDD.NHibernate.Lib.NHibernate.Repositorios;
using DDD.NHibernate.Libs.Biblioteca.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NHibernate;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DDD.NHibernate.Infra.UsuariosAcesso.Repositorios
{
    public class UsuariosAcessoRepositorio : RepositorioNHibernate<UsuarioAcesso>, IUsuarioAcessoRepositorio
    {
        private IConfiguration configuracao;
        public UsuariosAcessoRepositorio(ISession session, IConfiguration configuracao) : base(session)
        {
            this.configuracao = configuracao;
        }

        public string CriptografarSenhaAcesso(string login, string senha)
        {
            var hash = CriptografiaMD5.Criptografar(senha + "." + login);
            return hash;
        }

        public string GerarTokenJwt(SessaoAcesso sessao)
        {
            string secret = configuracao.GetSection("Jwt:Secret").Value;

            string id = sessao.Codigo.ToString();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, id)
            };

            DateTime dataCriacao = DateTime.Now.AddMinutes(-5);
            DateTime dataExpiracao = dataCriacao + TimeSpan.FromHours(8);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var handler = new JwtSecurityTokenHandler();
            var securityToken = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claims),
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            };

            return handler.WriteToken(handler.CreateToken(securityToken)); ;
        }
    }
}
