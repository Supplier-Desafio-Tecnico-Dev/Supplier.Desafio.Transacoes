﻿using Microsoft.Extensions.DependencyInjection;
using Supplier.Desafio.Commons.MessageBus;

namespace Supplier.Desafio.Transacoes.Consumer;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection)
    {
        if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException();

        services.AddSingleton<IMessageBus>(new MessageBus(connection));

        return services;
    }
}
