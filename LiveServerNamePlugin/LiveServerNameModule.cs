using AssettoServer.Server.Plugin;
using Autofac;

namespace LiveServerNamePlugin;

public class LiveServerNameModule : AssettoServerModule<LiveServerNameConfiguration>
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<LiveServerName>().AsSelf().AutoActivate().SingleInstance();
    }
}
