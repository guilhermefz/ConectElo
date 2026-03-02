using System.Runtime.InteropServices;

namespace ConectElo.Application.Areas.Base
{
    public class BaseResponse<T>
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public T? Dados { get; set; }
        public List<string> Erros { get; set; } = new();

        public static BaseResponse<T> Ok(T dados, string mensagem = "Operação realizada com sucesso")
            => new() { Sucesso = true, Dados = dados, Mensagem = mensagem };

        public static BaseResponse<T> Falha(string mensagem, List<string>? erros = null)
            => new() { Sucesso = false, Mensagem = mensagem, Erros = erros ??  new() };
     }
}
