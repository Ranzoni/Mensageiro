using Microsoft.Extensions.DependencyInjection;

namespace Mensageiro.WebApi
{
    /// <summary>
    /// Classe estática responsável por fornecer ações para a injeção de dependência da API .NET
    /// </summary>
    public static class InjecaoDependencia
    {
        /// <summary>
        /// Método responsável por fazer a injeção de dependência do Notificador
        /// </summary>
        /// <param name="services"></param>
        public static void AddMensageiro(this IServiceCollection services)
        {
            services.AddScoped<INotificador, Notificador>();
        }
    }
}
