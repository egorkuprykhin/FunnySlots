using EntryPoint;
using FSM;
using SceneData;
using Scripts.Configuration;
using Services;
using States;
using VContainer;
using VContainer.Unity;
using View;

namespace LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        public GameSceneData GameSceneData;
        public GlobalConfiguration GlobalConfiguration;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameEntryPoint>();
            
            builder.Register<StateMachine>(Lifetime.Singleton);
            
            builder.Register<LoadingState>(Lifetime.Singleton);
            builder.Register<MenuState>(Lifetime.Singleton);
            builder.Register<GameplayState>(Lifetime.Singleton);

            builder.Register<SceneLoaderService>(Lifetime.Singleton);

            builder.RegisterComponent<GameSceneData>(GameSceneData);
            builder.RegisterComponent<MenuView>(GameSceneData.MenuView);
            builder.RegisterComponent<PolicyScreenView>(GameSceneData.PolicyScreenView);
            builder.RegisterComponent<ColorPrepareService>(GameSceneData.ColorPrepareService);

            builder.RegisterInstance(GlobalConfiguration);
        }
    }
}