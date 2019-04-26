using pAPI_WSD_Agenda.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace pAPI_WSD_Agenda
{
    public class AuthSession
    {
        public string Name { get; set; }

        public string Email { get; set; }

        //public static Usuario Login_SetToken(String NOLOGIN, String SENHA)
        //{
        //    Usuario usuario = new UsuarioDAO().LoginUsuarioLogin(NOLOGIN, SENHA);

        //    if (usuario == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        usuario.CHAVEWEB = (Guid.NewGuid().ToString() + Guid.NewGuid().ToString() + Guid.NewGuid().ToString()).ToUpper();
        //        new UsuarioDAO().SetaChave(usuario.CHAVEWEB, usuario.CDPESSOAUSR + "");
        //        return usuario;
        //    }            
        //}

        //public static Usuario getUserFromToken(String TOKEN)
        //{
        //    Usuario usuario = new UsuarioDAO().BuscaUserFromToken(TOKEN);

        //    return usuario;
        //}

    }

}