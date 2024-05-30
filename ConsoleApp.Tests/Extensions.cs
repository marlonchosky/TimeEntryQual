using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp.Tests;

public static class Extensions {
    /// <summary>
    /// Extracted from https://stackoverflow.com/a/64043932/2844593
    /// </summary>
    public static bool IsServiceRegistered(this IServiceCollection serviceCollection,
        Type serviceType, ServiceLifetime serviceLifetime) {

        var serviceDescriptors = serviceCollection.GetEnumerator();
        if (serviceDescriptors == null)
            return false;

        while (serviceDescriptors.MoveNext()) {
            var current = serviceDescriptors.Current;
            if (current.Lifetime == serviceLifetime && current.ServiceType == serviceType) {
                return true;
            }
        }

        return false;
    }
}
